//using UnityEngine;
//using TMPro;

//public class InventoryUI : MonoBehaviour
//{
//    public TMP_Text basuraText;
//    public TMP_Text ItemEspecialText;

//    void Update()
//    {
//        basuraText.text = "Basura: " + InventorySystem.Instance.GetAmount(ResourceType.Basura);
//        ItemEspecialText.text = "ItemEspecial: " + InventorySystem.Instance.GetAmount(ResourceType.ItemEspecial);
//    }
//}
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public TMP_Text basuraText;
    public TMP_Text itemEText;

    void Update()
    {
        basuraText.text = "Basura: " + InventorySystem.Instance.GetAmount(ResourceType.Basura);
        itemEText.text = "ItemEspecial: " + InventorySystem.Instance.GetAmount(ResourceType.ItemEspecial);
    }
}