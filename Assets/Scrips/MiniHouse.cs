using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniHouse : MonoBehaviour, ICambiable
{
    // Este atributo se muestra en el inspector de Unity con un encabezado "Escala aumentada"
    [Header("Escala aumentada")]
    // Variable privada que guarda la escala a la que se aumentará la casa (por defecto 2x en cada eje)
    [SerializeField] private Vector3 escalaAumentada = new Vector3(2f, 2f, 2f);

    // Variable privada para guardar la escala original del objeto al iniciar el juego
    private Vector3 escalaOriginal;

    // Variable estática (compartida por todas las instancias) que indica si el objeto está en escala alternativa (aumentada)
    private static bool esAlternativa = false;

    // Variable estática para saber si hay un dedo en la pantalla (usada para controlar toques)
    private static bool unDedoEnPantalla = false;

    void Start()
    {
        // Al iniciar, guardamos la escala original del objeto para poder revertir luego
        escalaOriginal = transform.localScale;
    }

    void Update()
    {
        // Si hay un solo dedo tocando la pantalla (para aumentar escala)
        if (Input.touchCount == 1)
        {
            // Tomamos el primer toque
            Touch t = Input.GetTouch(0);

            // Si el toque acaba de comenzar
            if (t.phase == TouchPhase.Began)
            {
                // Indicamos que hay un dedo en pantalla
                unDedoEnPantalla = true;

                // Revisamos si el toque fue sobre este objeto, sin pedir revertir
                RevisarToque(t.position, false);
            }
        }
        // Si hay dos dedos tocando la pantalla (para revertir escala)
        else if (Input.touchCount == 2)
        {
            // Tomamos el segundo toque
            Touch segundo = Input.GetTouch(1);

            // Si el segundo toque acaba de comenzar
            if (segundo.phase == TouchPhase.Began)
            {
                // Revisamos si el toque fue sobre este objeto, pidiendo revertir la escala
                RevisarToque(segundo.position, true);
            }
        }
        // Si no hay dedos tocando la pantalla
        else if (Input.touchCount == 0)
        {
            // Indicamos que no hay dedo en pantalla
            unDedoEnPantalla = false;
        }
    }

    // Método privado para revisar si el toque fue sobre este objeto y decidir si cambiar o revertir escala
    private void RevisarToque(Vector2 posicionPantalla, bool revertir)
    {
        // Creamos un rayo desde la cámara hacia la posición del toque en pantalla
        Ray ray = Camera.main.ScreenPointToRay(posicionPantalla);

        // Si el rayo golpea algún collider en el mundo
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            // Si el objeto golpeado es este mismo gameObject
            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                // Si se pidió revertir, y hay un dedo en pantalla, y ya está en escala alternativa, revertimos
                if (revertir && unDedoEnPantalla && esAlternativa)
                    Revertir();
                // Si no se pidió revertir, y no está en escala alternativa, entonces cambiamos a escala aumentada
                else if (!revertir && !esAlternativa)
                    Cambiar();
            }
        }
    }

    // Método público que cambia la escala al valor aumentado y marca que está en estado alternativo
    public void Cambiar()
    {
        transform.localScale = escalaAumentada;
        esAlternativa = true;
    }

    // Método público que revierte la escala a la original y marca que ya no está en estado alternativo
    public void Revertir()
    {
        transform.localScale = escalaOriginal;
        esAlternativa = false;
    }
}
