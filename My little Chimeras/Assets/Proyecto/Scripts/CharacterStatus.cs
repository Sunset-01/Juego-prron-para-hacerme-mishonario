using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [System.Serializable]
    public class CharacterData
    {
        public float hunger;
        public float energy;
        public float fun;

        public float currentHappiness;
        public int stars;

        public string lastSaveTime;
    }

    [Header("Character Stats")]
    [Range(0, 100)] public float hunger = 100;
    [Range(0, 100)] public float energy = 100;
    [Range(0, 100)] public float fun = 100;

    [Header("Decay Rates")]
    public float hungerDecay = 1f;
    public float energyDecay = 0.5f;
    public float funDecay = 0.8f;

    void Update()
    {
        hunger = Mathf.Clamp(hunger - hungerDecay * Time.deltaTime, 0, 100);
        energy = Mathf.Clamp(energy - energyDecay * Time.deltaTime, 0, 100);
        fun = Mathf.Clamp(fun - funDecay * Time.deltaTime, 0, 100);
    }

    // Devuelve un valor entre 0 y 1
    public float GetHappinessMultiplier()
    {
        // Si algún stat está en 0, no hay felicidad
        if (hunger <= 0 || energy <= 0 || fun <= 0)
            return 0f;

        float average = (hunger + energy + fun) / 3f;
        return average / 100f;
    }

}
