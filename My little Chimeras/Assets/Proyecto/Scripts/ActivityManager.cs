using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivityManager : MonoBehaviour
{
    public static ActivityManager Instance;

    public ActivityResult[] activities;

    [Header("References")]
    public CharacterStatus characterStatus;
    public HappinessGenerator happinessGenerator;
    public ActivitySummaryUI summaryUI;

    string lastScene;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetLastScene(string sceneName)
    {
        lastScene = sceneName;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainScene")
            return;

        if (string.IsNullOrEmpty(lastScene))
            return;

        ApplyActivityResult(lastScene);
        lastScene = null;
    }

    void ApplyActivityResult(string sceneName)
    {
        ActivityResult result = GetActivity(sceneName);
        if (result == null) return;

        characterStatus.hunger = Mathf.Max(0, characterStatus.hunger - result.hungerCost);
        characterStatus.energy = Mathf.Max(0, characterStatus.energy - result.energyCost);
        characterStatus.fun = Mathf.Min(100, characterStatus.fun + result.fun);

        happinessGenerator.AddOfflineHappiness(result.happiness);

        summaryUI.Show(
            result.happiness,
            result.fun,
            result.hungerCost,
            result.energyCost
        );
    }

    ActivityResult GetActivity(string sceneName)
    {
        foreach (var a in activities)
            if (a.sceneName == sceneName)
                return a;

        return null;
    }
}
