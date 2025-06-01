using UnityEngine;

// Implementa interfaces para interacci�n, destrucci�n y cambio de forma
public class Pet : MonoBehaviour, IInteractable, IDestroy, ICambiable
{
    [Header("Movimiento")]
    [SerializeField] float velocidad = 3f;           // Velocidad a la que sigue al jugador
    [SerializeField] float distanciaMinima = 1.5f;   // Distancia m�nima para detenerse y no empujar al jugador
    Transform jugador;                               // Referencia al jugador
    bool siguiendo;                                  // Si est� siguiendo o no

    [Header("Transformaciones")]
    [SerializeField] GameObject formaAlternativa;    // Prefab de la forma alternativa
    [SerializeField] GameObject objetoOriginal;      // Prefab de la forma original (para volver a ella)
    [SerializeField] GameObject objetoFracturado;    // Prefab con f�sica para simular una explosi�n

    [Header("Explosi�n")]
    [SerializeField] float fuerzaMin = 300f;         // Fuerza m�nima de explosi�n
    [SerializeField] float fuerzaMax = 500f;         // Fuerza m�xima de explosi�n
    [SerializeField] float radioExplocion = 2f;      // Radio de efecto de la explosi�n

    static bool esAlternativo;                       // Indica si el objeto est� en forma alternativa

    void Start()
    {
        // Busca al jugador por su tag al iniciar
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        // Movimiento hacia el jugador si est� en modo seguimiento
        if (siguiendo && jugador != null)
        {
            float distancia = Vector3.Distance(transform.position, jugador.position);
            // Solo se mueve si est� m�s lejos que la distancia m�nima
            if (distancia > distanciaMinima)
                transform.position = Vector3.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);
        }

        // Detecta toques: uno para cambiar de forma, dos para volver a la forma original
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            DetectarToque(Input.GetTouch(0).position, false); // Cambio de forma
        else if (Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Began)
            DetectarToque(Input.GetTouch(1).position, true);  // Reversi�n
    }

    // Raycast para detectar si se toc� el pet
    void DetectarToque(Vector2 posicion, bool revertir)
    {
        Ray ray = Camera.main.ScreenPointToRay(posicion);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
        {
            if (revertir && esAlternativo) Revertir();       // Revertir a forma original
            else if (!revertir && !esAlternativo) Cambiar(); // Cambiar a forma alternativa
        }
    }

    // M�todo que cambia el estado de seguimiento
    public void Interact(GameObject player) => siguiendo = !siguiendo;

    // Instancia la forma alternativa y destruye la actual
    public void Cambiar()
    {
        if (formaAlternativa == null) return;

        GameObject nueva = Instantiate(formaAlternativa, transform.position, transform.rotation);
        nueva.transform.localScale = transform.localScale; // Mantiene el mismo tama�o

        // Copia el estado de seguimiento al nuevo objeto
        Pet nuevoPet = nueva.GetComponent<Pet>();
        if (nuevoPet != null)
        {
            nuevoPet.siguiendo = siguiendo;
        }

        esAlternativo = true; // Cambia estado global
        Destroy(gameObject);  // Destruye la forma actual
    }

    // Reinstancia la forma original
    public void Revertir()
    {
        if (objetoOriginal == null) return;

        GameObject original = Instantiate(objetoOriginal, transform.position, transform.rotation);
        original.transform.localScale = transform.localScale;

        // Copia el estado de seguimiento tambi�n
        Pet nuevoPet = original.GetComponent<Pet>();
        if (nuevoPet != null)
        {
            nuevoPet.siguiendo = siguiendo;
        }

        esAlternativo = false; // Ya no es forma alternativa
        Destroy(gameObject);   // Destruye la forma alternativa
    }

    // Simula una explosi�n con fragmentos f�sicos
    public void Destroy()
    {
        if (objetoFracturado == null) return;

        GameObject fragmentos = Instantiate(objetoFracturado, transform.position, transform.rotation);

        // A�ade fuerza de explosi�n a cada fragmento
        foreach (Transform f in fragmentos.transform)
        {
            Rigidbody rb = f.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(Random.Range(fuerzaMin, fuerzaMax), transform.position, radioExplocion);
        }

        Destroy(fragmentos, 2f); // Destruye fragmentos tras 2 segundos
        Destroy(gameObject);     // Destruye el pet actual
    }
}

