using UnityEngine;

public class InteractableObject : MonoBehaviour, IClickable
{
    public void OnClick()
    {
        Debug.Log("✨ Click en " + name);
    }
}
