using UnityEngine;

public class SCR_CamaraFollow : MonoBehaviour
{
    /*----public----*/
    public Transform objetivo;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float suavizado = 5f;
    public bool seguirRotacion = false;

    /*----private----*/
    private Vector3 velocidadActual;

    void LateUpdate()
    {
        if (objetivo == null)
        {
            Debug.LogWarning("¡No hay objetivo asignado a la cámara!");
            return;
        }


        Vector3 posicionDeseada = objetivo.position + offset;

     
        transform.position = Vector3.SmoothDamp(
            transform.position,      // Posición actual
            posicionDeseada,         // Posición objetivo
            ref velocidadActual,     // Velocidad (se calcula automáticamente)
            1f / suavizado);          // Tiempo de suavizado



        // transform.LookAt(objetivo);
    }
}