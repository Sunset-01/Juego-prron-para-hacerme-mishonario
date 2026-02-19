using UnityEngine;

public class GachaTester : MonoBehaviour
{
    public GachaSystem gacha;

    void Start()
    {
        for (int i = 1; i <= 5; i++)
        {
            GachaItem result = gacha.Pull();
            Debug.Log($"Pull {i}: {result.itemName} ({result.rarity})");
        }
    }
}
