using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
           
            Collider[] colliders = Physics.OverlapSphere(transform.position, interactDistance);

            foreach (Collider col in colliders)
            {
                IInteractable interactable = col.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                    return; 
                }
            }
        }
    }
}

