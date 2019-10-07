public abstract class ColllisionResultGeneratorBase
{
    CollisionResult collisionResult;
    private EnemyStats _initator;
    private EnemyStats _other;
    private float _magnitude;
    public ColllisionResultGeneratorBase(EnemyStats initator, EnemyStats other, float magnitude)
    {
        _initator = initator;
        _other = other;
        _magnitude = magnitude;
        collisionResult = new CollisionResult();
    }
    //to do seal this or oveeride if needed...
    public virtual CollisionResult GetCollisionResult()
    {
        collisionResult.initiatorCollisionResult = GetResult(_initator,  _other,  _magnitude);
        collisionResult.Mass = GetMass(_initator, _other, _magnitude);
        collisionResult.Solid= GetSolid(_initator, _other, _magnitude);
        collisionResult.EnemyType = GetType(collisionResult.Mass, collisionResult.Solid);
        collisionResult.FullyConsumed = IsFullyConsumed(_initator, _other, _magnitude);
        return collisionResult;
    }

    public virtual InitiatorCollisionResult GetResult(EnemyStats initator, EnemyStats other, float magnitude)
    {
        return InitiatorCollisionResult.otherDestroyed;
    }

    public virtual float GetMass(EnemyStats initator, EnemyStats other, float magnitude)
    {
        return initator.mass + other.mass;
    }

    public virtual float GetSolid(EnemyStats initator, EnemyStats other, float magnitude)
    {
        return (initator.mass * initator.solidValue
            + other.mass * other.solidValue) / (initator.mass + other.mass);
    }

    public virtual EnemyType GetType(float mass, float solid)
    {
        return EnemyTypeCounter.GetEnemyType(mass, solid);
    }

    public virtual bool IsFullyConsumed(EnemyStats initator, EnemyStats other, float magnitude)
    {
        return true;
    }

}
