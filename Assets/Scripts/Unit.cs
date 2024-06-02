using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    protected NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
}
