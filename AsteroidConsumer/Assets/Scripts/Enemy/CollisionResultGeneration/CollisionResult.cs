public class CollisionResult
{
    public InitiatorCollisionResult initiatorCollisionResult { get; set; }
    public float Mass { get; set; }
    public float Solid { get; set; }
    public SpaceBodyType EnemyType { get; set; }
    public bool FullyConsumed { get; set; }
}
