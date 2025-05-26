using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInteractor : MonoBehaviour
{
    public float interactDistance = 2f;
    public KeyCode interactKey = KeyCode.E;
    [SerializeField] private KeyCode destroyKey = KeyCode.X;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            InteractuarConObjetos();
        }

        if (Input.GetKeyDown(destroyKey))
        {
            DestruirObjetosCercanos();
        }
    }

    public void InteractuarConObjetos()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactDistance);

        foreach (Collider col in colliders)
        {
            IInteractable interactable = col.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(gameObject);
                return;
            }
        }
    }

    public void DestruirObjetosCercanos()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactDistance);

        foreach (Collider col in colliders)
        {
            IDestroy destructible = col.GetComponent<IDestroy>();
            if (destructible != null)
            {
                destructible.Destroy();
                return;
            }
        }
    }
}


