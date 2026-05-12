using UnityEngine;

public class UI_SemillaSeleccionada : MonoBehaviour
{
    public static UI_SemillaSeleccionada Instance;

    public CropData cultivoSeleccionado;

    private void Awake()
    {
        Instance = this;
    }

    public void SeleccionarSemilla(CropData cultivo)
    {
        cultivoSeleccionado = cultivo;
        Debug.Log("Seleccionada");
    }

    public void LimpiarSeleccion()
    {
        cultivoSeleccionado = null;
    }
}