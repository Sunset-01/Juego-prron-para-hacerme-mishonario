using System.Collections.Generic;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    public GachaBanner currentBanner;

    private int pullsSinceLastFiveStar = 0;
    private int pullsSinceLastFourStar = 0;

    public GachaItem Pull()
    {
        pullsSinceLastFiveStar++;
        pullsSinceLastFourStar++;

        // Garantía 5★
        if (pullsSinceLastFiveStar >= currentBanner.fiveStarPity)
            return GetFiveStar();

        // Garantía 4★
        if (pullsSinceLastFourStar >= currentBanner.fourStarPity)
            return GetFourStar();

        float roll = Random.Range(0f, 100f);

        if (roll < currentBanner.fiveStarRate)
            return GetFiveStar();
        else if (roll < currentBanner.fiveStarRate + currentBanner.fourStarRate)
            return GetFourStar();
        else
            return GetThreeStar();
    }

    //public GachaResultUI resultUI;

    public GachaResultsPanelUI resultsPanelUI;

    public void PullFiveFromButton()
    {
        List<GachaItem> results = new List<GachaItem>();

        for (int i = 0; i < 5; i++)
        {
            results.Add(Pull());
        }

        if (resultsPanelUI != null)
            resultsPanelUI.ShowResults(results);
    }

    //public void PullFromButton()
    //{
    //    for (int i = 1; i <= 5; i++)
    //    {
    //        GachaItem result = Pull();

    //        if (resultUI != null)
    //            resultUI.ShowResult(result);

    //        Debug.Log("Obtuviste: " + result.itemName + " (" + result.rarity + ")");
    //    }
    //}


    GachaItem GetFiveStar()
    {
        pullsSinceLastFiveStar = 0;
        pullsSinceLastFourStar = 0;
        return currentBanner.fiveStarItems[
            Random.Range(0, currentBanner.fiveStarItems.Count)];
    }

    GachaItem GetFourStar()
    {
        pullsSinceLastFourStar = 0;
        return currentBanner.fourStarItems[
            Random.Range(0, currentBanner.fourStarItems.Count)];
    }

    GachaItem GetThreeStar()
    {
        return currentBanner.threeStarItems[
            Random.Range(0, currentBanner.threeStarItems.Count)];
    }
}
