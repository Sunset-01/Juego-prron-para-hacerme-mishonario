using UnityEngine;
using System.Collections.Generic;

public class GachaResultsPanelUI : MonoBehaviour
{
    [Header("Setup")]
    public GachaResultUI resultPrefab;
    public Transform resultsParent;

    List<GachaResultUI> activeResults = new List<GachaResultUI>();

    public void ShowResults(List<GachaItem> items)
    {
        ClearResults();

        foreach (GachaItem item in items)
        {
            GachaResultUI ui =
                Instantiate(resultPrefab, resultsParent);

            ui.ShowResult(item);
            activeResults.Add(ui);
        }
    }

    void ClearResults()
    {
        foreach (var ui in activeResults)
            Destroy(ui.gameObject);

        activeResults.Clear();
    }
}
