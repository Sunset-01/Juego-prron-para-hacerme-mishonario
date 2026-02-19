using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GachaResultUI : MonoBehaviour
{
    public Image itemIcon;
    public TMP_Text itemNameText;
    public TMP_Text rarityText;

    public void ShowResult(GachaItem item)
    {
        itemIcon.sprite = item.icon;
        itemNameText.text = item.itemName;
        rarityText.text = item.rarity.ToString();

        SetRarityColor(item.rarity);
    }

    void SetRarityColor(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.ThreeStar:
                rarityText.color = Color.white;
                break;
            case Rarity.FourStar:
                rarityText.color = Color.magenta;
                break;
            case Rarity.FiveStar:
                rarityText.color = Color.yellow;
                break;
        }
    }
}
