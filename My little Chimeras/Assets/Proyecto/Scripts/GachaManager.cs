//using UnityEngine;
//using UnityEngine.UI;

//public class GachaManager : MonoBehaviour
//{
//    [Header("Gacha Config")]
//    public int pullCost = 5;

//    [Header("References")]
//    public HappinessGenerator happinessGenerator;
//    public Button pullButton;

//    void Start()
//    {
//        UpdateButtonState();
//    }

//    public void PullGacha()
//    {
//        if (happinessGenerator == null) return;

//        bool success = happinessGenerator.SpendStars(pullCost);

//        if (!success)
//        {
//            Debug.Log("❌ No hay suficientes estrellas");
//            return;
//        }

//        Debug.Log("🎉 Tirada de Gacha!");

//        // Aquí va tu lógica real de gacha
//        DoGachaPull();

//        UpdateButtonState();
//    }

//    void DoGachaPull()
//    {
//        // Ejemplo simple
//        Debug.Log("✨ Objeto obtenido!");
//    }

//    void UpdateButtonState()
//    {
//        if (pullButton != null)
//        {
//            pullButton.interactable =
//                happinessGenerator.GetStars() >= pullCost;
//        }
//    }
//}

using UnityEngine;
using UnityEngine.UI;

public class GachaManager : MonoBehaviour
{
    public int pullCost = 5;
    public Button pullButton;
    public GachaSystem gachassystemm;

    void Start()
    {
        UpdateButton();
    }
    
    public void PullGacha()
    {
        Debug.Log(CurrencyManager.Instance.GetStars());

        if (!CurrencyManager.Instance.SpendStars(pullCost))
        {
            Debug.Log("❌ No hay suficientes estrellas");
            return;
        }

        Debug.Log("🎉 Tirada de Gacha");
        DoGachaPull();

        UpdateButton();
    }

    void DoGachaPull()
    {
        gachassystemm.PullFiveFromButton();
        Debug.Log("✨ Recompensa obtenida");
    }

    void UpdateButton()
    {
        if (CurrencyManager.Instance != null) { 
        pullButton.interactable =
            CurrencyManager.Instance.GetStars() >= pullCost;        
        }


    }
}
