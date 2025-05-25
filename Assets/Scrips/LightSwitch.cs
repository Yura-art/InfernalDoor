using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public GameObject lightObject; 

    public void Interact(GameObject player)
    {
        if (lightObject != null)
            lightObject.SetActive(!lightObject.activeSelf);
    }
}