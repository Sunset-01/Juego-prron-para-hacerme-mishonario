using UnityEngine;

public class ChimeraMenuUI : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform container;

    void Start()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        Chimera[] all = FindObjectsOfType<Chimera>();

        foreach (Chimera c in all)
        {
            GameObject btn = Instantiate(buttonPrefab, container);
            btn.GetComponent<ChimeraButton>().Setup(c);
        }
    }
}