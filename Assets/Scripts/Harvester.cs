using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Harvester : Unit
{
    public float harvestRange;
    public float harvestSpeed;

    void Update()
    {
        //MoveToDestination();
        HarvestCropsInRange();
    }

    void HarvestCropsInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, harvestRange);
        foreach (var hitCollider in hitColliders)
        {
            Crop crop = hitCollider.GetComponent<Crop>();
            if (crop != null && crop.isRipe)
            {
                Harvest(crop);
            }
        }
    }

    void Harvest(Crop crop)
    {
        if (Vector3.Distance(transform.position, crop.transform.position) <= harvestRange)
        {
            CropManager.HarvestCrop(crop);
        }
    }
}