using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ManagerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public AIPath aiPath;
    public AIDestinationSetter aiDestinationSetter;

    private Vector2 movement;
    private float speed;

    public Animator animator;
    public Animator bodyAnimator;

    // Targets for the AI to follow
    public List<Transform> targets;
    private List<Transform> queue;

    private int currentTarget = 0;

    void Start()
    {
        queue = new List<Transform>();
        for (int i = 0; i < targets.Count; i++)
        {
            queue.Add(targets[i]);
            Debug.Log("Target " + i + ": " + targets[i]);
        }
        // Set the first target
        aiDestinationSetter.target = queue[0];
    }
    void Update()
    {
        movement.x = aiPath.desiredVelocity.x;
        movement.y = aiPath.desiredVelocity.y;

        if (aiPath.desiredVelocity.sqrMagnitude > 0.2f)
        {
            speed = 1.0f;
        }
        else
        {
            speed = 0f;
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", speed);

        bodyAnimator.SetFloat("Horizontal", movement.x);
        bodyAnimator.SetFloat("Vertical", movement.y);
        bodyAnimator.SetFloat("Speed", speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            if (collision.gameObject.transform == queue[currentTarget])
            {
                queue.RemoveAt(currentTarget);
                currentTarget = Random.Range(0, queue.Count);
                aiDestinationSetter.target = queue[currentTarget];
            }
        }
    }

}
