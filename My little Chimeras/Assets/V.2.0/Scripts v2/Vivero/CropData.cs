//using UnityEngine;

//[CreateAssetMenu(fileName = "NuevoCultivo", menuName = "Cultivos/CropData")]
//public class CropData : ScriptableObject
//{
//    public string seedID;
//    public string cropID;

//    public float tiempoCrecimiento;

//    public int baseYield = 1;

//    [Header("Bonus por suerte")]
//    public int extraRolls = 2;
//    public float extraChance = 0.25f;
//}
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoCultivo", menuName = "Cultivos/CropData")]
public class CropData : ScriptableObject
{
    public string seedID;
    public string cropID;

    public float tiempoCrecimiento;

    public int baseYield = 1;

    [Header("Bonus por suerte")]
    public int extraRolls = 2;
    public float extraChance = 0.25f;

    [Header("Etapas visuales")]
    public GameObject[] etapasVisuales;
}