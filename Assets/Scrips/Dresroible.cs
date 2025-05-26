using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Explosion : MonoBehaviour, IDestroy
{
    [SerializeField] private GameObject objetoOriginal;
    [SerializeField] private GameObject objetoFracturado;

    [SerializeField] private float fuerzaMin = 5f;
    [SerializeField] private float fuerzaMax = 100f;
    [SerializeField] private float radioExplocion = 10f;

    public void Destroy()
    {
        if (objetoOriginal != null && objetoFracturado != null)
        {
            objetoOriginal.SetActive(false);

            GameObject fragmentos = Instantiate(objetoFracturado, transform.position, transform.rotation);

            foreach (Transform fragmento in fragmentos.transform)
            {
                Rigidbody rb = fragmento.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    float fuerza = Random.Range(fuerzaMin, fuerzaMax);
                    rb.AddExplosionForce(fuerza, transform.position, radioExplocion);
                }
            }

            Destroy(fragmentos, 2f);
        }
    }
}