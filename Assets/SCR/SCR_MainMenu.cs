using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_MainMenu : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelOpciones;

    void Start()
    {
        if (panelMenu != null) panelMenu.SetActive(true);
        if (panelOpciones != null) panelOpciones.SetActive(false);
    }

    public void IniciarJuego()
    {
        Debug.Log("Iniciando juego...");
        if (SCR_ControladorJuego.instancia != null)
        {
            SCR_ControladorJuego.instancia.ResetearJuego();
        }
        SceneManager.LoadScene("level001");
    }

    public void AbrirOpciones()
    {
        Debug.Log("Abriendo opciones...");
        if (panelMenu != null) panelMenu.SetActive(false);
        if (panelOpciones != null) panelOpciones.SetActive(true);
    }

    public void CerrarOpciones()
    {
        Debug.Log("Cerrando opciones...");
        if (panelMenu != null) panelMenu.SetActive(true);
        if (panelOpciones != null) panelOpciones.SetActive(false);
    }

    public void VolverAlMenu()
    {
        Debug.Log("Volviendo al menú principal...");

        // Destruye el GameManager al volver al menú
        if (SCR_ControladorJuego.instancia != null)
        {
            Destroy(SCR_ControladorJuego.instancia.gameObject);
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}