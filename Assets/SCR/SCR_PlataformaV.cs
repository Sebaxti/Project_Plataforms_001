using Unity.VisualScripting;
using UnityEngine;

public class SCR_PlataformaV : MonoBehaviour
{
    public float velocidad;
    public bool vaViene;
    public GameObject pointA, pointB;

    private bool enPausa = false;
    private float tiempoEspera = 2f;
    private float tiempoTranscurrido = 0f;
    private float distanciaMinima = 0.1f;

    void Start()
    {
        vaViene = true;
    }

    void Update()
    {
        // Si está en pausa, contar el tiempo
        if (enPausa)
        {
            tiempoTranscurrido += Time.deltaTime;

            // Cuando pasen 2 segundos, continuar movimiento
            if (tiempoTranscurrido >= tiempoEspera)
            {
                enPausa = false;
                tiempoTranscurrido = 0f;
            }
            return; // No mover mientras está en pausa
        }

        // Movimiento normal
        if (vaViene)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.transform.position, velocidad * Time.deltaTime);

            // Verificar si llegó a pointA
            if (Vector3.Distance(transform.position, pointA.transform.position) < distanciaMinima)
            {
                transform.position = pointA.transform.position; 
                vaViene = false;
                enPausa = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.transform.position, velocidad * Time.deltaTime);

            // Verificar si llegó a pointB
            if (Vector3.Distance(transform.position, pointB.transform.position) < distanciaMinima)
            {
                transform.position = pointB.transform.position;
                vaViene = true;
                enPausa = true;
            }
        }
    }
}
