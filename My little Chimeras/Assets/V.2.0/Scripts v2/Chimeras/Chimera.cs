//using UnityEngine;

//public class Chimera : MonoBehaviour
//{
//    public string chimeraName;
//    [TextArea] public string description;
//    public Sprite icon;

//    // Punto donde aparecerá el popup (encima)
//    public Transform popupAnchor;
//}
//using UnityEngine;

//public class Chimera : MonoBehaviour
//{
//    [Header("Info")]
//    public string chimeraName;
//    public Sprite icon;

//    [Header("Stats")]
//    public float suerte = 1f;
//    public float afinidad = 1f;
//    public Rareza rareza;
//    public Estado estado;

//    [Header("Sistema cápsulas")]
//    public bool hasCapsule = false;

//    private CapsuleFinder finder;

//    void Start()
//    {
//        finder = GetComponent<CapsuleFinder>();
//    }

//    public void OnCapsuleFound()
//    {
//        hasCapsule = true;

//        // Activar brillo
//        GetComponent<ChimeraGlow>().SetGlow(true);
//    }

//    public void ClaimCapsule()
//    {
//        hasCapsule = false;

//        // Quitar brillo
//        GetComponent<ChimeraGlow>().SetGlow(false);
//    }
//}

using UnityEngine;

public class Chimera : MonoBehaviour
{
    [Header("Info")]
    public string chimeraName;

    [TextArea]
    public string description;

    public Sprite icon;
    public Transform popupAnchor;
    public CapsuleType currentCapsuleType;

    [Header("Stats")]
    public float suerte = 1f;
    public float afinidad = 1f;
    public Rareza rareza;
    public Estado estado;

    [Header("Sistema cápsulas")]
    public bool hasCapsule = false;

    public void OnCapsuleFound(CapsuleType type)
    {
        hasCapsule = true;
        currentCapsuleType = type;

        GetComponent<ChimeraGlow>()?.SetGlow(true);
    }

    public void ClaimCapsule()
    {
        hasCapsule = false;
        GetComponent<ChimeraGlow>()?.SetGlow(false);
    }
}