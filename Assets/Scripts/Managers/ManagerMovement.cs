using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ManagerMovement : MonoBehaviour
{
    public int id = 0;
    public float moveSpeed = 5f;

    public AIPath aiPath;
    public AIDestinationSetter aiDestinationSetter;

    private Vector2 movement;
    private float speed;

    public Animator animator;
    public Animator bodyAnimator;

    // Targets for the AI to follow

    void Start()
    {

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

    public void setManagerTarget(Transform target)
    {
        aiDestinationSetter.target = target;
    }

}
