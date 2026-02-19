using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ResetGameButton : MonoBehaviour
{
    [Header("Opciones")]
    public bool resetPlayerPrefs = true;
    public bool resetSaveFile = true;

    [Header("Escena a recargar")]
    public string mainSceneName = "MainScene";

    public void ResetGame()
    {
        Debug.Log("🔁 Reiniciando juego...");

        if (resetPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        if (resetSaveFile)
        {
            string path = Path.Combine(Application.persistentDataPath, "save.json");

            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("🗑 Archivo de guardado eliminado");
            }
        }

        SceneManager.LoadScene(mainSceneName);
    }
}
