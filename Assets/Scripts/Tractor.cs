using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tractor : Unit
{
    public float plantRange;
    public Crop selectedCrop; // The crop selected from the dropdown menu
    private bool hasPlanted = false; // To ensure only one crop is planted per click
    private bool hasArrived = false; // To track if the tractor has arrived at the destination
    [SerializeField] Vector3 spawnOffset;
    void Update()
    {
        if (agent.hasPath && !agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    if (!hasArrived)
                    {
                        hasArrived = true;
                        OnArrivedAtDestination();
                    }
                }
            }
            else
            {
                hasArrived = false;
                hasPlanted = false; // Reset hasPlanted flag when tractor is moving
            }
        }
        else
        {
            hasArrived = false;
            hasPlanted = false; // Reset hasPlanted flag when tractor is not moving
        }
    }

    void OnArrivedAtDestination()
    {
        Debug.Log("Tractor has arrived at the destination and is stationary.");
        agent.velocity = Vector3.zero;
        if (IsInFarmland())
        {
            Debug.Log("Tractor is in Farmland. Attempting to plant crops.");
            PlantCropsInRange();
        }
        else
        {
            Debug.Log("Tractor is not in Farmland area.");
        }
    }

    bool IsInFarmland()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.3f);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Checking collider: " + hitCollider.name);
            if (hitCollider.CompareTag("Farmland"))
            {
                Debug.Log("Tractor is within Farmland area.");
                return true;
            }
        }
        Debug.Log("Tractor is not in Farmland area.");
        return false;
    }

    void PlantCropsInRange()
    {
        if (selectedCrop != null && !hasPlanted)
        {
            Vector3 plantPosition = transform.position;
            Vector3 spawnOffset = new Vector3(0f, 0.5f, 0f); // Set the spawn offset here
            Debug.Log("Planting crop at position: " + plantPosition + " with spawn offset: " + spawnOffset);
            Plant(plantPosition, spawnOffset);
            hasPlanted = true; // Set hasPlanted to true after planting
        }
        else if (selectedCrop == null)
        {
            Debug.Log("No crop selected for planting.");
        }
    }

    public void Plant(Vector3 position, Vector3 spawnOffset)
    {
        if (selectedCrop != null)
        {
            Crop newCrop = Instantiate(selectedCrop, position + spawnOffset, Quaternion.identity);
            CropManager.PlantCrop(newCrop);
            Debug.Log("Crop instantiated at: " + (position + spawnOffset));
        }
        else
        {
            Debug.Log("No crop prefab assigned.");
        }
    }

    public void SetDestination(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
        Debug.Log("Tractor destination set to: " + targetPosition);
        hasPlanted = false; // Allow planting at the new destination
        hasArrived = false; // Reset arrival flag when setting a new destination
        agent.isStopped = false; // Allow the tractor to move to the next destination
    }
}









