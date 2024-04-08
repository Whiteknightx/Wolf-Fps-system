using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : MonoBehaviour
{
    [SerializeField] private Animator door = null;
    [SerializeField] private bool onTrigger = false;
    [SerializeField] private bool offTrigger = false;
    [SerializeField] private string doorOpen = "gateOpen";
    [SerializeField] private string doorClosed = "gateOpen";

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           if (onTrigger)
            {
                door.Play(doorOpen,0,0.0f);
                gameObject.SetActive(false);
            }
           else if (offTrigger)
            {
                door.Play(doorClosed,0,0.0f);
                gameObject.SetActive(false);
            }
        }
    }

}
