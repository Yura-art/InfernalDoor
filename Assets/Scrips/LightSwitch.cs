using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public Light lightSource;

    public void Interact()
    {
        if (lightSource != null)
            lightSource.enabled = !lightSource.enabled;
    }
}