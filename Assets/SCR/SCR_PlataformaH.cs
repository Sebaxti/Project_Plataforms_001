using Unity.VisualScripting;
using UnityEngine;

public class SCR_PlataformaH : MonoBehaviour
{
    public float velocidad;
    public bool vaViene;
    public GameObject pointA, pointB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vaViene = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoPlataforma();
    }

    void MovimientoPlataforma()
    {
        if (vaViene)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.transform.position, velocidad * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.transform.position, velocidad * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coordenadas")) 
        {
            vaViene= !vaViene;
        }
    }

}

