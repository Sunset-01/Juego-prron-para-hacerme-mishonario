using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    [SerializeField] int stars;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // --- API PÚBLICA ---

    public int GetStars()
    {
        return stars;
    }

    public void AddStars(int amount)
    {
        stars += amount;
        SaveManager.Instance.SaveGame();
    }

    public bool SpendStars(int amount)
    {
        if (stars < amount)
            return false;

        stars -= amount;
        SaveManager.Instance.SaveGame();
        return true;
    }

    // --- CARGA DESDE SAVE ---

    public void SetStars(int value)
    {
        stars = value;
    }
}
