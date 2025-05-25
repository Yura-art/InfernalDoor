using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pet : MonoBehaviour, IInteractable
{
    Transform jugador;
    Vector3 ultimaPos;
    bool siguiendo;

    public float velocidad = 3f;
    public float distanciaMinima = 0.2f;
   

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

        bool seMueve = Vector3.Distance(jugador.position, ultimaPos) > 0.01f;
        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (seMueve && distancia > distanciaMinima)
            transform.position = Vector3.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);

        ultimaPos = jugador.position;
    }
    
}


