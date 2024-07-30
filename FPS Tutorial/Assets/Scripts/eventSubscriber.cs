using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventSubscriber : MonoBehaviour
{
    eventPublisher EP;

    // Start is called before the first frame update
    void Start()
    {
      // EP = GetComponent<eventPublisher>(); //This is if it is on the same GameObject;
      EP = FindObjectOfType<eventPublisher>();

      EP.onSpacePressed += TestingEvents_OnSpacePressed;
        EP.onKeyCollected += DoorOpen;
    }

    void TestingEvents_OnSpacePressed(object sender, EventArgs e)
    {
        Debug.Log("Space Pressed! from another script!");

      
        EP.onSpacePressed -= TestingEvents_OnSpacePressed;
    }

    void DoorOpen(object sender, EventArgs e)
    {
        // Door Opens
        gameObject.transform.position += new Vector3(0, 10, 0);
    }

    void DoorClose(object sender, EventArgs e)
    {
        // Door Opens
        gameObject.transform.position += new Vector3(0, -20, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            EP = other.gameObject.GetComponent<eventPublisher>();
            EP.onInteractE += DoorClose;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EP = other.gameObject.GetComponent<eventPublisher>();
            EP.onInteractE -= DoorClose;
        }
    }
}
