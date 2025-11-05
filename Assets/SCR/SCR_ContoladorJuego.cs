using UnityEngine;
using TMPro; // Para TextMeshPro

public class SCR_ControladorJuego : MonoBehaviour
{
    public static SCR_ControladorJuego instancia;

    /*----public----*/
    public int valorEnemigo;
    public int valorMonedas;
    public int monedas = 0;
    public int vidas = 3;
    public int puntosParaVida = 100; // Cuántos puntos necesitas para ganar una vida
    public int puntosAlMatarEnemigo = 10; // Puntos que da cada enemigo

    // Referencias a los textos del UI
    public TextMeshProUGUI textoMonedas;
    public TextMeshProUGUI textoVidas;

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ActualizarUI();
    }

    // Método para recoger monedas
    public void RecogerMoneda(int i)
    {
        monedas+=i;
        Debug.Log("Monedas: " + monedas);

        if (monedas >= puntosParaVida)
        {
            GanarVida();
            monedas = 0; // Resetea el contador
        }
        ActualizarUI();
    }

    // Método para ganar una vida
    public void GanarVida()
    {
        vidas++;
        Debug.Log("¡Vida extra! Vidas: " + vidas);
        ActualizarUI();
    }

    // Método para perder una vida
    public void PerderVida()
    {
        vidas--;
        Debug.Log("Perdiste una vida. Vidas restantes: " + vidas);

        if (vidas <= 0)
        {
            GameOver();
        }

        ActualizarUI();
    }

    // Método para actualizar los textos del UI
    void ActualizarUI()
    {
        if (textoMonedas != null)
            textoMonedas.text = "Monedas: " + monedas;

        if (textoVidas != null)
            textoVidas.text = "Vidas: " + vidas;
    }

    public void ResetearJuego()
    {
        monedas = 0;
        vidas = 3;
        ActualizarUI();
    }

    // Método para cuando se acaban las vidas
    void GameOver()
    {
        Debug.Log("GAME OVER");
        // Aquí puedes cargar una escena de Game Over
        if (SCR_UIManager.instancia != null)
        {
            SCR_UIManager.instancia.MostrarGameOver();
        }
    }
}