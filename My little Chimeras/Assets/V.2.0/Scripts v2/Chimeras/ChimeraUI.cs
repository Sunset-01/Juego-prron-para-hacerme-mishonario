//using UnityEngine;
//using TMPro;
//using Unity.VisualScripting;

//public class ChimeraUI : MonoBehaviour
//{
//    public static ChimeraUI Instance;

//    public GameObject popup;
//    public TMP_Text nameText;
//    public TMP_Text descText;
//    public GameObject claimButton;


//    private Chimera current;
//    public Vector3 offset = new Vector3(0, 2, 0);

//    void Awake()
//    {
//        Instance = this;
//        popup.SetActive(false);
//    }

//    public void ShowPopup(Chimera chimera)
//    {
//        current = chimera;

//        nameText.text = chimera.chimeraName;
//        descText.text = chimera.description;

//        popup.SetActive(true);

//        // 🔥 NUEVO: activar/desactivar botón
//        if (claimButton != null)
//            claimButton.SetActive(chimera.hasCapsule);
//    }
//    //public void ShowPopup(Chimera chimera)
//    //{
//    //    current = chimera;

//    //    nameText.text = chimera.chimeraName;
//    //    descText.text = chimera.description;

//    //    popup.SetActive(true);
//    //}

//    //public void ShowPopup(Chimera chimera)
//    //{
//    //    current = chimera;

//    //    nameText.text = chimera.chimeraName;

//    //    claimButton.SetActive(chimera.hasCapsule);
//    //}

//    public void OnClaimCapsule()
//    {
//        if (current == null) return;

//        current.ClaimCapsule();

//        if (claimButton != null)
//            claimButton.SetActive(false);
//    }

//    void Update()
//    {
//        if (current == null) return;

//        Vector3 worldPos = current.popupAnchor.position + offset;
//        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

//        popup.transform.position = screenPos;
//    }
//    public void HidePopup()
//    {
//        popup.SetActive(false);
//    }

//}

using UnityEngine;
using TMPro;

public class ChimeraUI : MonoBehaviour
{
    public static ChimeraUI Instance;

    public GameObject popup;
    public TMP_Text nameText;
    public TMP_Text descText;

    public GameObject claimButton;

    private Chimera current;

    void Awake()
    {
        Instance = this;
        popup.SetActive(false);
    }

    public void ShowPopup(Chimera chimera)
    {
        current = chimera;

        nameText.text = chimera.chimeraName;
        descText.text = chimera.description;

        popup.SetActive(true);

        if (claimButton != null)
            claimButton.SetActive(chimera.hasCapsule);
    }

    public void HidePopup()
    {
        popup.SetActive(false);
    }

    //public void OnClaimCapsule()
    //{
    //    if (current == null) return;

    //    current.ClaimCapsule();

    //    if (claimButton != null)
    //        claimButton.SetActive(false);
    //}
    public TMP_Text rewardText; // opcional para mostrar resultado

    //public void OnClaimCapsule()
    //{
    //    if (current == null) return;

    //    RewardData reward = CapsuleSystem.Instance.OpenCapsule(current.currentCapsuleType);

    //    current.ClaimCapsule();

    //    if (claimButton != null)
    //        claimButton.SetActive(false);

    //    if (rewardText != null)
    //        rewardText.text = $"Obtuviste: {reward.rewardName} x{reward.amount}";
    //}
    public void OnClaimCapsule()
    {
        if (current == null) return;

        RewardData reward = CapsuleSystem.Instance.OpenCapsule(current.currentCapsuleType);

        // 🔥 AÑADIR AL INVENTARIO
        InventorySystem.Instance.AddReward(reward);

        current.ClaimCapsule();

        if (claimButton != null)
            claimButton.SetActive(false);

        if (rewardText != null)
            rewardText.text = $"Obtuviste: {reward.rewardName} x{reward.amount}";
    }

    void Update()
    {
        if (current == null) return;

        Vector3 worldPos = current.popupAnchor != null
            ? current.popupAnchor.position
            : current.transform.position + Vector3.up * 2;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        popup.transform.position = screenPos;
    }
}