using UnityEngine;

public class SCR_CamaraFollow : MonoBehaviour
{
    /*----public----*/
    public Transform objetivo; // El Player
    public Vector3 offset = new Vector3(0, 5, -10); // Distancia relativa al jugador
    public float suavizado = 5f; // Qué tan suave sigue (mayor = más rápido)
    public bool seguirRotacion = false; // Para Crash-style, esto debe ser FALSE

    /*----private----*/
    private Vector3 velocidadActual; // Para SmoothDamp

    void LateUpdate()
    {
        // LateUpdate se ejecuta DESPUÉS del Update del jugador
        // Así la cámara siempre tiene la posición actualizada del Player

        if (objetivo == null)
        {
            Debug.LogWarning("¡No hay objetivo asignado a la cámara!");
            return;
        }

        // Calcula la posición deseada
        Vector3 posicionDeseada = objetivo.position + offset;

        // Mueve suavemente la cámara hacia esa posición
        transform.position = Vector3.SmoothDamp(
            transform.position,      // Posición actual
            posicionDeseada,         // Posición objetivo
            ref velocidadActual,     // Velocidad (se calcula automáticamente)
            1f / suavizado);          // Tiempo de suavizado


        // La rotación se mantiene fija (no sigue al jugador)
        // Si quisieras que mirara al jugador, usarías:
        // transform.LookAt(objetivo);
    }
}