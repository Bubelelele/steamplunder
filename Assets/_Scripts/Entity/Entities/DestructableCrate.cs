public class DestructableCrate : EntityBase {
    protected override void Die() {
        Destroy(gameObject);
    }
}
