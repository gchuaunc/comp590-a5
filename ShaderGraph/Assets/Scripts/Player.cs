using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator saberAnimator;
    [SerializeField] private Saber saber;
    [SerializeField] private float attackCooldown = 1.5f;

    private float health;
    private float cooldown = 0f;

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
        if (Input.GetButtonDown("Fire1") && cooldown < 0.1f)
        {
            saber.PlaySwingClip();
            saberAnimator.SetTrigger("Swing");
        }
        cooldown -= Time.deltaTime;
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
