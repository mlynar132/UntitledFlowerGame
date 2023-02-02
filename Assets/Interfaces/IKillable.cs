public interface IKillable
{
    bool GetIsStuned();
    void TakeDamage(int damage);
    void Death();
}