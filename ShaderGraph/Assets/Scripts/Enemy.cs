using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float damageRadius = 5f;
    [SerializeField] private float damageCooldownSeconds = 1f;
    [SerializeField] private float maxHealth = 30f;

    private float cooldown = 0f;
    private float health;

    public static List<Enemy> enemies;

    private void Awake()
    {
        enemies = new List<Enemy>();
    }

    private void Start()
    {
        health = maxHealth;

        enemies.Add(this);
    }
    

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

    public void Hurt(float amount)
    {
        health -= amount;
        animator.SetTrigger("Hurt");
        if (health <= 0f)
        {
            enemies.Remove(this);
            Destroy(gameObject);
        }
    }
}
