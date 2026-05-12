using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{

    void Start()
    {
        AddItem("semilla_trigo", 10);
        Debug.Log("añadido");
    }

    Dictionary<string, int> items = new Dictionary<string, int>();

    public bool HasItem(string id, int cantidad)
    {
        return items.ContainsKey(id) && items[id] >= cantidad;
    }

    public void RemoveItem(string id, int cantidad)
    {
        if (!items.ContainsKey(id)) return;

        items[id] -= cantidad;

        if (items[id] <= 0)
            items.Remove(id);
    }

    public void AddItem(string id, int cantidad)
    {
        if (!items.ContainsKey(id))
            items[id] = 0;

        items[id] += cantidad;
    }
}