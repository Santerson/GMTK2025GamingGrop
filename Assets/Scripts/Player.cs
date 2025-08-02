using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // Speed of the player movement
    List<Vector2> pathNodes = new List<Vector2>(); // List to hold path nodes

    [SerializeField] private float dashIncrease = 8f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f; // Cooldown time for dashing
    [SerializeField] private float shieldDuration = 15f;
    [SerializeField] private LineRenderer cooldownLine = null;
    [SerializeField] GameObject shield;

    [SerializeField] private AudioSource damageSFX;
    [SerializeField] private AudioSource dashSFX;
    [SerializeField] private AudioSource shieldbreakSFX;
    [SerializeField] private AudioSource turnSFX;
    [SerializeField] private AudioSource deathSFX;
    [SerializeField] private AudioSource PowerupSFX;
    [SerializeField] private AudioSource screennukesfx;

    [SerializeField] private bool isControlable = true;

    int currentNode = 0;
    int direction = 1;
    float baseSpeed = 0f;
    float dashCooldownTimeLeft = 0f;

    public bool halted = false;
    public bool dying = false;

    // Start is called before the first frame update
    void Start()
    {
        pathNodes = FindObjectOfType<PathGenerator>().PathNodes; // Get the path nodes from the PathGenerator
        transform.position = pathNodes[0];
        baseSpeed = speed; // Store the base speed for dashing  
        shield.SetActive(false);
        try
        {
            playerColor script = FindObjectOfType<playerColor>();
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color(script.r/256f, script.g/256f, script.b/256f);
        }
        catch
        {
            Debug.LogError("an error occured. May have happened becasue you started from this scene. \n if so, ignore this.");
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (halted)
        {
            return;
        }
        movement();
        if (shieldDuration >= 0)
        {
            shieldDuration -= Time.deltaTime;
        }
        else
        {
            shield.SetActive(false);
        }
    }

    void movement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isControlable)
        {
            turnSFX.Play();
            //inverse the direction the player is going
            direction *= -1;
            currentNode += direction;
            if (currentNode > pathNodes.Count - 1)
            {
                currentNode = 0;
            }
            else if (currentNode < 0)
            {
                currentNode = pathNodes.Count - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimeLeft <= 0 && isControlable)
        {
            // Dash in the current direction
            speed += dashIncrease; // Increase speed for dashing
            dashSFX.Play();
            dashCooldownTimeLeft = 100f;
            StartCoroutine(Dash());
        }
        if (dashCooldownTimeLeft > 0)
        {
            dashCooldownTimeLeft -= Time.deltaTime; // Decrease cooldown time
            if (cooldownLine != null)
            {
                cooldownLine.enabled = true;
                cooldownLine.startWidth = 0.1f;
                cooldownLine.endWidth = 0.1f;

                // Calculate progress (0 = just started, 1 = fully cooled down)
                float progress = 1f - Mathf.Clamp01(dashCooldownTimeLeft / dashCooldown);

                // Center point directly above the player
                Vector2 center = new Vector2(transform.position.x, transform.position.y + 0.5f);

                // At 0% progress, both points are at 'center'
                // At 100% progress, points are 1 unit up and 1 unit down from 'center'
                cooldownLine.SetPosition(0, center + (new Vector2(-0.3f, 0) * progress));
                cooldownLine.SetPosition(1, center + (new Vector2(0.3f, 0) * progress));
            }
        }
        else if (cooldownLine != null)
        {
            cooldownLine.enabled = false; // Disable the line when not cooling down
        }
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashDuration);
        speed -= dashIncrease; // Reset speed after dashing
        dashCooldownTimeLeft = dashCooldown; // Start cooldown
    }

    private void FixedUpdate()
    { 
        if (halted)
        {
            return;
        }
        //move the player speed distance on the line they are currently on
        if (pathNodes.Count > 0)
        {
            Vector2 currentPosition = transform.position;
            Vector2 nextNode = pathNodes[currentNode];
            // Move towards the next node
            Vector2 direction = (nextNode - currentPosition).normalized;
            float step = speed * Time.fixedDeltaTime;
            // Check if we are close enough to the next node
            if (Vector2.Distance(currentPosition, nextNode) < step)
            {
                // Move to the next node
                transform.position = nextNode;
                

                currentNode += this.direction;
                if (currentNode > pathNodes.Count -1)
                {
                    currentNode = 0;
                }
                else if (currentNode < 0)
                {
                    currentNode = pathNodes.Count - 1;
                }

                step -= Vector2.Distance(currentPosition, nextNode);
                //keep moving the remaining distance
                if (step > 0)
                {
                    // Move towards the next node
                    nextNode = pathNodes[currentNode];
                    direction = (nextNode - currentPosition).normalized;
                    transform.position += (Vector3)(direction * step);
                }
            }
            else
            {
                // Move towards the next node
                transform.position += (Vector3)(direction * step);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpeedPowerup"))
        {
            PowerupSFX.Play();
            collision.GetComponent<SpeedPowerup>().activate();
        }
        else if (collision.CompareTag("NukePowerup"))
        {
            screennukesfx.Play();
            collision.GetComponent<screennukepowerup>().activate();
        }
        else if (collision.CompareTag("ShieldPowerup"))
        {
            PowerupSFX.Play();
            Destroy(collision.transform.parent.gameObject);
            shieldDuration = 15;
            shield.SetActive(true);
        }
        else
        {
            damageSFX.Play();
            if (shieldDuration <= 0)
            {
                //TODO: SKILL ISSUE
                death();
                dying = true;
                FindObjectOfType<Score>().OnPlayerDeath();

            }
            else
            {
                shieldDuration = 0;
                shield.SetActive(false);
                shieldbreakSFX.Play();
                if (collision.CompareTag("GIANTFUCKOFFLAZER"))
                {
                    FindObjectOfType<ObstacleGenerator>().lazerUp = false;
                }
                Destroy(collision.transform.parent.gameObject);
                
            }
        }
    }

    void death()
    {
        //freeze everything
        FindObjectOfType<ObstacleGenerator>().freeze();
        halted = true;
        StartCoroutine(haltDeath());
    }

    IEnumerator haltDeath()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<ObstacleGenerator>().thaw();
        deathSFX.Play();
        FindObjectOfType<DeathScript>().Death();
        Destroy(gameObject);

    }

    float tempSpeedUp = 0;
    public void temporarySpeedUp(float speed, float duration)
    {
        tempSpeedUp += speed;
        this.speed += speed;
        StartCoroutine(temporarySpeedUpDuration(duration));
    }

    IEnumerator temporarySpeedUpDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        speed -= tempSpeedUp;
        tempSpeedUp = 0;
    }
}
