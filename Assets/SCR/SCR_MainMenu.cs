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