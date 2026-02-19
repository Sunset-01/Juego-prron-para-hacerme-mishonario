using UnityEngine;
using System.IO;
using static CharacterStatus;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [Header("Offline Progress")]
    public float maxOfflineHours = 8f;

    public CharacterStatus characterStatus;
    public HappinessGenerator happinessGenerator;

    public OfflineSummaryUI offlineSummaryUI;


    string path;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        DontDestroyOnLoad(gameObject);
        path = Application.persistentDataPath + "/save.json";
        LoadGame();
    }


    public void SaveGame()
    {
        CharacterData data = new CharacterData
        {
            hunger = characterStatus.hunger,
            energy = characterStatus.energy,
            fun = characterStatus.fun,
            currentHappiness = happinessGenerator.GetCurrentHappiness(),
            stars = happinessGenerator.GetStars(),
            lastSaveTime = System.DateTime.UtcNow.ToString()
        };
        data.stars = CurrencyManager.Instance.GetStars();

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }



    public void LoadGame()
    {
        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);
        CharacterData data = JsonUtility.FromJson<CharacterData>(json);

        characterStatus.hunger = data.hunger;
        characterStatus.energy = data.energy;
        characterStatus.fun = data.fun;

        happinessGenerator.SetStars(data.stars);
        happinessGenerator.SetCurrentHappiness(data.currentHappiness); // NUEVO
        CurrencyManager.Instance.SetStars(data.stars);

        ApplyOfflineProgress(data);

    }

    void ApplyOfflineProgress(CharacterData data)
    {
        if (string.IsNullOrEmpty(data.lastSaveTime))
        {
            // No hay progreso offline que calcular
            return;
        }

        System.DateTime lastTime;
        if (!System.DateTime.TryParse(data.lastSaveTime, out lastTime))
        {
            Debug.LogWarning("Formato de fecha inválido en el guardado.");
            return;
        }

        System.TimeSpan timeAway = System.DateTime.UtcNow - lastTime;

        float secondsAway = Mathf.Min(
            (float)timeAway.TotalSeconds,
            maxOfflineHours * 3600f
        );

        int starsBefore = happinessGenerator.GetStars();

        ApplyStatDecay(secondsAway);
        ApplyHappinessGeneration(secondsAway);

        int starsAfter = happinessGenerator.GetStars();
        int starsGained = starsAfter - starsBefore;

        if (starsGained > 0 && offlineSummaryUI != null)
        {
            offlineSummaryUI.ShowSummary(
                secondsAway,
                starsGained,
                happinessGenerator.GetCurrentHappiness()
            );
        }
    }




    void ApplyStatDecay(float secondsAway)
    {
        characterStatus.hunger = Mathf.Max(
            0,
            characterStatus.hunger - characterStatus.hungerDecay * secondsAway
        );

        characterStatus.energy = Mathf.Max(
            0,
            characterStatus.energy - characterStatus.energyDecay * secondsAway
        );

        characterStatus.fun = Mathf.Max(
            0,
            characterStatus.fun - characterStatus.funDecay * secondsAway
        );
    }

    void ApplyHappinessGeneration(float secondsAway)
{
    float interval = happinessGenerator.interval;
    int ticks = Mathf.FloorToInt(secondsAway / interval);

    for (int i = 0; i < ticks; i++)
    {
        float multiplier = characterStatus.GetHappinessMultiplier();
        if (multiplier <= 0) break;

        float happiness = happinessGenerator.baseHappinessPerTick * multiplier;
        happinessGenerator.AddOfflineHappiness(happiness);
    }
}



}
