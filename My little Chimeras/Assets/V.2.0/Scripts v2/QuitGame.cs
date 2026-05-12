using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");

        Application.Quit();

        // Esto solo funciona en el editor de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}