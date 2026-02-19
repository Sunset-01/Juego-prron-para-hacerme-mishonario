using UnityEngine;
using TMPro;

public class ActivitySummaryUI : MonoBehaviour
{
    public GameObject panel;

    public TextMeshProUGUI happinessText;
    public TextMeshProUGUI funText;
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI energyText;

    public void Show(float happiness, float fun, float hunger, float energy)
    {
        panel.SetActive(true);

        happinessText.text = $"+{happiness} Felicidad";
        funText.text = $"+{fun} Diversión";
        hungerText.text = $"-{hunger} Hambre";
        energyText.text = $"-{energy} Energía";
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}
