using UnityEngine;
using UnityEngine.AI;

public class HarvesterManager : Manager
{
    public float harvestBonus;
    public string bonusType = "harvest";

    private void Start()
    {
        bonusType = "harvest";
        agent = GetComponent<NavMeshAgent>();
        wt_ = FindObjectOfType<WatchTower>();
        Debug.Log("HarvesterManager started with bonus type: " + bonusType);
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
