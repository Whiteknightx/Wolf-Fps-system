using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator door = null;
    [SerializeField] private bool onTrigger = false;
    [SerializeField] private bool offTrigger = true;

    // Update is called once per frame
    void Update()
    {
        if (onTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (door.GetBool("isOpen"))
            {
                door.SetBool("isOpen", false);
                Debug.Log("Closing the gate.");
            }
            else
            {
                door.SetBool("isOpen", true);
                Debug.Log("Opening the gate.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onTrigger = true;
            offTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onTrigger = false;
            offTrigger = true;
        }
    }
}
