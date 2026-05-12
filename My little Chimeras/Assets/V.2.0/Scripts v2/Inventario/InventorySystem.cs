using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    private Dictionary<ResourceType, int> inventory = new Dictionary<ResourceType, int>();

    void Awake()
    {
        Instance = this;
        LoadInventory();
    }

    public void AddReward(RewardData reward)
    {
        if (!inventory.ContainsKey(reward.resourceType))
            inventory[reward.resourceType] = 0;

        inventory[reward.resourceType] += reward.amount;

        SaveInventory();
    }

    public int GetAmount(ResourceType type)
    {
        return inventory.ContainsKey(type) ? inventory[type] : 0;
    }

    // -------- GUARDADO --------

    void SaveInventory()
    {
        foreach (var pair in inventory)
        {
            PlayerPrefs.SetInt(pair.Key.ToString(), pair.Value);
        }

        PlayerPrefs.Save();
    }

    void LoadInventory()
    {
        foreach (ResourceType type in System.Enum.GetValues(typeof(ResourceType)))
        {
            int value = PlayerPrefs.GetInt(type.ToString(), 0);
            inventory[type] = value;
        }
    }
}