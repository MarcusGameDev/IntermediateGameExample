using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    public Animator anim;
    public bool inRange = false;
    public bool isOpen = false;


    // Update is called once per frame
    void Update()
    {
        if(inRange == true && isOpen == false && Input.GetKeyDown(KeyCode.E))
        {
            // Open door
            anim.SetBool("IsOpen", true);
            isOpen = true;
        }
        else if (inRange == true && isOpen == true && Input.GetKeyDown(KeyCode.E))
        {
            // Close door
            anim.SetBool("IsOpen", false);
            isOpen = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          // InRange
          inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // InRange
            inRange = false;
        }
    }
}
