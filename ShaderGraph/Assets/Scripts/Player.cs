using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;

    private float health;

    public static Player instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Debug.LogWarning("More than one Player detected in the scene, disabling this one");
            enabled = false;
        }
    }

    private void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hurt(float amount)
    {
        health -= amount;
        if (health < 0f)
        {
            Die();
        }
        Debug.Log("My health decreased by " + amount + " and now is " + health);
    }

    private void Die()
    {
        Debug.Log("I died!");
    }
}
