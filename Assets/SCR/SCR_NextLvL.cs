using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_TriggerNivel : MonoBehaviour
{
    /*----public----*/
    public string nombreEscenaSiguiente; // El nombre de la siguiente escena
    public int indiceEscenaSiguiente = -1; // O usa el índice (-1 = usa nombre)
    public bool cargarSiguienteAutomatico = false; // Carga la escena siguiente en el Build

    /*----private----*/
    private bool yaActivado = false; // Para que solo se active una vez

    void OnTriggerEnter(Collider other)
    {
        // Verifica que sea el jugador y que no se haya activado antes
        if (other.CompareTag("Player") && !yaActivado)
        {
            yaActivado = true;
            CargarSiguienteNivel();
        }
    }

    void CargarSiguienteNivel()
    {
        Debug.Log("¡Nivel completado! Cargando siguiente nivel...");

        // Opción 1: Cargar por nombre
        if (!string.IsNullOrEmpty(nombreEscenaSiguiente))
        {
            SceneManager.LoadScene(nombreEscenaSiguiente);
        }
        // Opción 2: Cargar por índice
        else if (indiceEscenaSiguiente >= 0)
        {
            SceneManager.LoadScene(indiceEscenaSiguiente);
        }
        // Opción 3: Cargar la siguiente escena automáticamente
        else if (cargarSiguienteAutomatico)
        {
            int escenaActual = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(escenaActual + 1);
        }
        else
        {
            Debug.LogWarning("¡No hay escena configurada para cargar!");
        }
    }
}