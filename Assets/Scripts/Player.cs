using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Speed of the player movement
    List<Vector2> pathNodes = new List<Vector2>(); // List to hold path nodes

    [SerializeField] private float dashSpeed = 7f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f; // Cooldown time for dashing
    [SerializeField] private LineRenderer cooldownLine = null;

    int currentNode = 0;
    int direction = 1;
    float baseSpeed = 0f;
    float dashCooldownTimeLeft = 0f;

    // Start is called before the first frame update
    void Start()
    {
        pathNodes = FindObjectOfType<PathGenerator>().PathNodes; // Get the path nodes from the PathGenerator
        transform.position = pathNodes[0];
        baseSpeed = speed; // Store the base speed for dashing  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimeLeft <= 0)
        {
            // Dash in the current direction
            speed = dashSpeed; // Increase speed for dashing
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
        speed = baseSpeed; // Reset speed after dashing
        dashCooldownTimeLeft = dashCooldown; // Start cooldown
    }

    private void FixedUpdate()
    { 
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
        //TODO: SKILL ISSUE
        Destroy(gameObject);
    }
}
