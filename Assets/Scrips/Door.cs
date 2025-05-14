using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private bool isOpen = false;

    public void Interact()
    {
        if (isOpen)
            transform.rotation = Quaternion.Euler(0, 0, 0); 
        else
            transform.rotation = Quaternion.Euler(0, -90, 0); 

        isOpen = !isOpen;
    }
}
