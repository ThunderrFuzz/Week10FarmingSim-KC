
using UnityEngine;
using UnityEngine.AI;

public class PlanterManager : Manager
{
    public float plantBonus;
    public string bonusType = "plant";

    private void Start()
    {
        bonusType = "plant";
        agent = GetComponent<NavMeshAgent>();
        wt_ = FindObjectOfType<WatchTower>();
        Debug.Log("PlanterManager started with bonus type: " + bonusType);
    }

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
            }
        }
        else
        {
            hasArrived = false;
        }
    }

   
}
