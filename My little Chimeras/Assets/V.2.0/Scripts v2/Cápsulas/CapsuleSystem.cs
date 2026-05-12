using UnityEngine;

public class CapsuleSystem : MonoBehaviour
{
    public static CapsuleSystem Instance;

    public CapsuleLootTable comunTable;
    public CapsuleLootTable raraTable;
    public CapsuleLootTable epicaTable;

    void Awake()
    {
        Instance = this;
    }

    public RewardData OpenCapsule(CapsuleType type)
    {
        CapsuleLootTable table = null;

        switch (type)
        {
            case CapsuleType.Comun: table = comunTable; break;
            case CapsuleType.Rara: table = raraTable; break;
            case CapsuleType.Epica: table = epicaTable; break;
        }

        return table.GetRandomReward();
    }
}