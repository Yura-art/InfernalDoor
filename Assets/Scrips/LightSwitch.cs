using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public GameObject lightObject; // Asigna aquí el objeto completo que tiene la luz

    public void Interact()
    {
        if (lightObject != null)
            lightObject.SetActive(!lightObject.activeSelf);
    }
}