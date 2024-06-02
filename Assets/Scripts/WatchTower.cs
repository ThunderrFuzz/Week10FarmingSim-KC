
using System.Collections.Generic;
using UnityEngine;

public class WatchTower : MonoBehaviour
{
    public int managerSlots = 2;
    public List<Manager> managers;

    void Start()
    {
        managers = new List<Manager>();
        Debug.Log("WatchTower initialized with manager slots: " + managerSlots);
    }

    public void AddManager(Manager manager)
    {
        if (managers.Count < managerSlots)
        {
            managers.Add(manager);
            manager.ApplyBonus();
            UpdateManagerBonuses();
            Debug.Log("Manager added: " + manager.name);
        }
        else
        {
            Debug.Log("Cannot add manager, slots are full.");
        }
    }

    public void RemoveManager(Manager manager)
    {
        if (managers.Contains(manager))
        {
            managers.Remove(manager);
            UpdateManagerBonuses();
            Debug.Log("Manager removed: " + manager.name);
        }
        else
        {
            Debug.Log("Manager not found in the list.");
        }
    }

    void UpdateManagerBonuses()
    {
        if (managers.Count == 2)
        {
            Manager manager1 = managers[0];
            Manager manager2 = managers[1];
            Debug.Log("Updating manager bonuses.");

            if (manager1 is PlanterManager && manager2 is HarvesterManager)
            {
                manager1.watchTowerRange += 5;
                manager2.watchTowerRange += 5;
                Debug.Log("Applied range boost bonus.");
            }
            else if (manager1 is PlanterManager && manager2 is PlanterManager)
            {
                manager1.watchTowerRange += 5;
                manager2.watchTowerRange += 5;
                Debug.Log("Applied movement speed bonus.");
            }
            else if (manager1 is HarvesterManager && manager2 is HarvesterManager)
            {
                manager1.watchTowerRange += 5;
                manager2.watchTowerRange += 5;
                Debug.Log("Applied grow time bonus.");
            }

            foreach (Manager manager in managers)
            {
                manager.ApplyBonus();
                Debug.Log("Bonus applied to manager: " + manager.name);
            }
        }
    }
}

