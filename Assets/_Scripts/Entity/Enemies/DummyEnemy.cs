public class DummyEnemy : EnemyBase {
    protected override void Die() {
        base.Die();
        Destroy(gameObject);
    }
}
