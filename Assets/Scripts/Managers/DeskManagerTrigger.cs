using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskManagerTrigger : MonoBehaviour
{
    public ClickDeskScript clickDeskScript;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Manager"))
        {
            clickDeskScript.StartClickDelay();

        }
    }
}
