using UnityEngine;
using UnityEngine.UI;

public class StatImageUI : MonoBehaviour
{
    public CharacterStatus characterStatus;

    public enum StatType { Hunger, Energy, Fun }
    public StatType statType;

    [Header("Sprites (0 a 5)")]
    public Sprite[] levelSprites; // tamaño 6

    public Image statImage;

    void Update()
    {
        float value = GetStatValue();
        int level = GetLevel(value);
        statImage.sprite = levelSprites[level];
    }

    float GetStatValue()
    {
        return statType switch
        {
            StatType.Hunger => characterStatus.hunger,
            StatType.Energy => characterStatus.energy,
            StatType.Fun => characterStatus.fun,
            _ => 0
        };
    }

    int GetLevel(float value)
    {
        if (value <= 0) return 0;
        if (value <= 20) return 1;
        if (value <= 40) return 2;
        if (value <= 60) return 3;
        if (value <= 80) return 4;
        return 5;
    }
}
