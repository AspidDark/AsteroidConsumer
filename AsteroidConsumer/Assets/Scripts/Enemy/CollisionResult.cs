public class CollisionResult
{
    public InitiatorCollisionResult initiatorCollisionResult { get; set; }
    public float Mass { get; set; }
    public float Solid { get; set; }
    public EnemyType EnemyType { get; set; }
    public bool FullyConsumed { get; set; }
}
