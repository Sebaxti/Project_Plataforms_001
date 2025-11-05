using UnityEngine;

public class SCR_CheckPointControlador : MonoBehaviour
{
    public static SCR_CheckPointControlador Instancia;
    public Transform puntoRespawnInicial;

    private Vector3 ultimoCheckPoint;

    private void Awake()
    {
        if (Instancia == null) 
        {
            Instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        if (puntoRespawnInicial!=null)
        {
            ultimoCheckPoint=puntoRespawnInicial.position;
        }
        
    }
    void Update()
    {
        
    }

    public void ActualizarCheckPoint(Vector3 nuevaPosicion)
    {
        ultimoCheckPoint = nuevaPosicion;
    }
    public Vector3 ObtenerPosicionRespawn()
    {
        return ultimoCheckPoint;
    }
    public void RespawnearJugador(GameObject jugador)
    {
        jugador.transform.position= ultimoCheckPoint;

        Rigidbody rb=jugador.GetComponent<Rigidbody>(); 
        if (rb != null)
        {
            rb.linearVelocity=Vector3.zero;
            rb.angularVelocity=Vector3.zero;
        }
        if (SCR_ControladorJuego.instancia != null) 
        {
            SCR_ControladorJuego.instancia.PerderVida();
        }
    }
}
