using UnityEngine;

public class ChimeraGlow : MonoBehaviour
{
    public GameObject glowObject;

    public void SetGlow(bool active)
    {
        if (glowObject != null)
            glowObject.SetActive(active);
    }
}