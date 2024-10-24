using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator saberAnimator;
    [SerializeField] private Saber saber;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private GameObject hurtEffect; // yes yes I know you shouldn't mess with UI from here but quiet this is just for an assignment
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TextMeshProUGUI objectiveText;

    private float health;
    private float cooldown = 0f;

    public static Player instance;

    private void Awake()
    {
        Time.timeScale = 1.0f;
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
        hpText.text = health + " hp";
    }

    // Update is called once per frame
    void Update()
    {
        if (saber.IsOn && Input.GetButtonDown("Fire1") && cooldown < 0.1f)
        {
            saber.PlaySwingClip();
            saberAnimator.SetTrigger("Swing");
            cooldown = attackCooldown;

            if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out RaycastHit hit, attackRange))
            {
                if (hit.collider.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.Hurt(attackDamage);
                }
            }
        }
        cooldown -= Time.deltaTime;

        // check for win
        int numEnemies = Enemy.enemies.Count;
        objectiveText.text = "Objective: Eliminate all enemies. (" + numEnemies + " left)";
        if (numEnemies == 0)
        {
            Time.timeScale = 0f;
            winPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Hurt(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
        Debug.Log("My health decreased by " + amount + " and now is " + health);
        hurtEffect.SetActive(true);
        hpText.text = health + " hp";
    }

    private void Die()
    {
        Debug.Log("I died!");
        Time.timeScale = 0f;
        losePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
