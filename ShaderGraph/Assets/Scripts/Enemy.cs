using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float damageRadius = 5f;
    [SerializeField] private float damageCooldownSeconds = 1f;

    private float cooldown = 0f;

    private void FixedUpdate()
    {
        Vector3 playerPosition = Player.instance.transform.position;
        float distanceToPlayer = Vector3.Distance(playerPosition, transform.position);

        if (distanceToPlayer > damageRadius / 1.25)
        {
            agent.isStopped = false;
            agent.SetDestination(playerPosition);
        } else
        {
            agent.isStopped = true;
        }

        if (cooldown < 0.1f && distanceToPlayer < damageRadius)
        {
            Player.instance.Hurt(damage);
            cooldown = damageCooldownSeconds;
        }

        cooldown -= Time.fixedDeltaTime;
    }
}
