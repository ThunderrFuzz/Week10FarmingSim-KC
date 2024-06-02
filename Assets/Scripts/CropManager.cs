using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    public static List<Crop> crops = new List<Crop>();
    public static int score = 0;

    public static void PlantCrop(Crop crop)
    {
        crops.Add(crop);
        Debug.Log("Crop planted. Total crops: " + crops.Count);
    }

    public static void HarvestCrop(Crop crop)
    {
        if (crops.Contains(crop))
        {
            score += crop.value;
            crop.Harvest();
            crops.Remove(crop);
            Debug.Log("Crop harvested. Total score: " + score);
        }
    }

    public void UpdateCrops()
    {
        foreach (Crop crop in crops)
        {
            crop.Update();
        }
    }
}
