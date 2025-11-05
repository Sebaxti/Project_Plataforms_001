using UnityEngine;

public class SCR_Moneda : MonoBehaviour
{
    public float velocidadRotacion;
    public float velocidadBobbing;
    public float alturaBobbing;

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }
    void Update()
    {
        // Gira la moneda constantemente
        transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);

        float nuevaY = posicionInicial.y + Mathf.Sin(Time.time * velocidadBobbing) * alturaBobbing;
        transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);

}

void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Moneda recogida!");

            // Suma la moneda al GameManager
            if (SCR_ControladorJuego.instancia != null)
            {
                SCR_ControladorJuego.instancia.RecogerMoneda(SCR_ControladorJuego.instancia.valorMonedas);
            }

            // Destruye la moneda
            Destroy(gameObject);
        }
    }
}