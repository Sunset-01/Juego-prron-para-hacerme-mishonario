using UnityEngine;

public enum Rarity
{
    ThreeStar,
    FourStar,
    FiveStar
}

[CreateAssetMenu(fileName = "GachaItem", menuName = "Gacha/Gacha Item")]
public class GachaItem : ScriptableObject
{
    public string itemName;
    public Rarity rarity;
    public Sprite icon;
}
