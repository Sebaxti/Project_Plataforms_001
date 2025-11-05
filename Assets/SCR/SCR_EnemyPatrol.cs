using UnityEngine;

public class SCR_ : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad, fuerzaRebote;

    private Transform objetivoActual;
    private bool vaViene = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objetivoActual = puntoB;
    }

    // Update is called once per frame
    void Update()
    {
        MoverEnPatrulla();
    }

    void MoverEnPatrulla() 
    {
        transform.position = Vector3.MoveTowards(transform.position, objetivoActual.position, velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, objetivoActual.position) < 0.1f)
        {
            CambiarDireccion();
        }
    }
    void CambiarDireccion()
    {
        // Alterna entre punto A y punto B
        if (vaViene)
        {
            objetivoActual = puntoA;
            vaViene = false;
        }
        else
        {
            objetivoActual = puntoB;
            vaViene = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡El enemigo mató al jugador!");
            SCR_CheckPointControlador.Instancia.RespawnearJugador(collision.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Saltaste encima del enemigo!");

            Rigidbody rbJugador = other.GetComponent<Rigidbody>();
            if (rbJugador != null)
            {
                rbJugador.linearVelocity = new Vector3(rbJugador.linearVelocity.x, 0, rbJugador.linearVelocity.z);
                rbJugador.AddForce(Vector3.up * fuerzaRebote, ForceMode.Impulse);
            }

            if (SCR_ControladorJuego.instancia != null)
            {
                SCR_ControladorJuego.instancia.RecogerMoneda(SCR_ControladorJuego.instancia.valorMonedas+SCR_ControladorJuego.instancia.valorEnemigo);
            }

            Destroy(gameObject);
        }
    }

}
