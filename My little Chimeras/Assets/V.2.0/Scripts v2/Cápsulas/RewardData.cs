using UnityEngine;

[System.Serializable]
public class RewardData
{
    public string rewardName;
    public ResourceType resourceType;
    public int amount;

    [Range(0f, 1f)]
    public float probability;
}