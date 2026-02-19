using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GachaBanner", menuName = "Gacha/Gacha Banner")]
public class GachaBanner : ScriptableObject
{
    [Header("Pools")]
    public List<GachaItem> threeStarItems;
    public List<GachaItem> fourStarItems;
    public List<GachaItem> fiveStarItems;

    [Header("Probabilidades (%)")]
    public float fiveStarRate = 0.6f;
    public float fourStarRate = 5.1f;
    public float threeStarRate = 94.3f;

    [Header("Pity")]
    public int fiveStarPity = 90;
    public int fourStarPity = 10;
}
