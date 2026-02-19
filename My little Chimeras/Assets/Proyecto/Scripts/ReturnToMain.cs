using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMain : MonoBehaviour
{
    void Start()
    {
        ActivityManager.Instance.SetLastScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void Return()
    {
        SceneManager.LoadScene("MainScene");
    }
}
