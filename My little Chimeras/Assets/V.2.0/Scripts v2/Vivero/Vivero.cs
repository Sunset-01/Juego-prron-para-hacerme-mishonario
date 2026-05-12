using System.Collections.Generic;
using UnityEngine;

public class Vivero : MonoBehaviour
{
    [System.Serializable]
    public class SlotCultivo
    {
        public bool ocupado;
        public CropData cultivoActual;
        public float tiempoRestante;
        public bool listoParaCosechar;

        public Transform puntoSpawn;
        public GameObject modeloActual;

        public int etapaActual = -1;
    }

    public List<SlotCultivo> slots = new List<SlotCultivo>();

    public Inventario inventario;
    public ChimeraGeneral chimera;

    void Update()
    {
        ActualizarCultivos();
    }

    void ActualizarCultivos()
    {
        foreach (var slot in slots)
        {
            if (slot.ocupado && !slot.listoParaCosechar)
            {
                slot.tiempoRestante -= Time.deltaTime;

                float progreso = 1 - (slot.tiempoRestante / slot.cultivoActual.tiempoCrecimiento);

                ActualizarVisual(slot, progreso);

                if (slot.tiempoRestante <= 0)
                {
                    slot.listoParaCosechar = true;
                    ActualizarVisual(slot, 1f);
                }
            }
        }
    }

    void ActualizarVisual(SlotCultivo slot, float progreso)
    {
        int totalEtapas = slot.cultivoActual.etapasVisuales.Length;

        int etapaIndex = Mathf.FloorToInt(progreso * totalEtapas);
        etapaIndex = Mathf.Clamp(etapaIndex, 0, totalEtapas - 1);

        if (slot.etapaActual == etapaIndex) return;

        slot.etapaActual = etapaIndex;

        GameObject nuevoPrefab = slot.cultivoActual.etapasVisuales[etapaIndex];

        if (slot.modeloActual != null)
            Destroy(slot.modeloActual);

        slot.modeloActual = Instantiate(
            nuevoPrefab,
            slot.puntoSpawn.position,
            Quaternion.identity,
            slot.puntoSpawn
        );
    }

    // 🌱 CLICK EN SLOT
    public void InteractuarSlot(int index)
    {
        if (index < 0 || index >= slots.Count) return;

        var slot = slots[index];

        // 🌾 COSECHAR
        if (slot.ocupado && slot.listoParaCosechar)
        {
            Cosechar(index);
            return;
        }

        // 🌱 PLANTAR
        if (!slot.ocupado)
        {
            CropData seleccionado = UI_SemillaSeleccionada.Instance.cultivoSeleccionado;

            if (seleccionado != null)
                Plantar(index, seleccionado);
        }
    }

    public bool Plantar(int index, CropData cultivo)
    {
        var slot = slots[index];

        if (slot.ocupado) return false;

        if (!inventario.HasItem(cultivo.seedID, 1))
            return false;

        inventario.RemoveItem(cultivo.seedID, 1);

        slot.ocupado = true;
        slot.cultivoActual = cultivo;
        slot.tiempoRestante = cultivo.tiempoCrecimiento;
        slot.listoParaCosechar = false;
        slot.etapaActual = -1;

        ActualizarVisual(slot, 0f);

        return true;
    }

    public void Cosechar(int index)
    {
        var slot = slots[index];

        int cantidad = CalcularProduccion(slot.cultivoActual);

        inventario.AddItem(slot.cultivoActual.cropID, cantidad);

        if (slot.modeloActual != null)
            Destroy(slot.modeloActual);

        slot.ocupado = false;
        slot.cultivoActual = null;
        slot.tiempoRestante = 0;
        slot.listoParaCosechar = false;
        slot.etapaActual = -1;
    }

    int CalcularProduccion(CropData cultivo)
    {
        int cantidad = cultivo.baseYield;

        float luck = chimera.GetLuckMultiplier();

        for (int i = 0; i < cultivo.extraRolls; i++)
        {
            float prob = cultivo.extraChance * luck;

            if (Random.value < prob)
                cantidad++;
        }

        return cantidad;
    }
}