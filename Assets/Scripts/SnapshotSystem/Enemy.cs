using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            agent.destination = target.position;
        }
    }
}
