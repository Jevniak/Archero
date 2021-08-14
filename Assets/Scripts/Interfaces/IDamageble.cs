public interface IDamageable<T>
{
    void TakeDamage(T damageTaken);
}

public interface IKillable
{
    void Kill();
}