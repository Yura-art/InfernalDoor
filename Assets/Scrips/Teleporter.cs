using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable
{
   [SerializeField] GameObject portalDos;
    public void Interact(GameObject player)
    {
       player.transform.position = portalDos.transform.position;
    }

   
}
