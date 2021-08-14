using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour, IDamageable<int>, IKillable
{
    [SerializeField] private int maxHealth;
    private int health;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        if (health <= 0)
            Kill();
    }

    public void Kill()
    {
        Destroy(gameObject);
        if (CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}