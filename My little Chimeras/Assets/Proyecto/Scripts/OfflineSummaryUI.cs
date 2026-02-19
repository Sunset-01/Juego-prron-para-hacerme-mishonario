using UnityEngine;
using TMPro;

public class OfflineSummaryUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI starsText;
    public TextMeshProUGUI happinessText;

    public void ShowSummary(float secondsAway, int starsGained, float happiness)
    {
        panel.SetActive(true);

        System.TimeSpan time = System.TimeSpan.FromSeconds(secondsAway);
        timeText.text = $"Tiempo fuera: {time.Hours}h {time.Minutes}m";

        starsText.text = $"Estrellas ganadas: +{starsGained}";
        happinessText.text = $"Felicidad actual: {Mathf.RoundToInt(happiness)}%";
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}
