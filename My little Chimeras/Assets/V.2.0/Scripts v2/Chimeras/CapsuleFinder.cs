//using UnityEngine;

//public class CapsuleFinder : MonoBehaviour
//{
//    public float checkInterval = 5f;
//    public int currentFloor = 1; // 1 a 6

//    private Chimera chimera;
//    private float timer;

//    void Start()
//    {
//        chimera = GetComponent<Chimera>();
//    }

//    void Update()
//    {
//        if (chimera.hasCapsule) return;

//        timer += Time.deltaTime;

//        if (timer >= checkInterval)
//        {
//            TryFindCapsule();
//            timer = 0f;
//        }
//    }

//    void TryFindCapsule()
//    {
//        float chance = CalculateChance();

//        float roll = Random.value;

//        if (roll < chance)
//        {
//            chimera.OnCapsuleFound();
//        }
//    }

//    float CalculateChance()
//    {
//        float baseChance = 0.05f;

//        // Suerte
//        baseChance += chimera.suerte * 0.02f;

//        // Afinidad
//        baseChance += chimera.afinidad * 0.01f;

//        // Rareza
//        switch (chimera.rareza)
//        {
//            case Rareza.Baja: baseChance += 0.03f; break;
//            case Rareza.Media: baseChance += 0.015f; break;
//            case Rareza.Alta: baseChance += 0.005f; break;
//        }

//        // Piso
//        baseChance += currentFloor * 0.01f;

//        // Estado
//        switch (chimera.estado)
//        {
//            case Estado.Feliz: baseChance += 0.03f; break;
//            case Estado.Hambriento: baseChance -= 0.02f; break;
//            case Estado.Cansado: baseChance -= 0.01f; break;
//        }

//        return Mathf.Clamp01(baseChance);
//    }
//}

using UnityEngine;

public class CapsuleFinder : MonoBehaviour
{
    public float checkInterval = 5f;
    public int currentFloor = 1;

    private Chimera chimera;
    private float timer;

    void Start()
    {
        chimera = GetComponent<Chimera>();
    }

    void Update()
    {
        if (chimera.hasCapsule) return;

        timer += Time.deltaTime;

        if (timer >= checkInterval)
        {
            TryFindCapsule();
            timer = 0f;
        }
    }

    //void TryFindCapsule()
    //{
    //    float chance = CalculateChance();
    //    float roll = Random.value;

    //    if (roll < chance)
    //    {
    //        chimera.OnCapsuleFound();
    //    }
    //}
    void TryFindCapsule()
    {
        float chance = CalculateChance();
        float roll = Random.value;

        if (roll < chance)
        {
            CapsuleType type = RollCapsuleType();
            chimera.OnCapsuleFound(type);
        }
    }
    CapsuleType RollCapsuleType()
    {
        float roll = Random.value;

        if (roll < 0.6f) return CapsuleType.Comun;
        if (roll < 0.9f) return CapsuleType.Rara;
        return CapsuleType.Epica;
    }


    float CalculateChance()
    {
        float baseChance = 0.05f;

        baseChance += chimera.suerte * 0.02f;
        baseChance += chimera.afinidad * 0.01f;

        switch (chimera.rareza)
        {
            case Rareza.Baja: baseChance += 0.03f; break;
            case Rareza.Media: baseChance += 0.015f; break;
            case Rareza.Alta: baseChance += 0.005f; break;
        }

        baseChance += currentFloor * 0.01f;

        switch (chimera.estado)
        {
            case Estado.Feliz: baseChance += 0.03f; break;
            case Estado.Hambriento: baseChance -= 0.02f; break;
            case Estado.Cansado: baseChance -= 0.01f; break;
        }

        return Mathf.Clamp01(baseChance);
    }
}