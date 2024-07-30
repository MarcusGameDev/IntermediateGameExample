using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventPublisher : MonoBehaviour
{
    public event EventHandler onSpacePressed;
    public event EventHandler onKeyCollected;
    public event EventHandler onInteractE;

    private void Start()
    {
        onSpacePressed += Testing_OnSpacePressed;
    }

    //Example Function
    void Testing_OnSpacePressed(object sender, EventArgs e)
    {
        Debug.Log("Space Pressed!");
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            onSpacePressed?.Invoke(this, EventArgs.Empty);
          
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            onInteractE?.Invoke(this, EventArgs.Empty);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            onKeyCollected?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
        }
    }


}
