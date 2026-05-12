using UnityEngine;

public class SlotClick : MonoBehaviour
{
    public int index;
    public Vivero vivero;

    void OnMouseDown()
    {
        vivero.InteractuarSlot(index);
        Debug.Log("interactuado");
    }
}