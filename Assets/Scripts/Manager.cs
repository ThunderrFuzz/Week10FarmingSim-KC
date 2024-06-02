/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Manager : Unit
{
    public float watchTowerRange = 10f;
    public WatchTower wt_;
    public GameManager gm_;
    protected NavMeshAgent agent;
    protected bool hasArrived = false;

    void Update()
    {
        CheckIfOutOfRange();
    }

    void CheckIfOutOfRange()
    {
        Debug.Log("ifout of range called");
        if (wt_ != null && !IsWithinWatchpointRange())
        {
            
                wt_.RemoveManager(this);
                Debug.Log("Manager removed from WatchTower as it is out of range.");
            
        }
    }
    public virtual void ApplyBonus() { }
    public bool IsWithinWatchpointRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, watchTowerRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Watchpoint"))
            {
                Debug.Log("Manager is within watchpoint range.");
                return true;
            }
        }
        Debug.Log("Manager is not within watchpoint range.");
        return false;
    }

    public void OnArrivedAtDestination()
    {
        Debug.Log("Manager arrived at destination.");
        if (IsWithinWatchpointRange())
        {
            wt_.AddManager(this);
            Debug.Log("Manager added to WatchTower.");
        }
        else
        {
            Debug.Log("Manager is not within watchpoint range after arriving.");
        }
    }
}
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Manager : Unit
{
    public float watchTowerRange = 10f;
    public WatchTower wt_;
    public GameManager gm_;
    protected NavMeshAgent agent;
    protected bool hasArrived = false;
    private bool isInRange = false;

    public virtual void ApplyBonus() { }

    public bool IsWithinWatchpointRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, watchTowerRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Watchpoint"))
            {
                Debug.Log("Manager is within watchpoint range.");
                return true;
            }
        }
        Debug.Log("Manager is not within watchpoint range.");
        return false;
    }

    public void OnArrivedAtDestination()
    {
        Debug.Log("Manager arrived at destination.");
        WatchTower watchTower = GetClosestWatchTower(); // Get the closest WatchTower
        wt_ = watchTower; // Assign the WatchTower instance
        if (IsWithinWatchpointRange())
        {
            wt_.AddManager(this);
            Debug.Log("Manager added to WatchTower.");
        }
        else
        {
            wt_.RemoveManager(this);
            Debug.Log("Manager is not within watchpoint range after arriving.");
        }
    }
   
    WatchTower GetClosestWatchTower()
    {
        
        WatchTower[] watchTowers = GameObject.FindObjectsOfType<WatchTower>();
        WatchTower closestWatchTower = null;
        float closestDistance = float.MaxValue;
        foreach (WatchTower watchTower in watchTowers)
        {
            float distance = Vector3.Distance(transform.position, watchTower.transform.position);
            if (distance < closestDistance)
            {
                closestWatchTower = watchTower;
                closestDistance = distance;
            }
        }
        return closestWatchTower;
    }

}