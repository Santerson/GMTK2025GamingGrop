using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Speed of the player movement
    List<Vector2> pathNodes = new List<Vector2>(); // List to hold path nodes

    [SerializeField] private float dashSpeed = 7f;
    [SerializeField] private float dashDuration = 0.2f;

    int currentNode = 0;
    int direction = 1;
    float baseSpeed = 0f;

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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // Dash in the current direction
            speed = dashSpeed; // Increase speed for dashing
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashDuration);
        speed = baseSpeed; // Reset speed after dashing
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
            }
            else
            {
                // Move towards the next node
                transform.position += (Vector3)(direction * step);
            }
        }
    }
}
