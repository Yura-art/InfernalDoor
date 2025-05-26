using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pet : MonoBehaviour, IInteractable, IDestroy
{
    Transform jugador;
    Vector3 ultimaPos;
    [SerializeField] bool siguiendo;

    public float velocidad = 3f;
    public float distanciaMinima = 0.2f;

    [SerializeField] private GameObject objetoOriginal;
    [SerializeField] private GameObject objetoFracturado;

    [SerializeField] private float fuerzaMin = 5f;
    [SerializeField] private float fuerzaMax = 100f;
    [SerializeField] private float radioExplocion = 10f;


    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (jugador == null) Debug.LogWarning("Falta el tag 'Player' en el jugador.");
        ultimaPos = jugador.position;
    }
    public void Interact(GameObject player) => siguiendo = !siguiendo;
 
    

    private void Update()
    {
        if (!siguiendo || jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia > distanciaMinima)
            transform.position = Vector3.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);

        ultimaPos = jugador.position;
    }

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


