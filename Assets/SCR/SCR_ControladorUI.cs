using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_UIManager : MonoBehaviour
{
    public static SCR_UIManager instancia;

    /*----public----*/
    public GameObject panelGameOver;
    public GameObject panelPausa;
    public string nombreEscenaMenu = "MainMenu"; // Nombre de tu escena de menú

    /*----private----*/
    private bool juegoPausado = false;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //paneles desactivados al inicio
        if (panelGameOver != null) panelGameOver.SetActive(false);
        if (panelPausa != null) panelPausa.SetActive(false);

        // Asegura que el juego esté corriendo
        Time.timeScale = 1f;
        juegoPausado = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //tecla ESC para pausar/despausar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    // Mostrar Game Over
    public void MostrarGameOver()
    {
        Debug.Log("Mostrando Game Over");
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }

        // Pausa el juego
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Pausar el juego
    public void PausarJuego()
    {
        Debug.Log("Juego pausado");
        juegoPausado = true;

        if (panelPausa != null)
        {
            panelPausa.SetActive(true);
        }

        // Pausa el juego
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Reanudar el juego
    public void ReanudarJuego()
    {
        Debug.Log("Juego reanudado");
        juegoPausado = false;

        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }

        // Reanuda el juego
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Reintentar (recargar escena actual)
    public void Reintentar()
    {
        Debug.Log("Reintentando nivel...");

        // Reanuda el tiempo
        Time.timeScale = 1f;

        // Resetea el GameManager
        if (SCR_ControladorJuego.instancia != null)
        {
            SCR_ControladorJuego.instancia.monedas = 0;
            SCR_ControladorJuego.instancia.vidas = 3;
        }

        // Recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Volver al menú principal
    public void VolverAlMenu()
    {
        Debug.Log("Volviendo al menú principal...");

        // Reanuda el tiempo
        Time.timeScale = 1f;

        // Carga el menú principal
        SceneManager.LoadScene(nombreEscenaMenu);
    }

    // Salir del juego
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}