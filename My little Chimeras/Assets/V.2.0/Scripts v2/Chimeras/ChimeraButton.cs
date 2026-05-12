using UnityEngine;
using UnityEngine.UI;

public class ChimeraButton : MonoBehaviour
{
    public Image iconImage;
    private Chimera chimera;

    public void Setup(Chimera c)
    {
        chimera = c;
        iconImage.sprite = c.icon;
    }

    public void OnClick()
    {
        ChimeraManager.Instance.SelectChimera(chimera);
    }
}