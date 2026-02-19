using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
    [Header("References")]
    public PlayerSwitchManager playerManager;
    public Image[] characterIcons;

    [Header("Visuals")]
    public Color activeColor = Color.white;
    public Color inactiveColor = new Color(1f, 1f, 1f, 0.3f);
    public float activeScale = 1.2f;

    void Start()
    {
        UpdateUI(playerManager.activeIndex);
    }

    void Update()
    {
        UpdateUI(playerManager.activeIndex);
    }

    void UpdateUI(int activeIndex)
    {
        for (int i = 0; i < characterIcons.Length; i++)
        {
            if (i == activeIndex)
            {
                characterIcons[i].color = activeColor;
                characterIcons[i].transform.localScale = Vector3.one * activeScale;
            }
            else
            {
                characterIcons[i].color = inactiveColor;
                characterIcons[i].transform.localScale = Vector3.one;
            }
        }
    }
}
