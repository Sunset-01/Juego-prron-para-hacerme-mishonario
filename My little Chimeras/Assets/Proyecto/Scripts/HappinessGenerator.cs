//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using System.Collections;

//public class HappinessGenerator : MonoBehaviour
//{
//    [Header("Happiness Settings")]
//    public float happinessPerTick = 5f;
//    public float interval = 1f;

//    [Header("UI")]
//    public Slider happinessSlider;
//    public TextMeshProUGUI starsText;

//    private int stars = 0;
//    private float currentHappiness = 0f;

//    void Start()
//    {
//        happinessSlider.value = 0f;
//        UpdateStarsText();
//        StartCoroutine(GenerateHappiness());
//    }

//    IEnumerator GenerateHappiness()
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(interval);
//            AddHappiness(happinessPerTick);
//        }
//    }

//    void AddHappiness(float amount)
//    {
//        currentHappiness += amount;
//        happinessSlider.value = currentHappiness;

//        if (currentHappiness >= happinessSlider.maxValue)
//        {
//            GainStar();
//        }
//    }

//    void GainStar()
//    {
//        stars++;
//        currentHappiness = 0f;
//        happinessSlider.value = 0f;
//        UpdateStarsText();
//    }

//    void UpdateStarsText()
//    {
//        starsText.text = "Estrellas: " + stars;
//    }
//}
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HappinessGenerator : MonoBehaviour
{
    public int GetStars() => stars;

    public bool SpendStars(int amount)
    {
        if (stars < amount)
            return false;

        stars -= amount;
        UpdateStarUI();
        return true;
    }
    public void SetStars(int value)
    {
        stars = value;
        UpdateStarsText();
    }


    [Header("Base Happiness")]
    public float baseHappinessPerTick = 5f;
    public float interval = 1f;

    [Header("UI")]
    public Slider happinessSlider;
    public TextMeshProUGUI starsText;

    [Header("References")]
    public CharacterStatus characterStatus;

    [SerializeField] Text starText;

    void UpdateStarUI()
    {
        if (starText != null)
            starText.text = stars.ToString();
    }


    public float GetCurrentHappiness()
    {
        return currentHappiness;
    }

    public void SetCurrentHappiness(float value)
    {
        currentHappiness = Mathf.Clamp(value, 0, happinessSlider.maxValue);
        happinessSlider.value = currentHappiness;
    }


    private float currentHappiness = 0f;
    private int stars = 0;

    void Start()
    {
        happinessSlider.value = 0;
        UpdateStarsText();
        StartCoroutine(GenerateHappiness());
    }

    IEnumerator GenerateHappiness()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            float multiplier = characterStatus.GetHappinessMultiplier();
            float finalHappiness = baseHappinessPerTick * multiplier;

            AddHappiness(finalHappiness);
        }
    }

    void AddHappiness(float amount)
    {
        if (amount <= 0) return;

        currentHappiness += amount;
        happinessSlider.value = currentHappiness;

        if (currentHappiness >= happinessSlider.maxValue)
        {
            GainStar();
        }
    }

    void GainStar()
    {
        stars++;
        CurrencyManager.Instance.AddStars(1);
        currentHappiness = 0;
        happinessSlider.value = 0;
        UpdateStarsText();
        FindObjectOfType<SaveManager>().SaveGame();

    }

    void UpdateStarsText()
    {
        Debug.Log(stars);
        starsText.text = "Estrellas: " + stars;
    }

    public void AddOfflineHappiness(float amount)
    {
        currentHappiness += amount;

        while (currentHappiness >= happinessSlider.maxValue)
        {
            currentHappiness -= happinessSlider.maxValue;
            stars++;
        }

        happinessSlider.value = currentHappiness;
        UpdateStarsText();
    }

}
