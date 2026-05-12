using UnityEngine;

[CreateAssetMenu(menuName = "Chimera/Capsule Loot Table")]
public class CapsuleLootTable : ScriptableObject
{
    public CapsuleType capsuleType;
    public RewardData[] rewards;

    public RewardData GetRandomReward()
    {
        float roll = Random.value;
        float cumulative = 0f;

        foreach (var reward in rewards)
        {
            cumulative += reward.probability;

            if (roll <= cumulative)
                return reward;
        }

        return rewards[rewards.Length - 1];
    }
}