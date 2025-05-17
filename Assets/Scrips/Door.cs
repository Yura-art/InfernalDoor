using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public float rotationAngle = -90f;           // �ngulo negativo para abrir hacia adentro
    public float rotationSpeed = 90f;            // Grados por segundo
    public Vector3 rotationAxis = Vector3.up;    // Eje de rotaci�n

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;
    private Coroutine currentCoroutine;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.AngleAxis(rotationAngle, rotationAxis) * closedRotation;
    }

    public void Interact()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        isOpen = !isOpen;
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;

        currentCoroutine = StartCoroutine(RotateTo(targetRotation));
    }

    private IEnumerator RotateTo(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = targetRotation;
        currentCoroutine = null;
    }
}
