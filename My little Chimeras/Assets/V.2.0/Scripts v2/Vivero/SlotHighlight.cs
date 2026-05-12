using UnityEngine;

public class SlotHighlight : MonoBehaviour
{
    [Header("Render")]
    public Renderer targetRenderer;

    [Header("Colores")]
    public Color colorNormal = Color.white;
    public Color colorHover = Color.yellow;

    [Header("Opcional")]
    public bool usarEmision = false;
    public Color colorEmision = Color.yellow;

    Material materialInstancia;

    void Start()
    {
        // Clonar material para no modificar el original
        materialInstancia = targetRenderer.material;
        materialInstancia.color = colorNormal;

        if (usarEmision)
            materialInstancia.EnableKeyword("_EMISSION");
    }

    void OnMouseEnter()
    {
        SetHighlight(true);
    }

    void OnMouseExit()
    {
        SetHighlight(false);
    }

    void SetHighlight(bool activo)
    {
        if (activo)
        {
            materialInstancia.color = colorHover;

            if (usarEmision)
                materialInstancia.SetColor("_EmissionColor", colorEmision);
        }
        else
        {
            materialInstancia.color = colorNormal;

            if (usarEmision)
                materialInstancia.SetColor("_EmissionColor", Color.black);
        }
    }
}