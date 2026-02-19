using UnityEngine;

public class PlayerSelectionVisual: MonoBehaviour
{
    public GameObject selectionCircle;

    void Awake()
    {
        if (selectionCircle != null)
            selectionCircle.SetActive(false);
    }

    public void SetSelected(bool selected)
    {
        if (selectionCircle != null)
            selectionCircle.SetActive(selected);
    }
}
