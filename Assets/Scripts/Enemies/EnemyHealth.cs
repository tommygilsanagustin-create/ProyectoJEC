using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;

    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (animator != null)
            animator.SetBool("isDead", true);

        Destroy(gameObject, 0.6f);
    }
}