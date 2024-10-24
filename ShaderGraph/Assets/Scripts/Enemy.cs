using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    private void FixedUpdate()
    {
        agent.SetDestination(Player.instance.transform.position);
    }
}
