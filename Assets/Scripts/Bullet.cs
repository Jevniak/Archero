using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
