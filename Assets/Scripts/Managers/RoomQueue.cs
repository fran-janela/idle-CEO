using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomQueue : MonoBehaviour
{
    // List with desks trigger tranform
    private List<Transform> deskTriggers;
    public ManagerMovement Manager;

    public Transform ManagerIdleTarget;

    private Transform CurrentManagerTarget;

    void Start()
    {
        deskTriggers = new List<Transform>();
        CurrentManagerTarget = ManagerIdleTarget;
    }

    void Update()
    {
        if (Manager.aiPath.reachedDestination && CurrentManagerTarget != ManagerIdleTarget){
            Debug.Log("Manager reached destination");
            SetNextManagerTarget();
        } else if (CurrentManagerTarget == ManagerIdleTarget && deskTriggers.Count > 0){
            Debug.Log("Manager is idle and there are desks in queue");
            SetNextManagerTarget();
        } else {
            Debug.Log("Manager is not idle and its not moving to idle target");
        }
    }

    public bool CheckIfTriggerInQueue(Transform trigger)
    {
        // If the trigger is in the queue, return true
        if (deskTriggers.Contains(trigger))
        {
            return true;
        } else {
            return false;
        }
    }

    public void AddDeskTrigger(Transform deskTrigger)
    {
        deskTriggers.Add(deskTrigger);
    }

    public void RemoveDeskTrigger(Transform deskTrigger)
    {
        deskTriggers.Remove(deskTrigger);
    }

    public void SetNextManagerTarget()
    {
        if (deskTriggers.Count > 0)
        {
            CurrentManagerTarget = deskTriggers[0];
            Manager.setManagerTarget(deskTriggers[0]);
            deskTriggers.RemoveAt(0);
        } else {
            CurrentManagerTarget = ManagerIdleTarget;
            Manager.setManagerTarget(ManagerIdleTarget);
        }
    }
}
