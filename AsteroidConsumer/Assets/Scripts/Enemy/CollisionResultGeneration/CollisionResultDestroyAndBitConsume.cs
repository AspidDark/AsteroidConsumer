public class CollisionResultDestroyAndBitConsume : ColllisionResultGeneratorBase
{
    public CollisionResultDestroyAndBitConsume(EnemyStats initator, EnemyStats other, float magnitude) : base(initator, other, magnitude)
    {

    }

    public override InitiatorCollisionResult GetResult(EnemyStats initator, EnemyStats other, float magnitude)
    {
        if (magnitude > Consts.minMagnitudeValueToInteract)
        {
            return InitiatorCollisionResult.otherDestroyed;
        }
        return InitiatorCollisionResult.noAction;
    }
    public override float GetMass(EnemyStats initator, EnemyStats other, float magnitude)
    {
        return initator.mass + other.mass * other.consumePercentage* initator.consumePercentage;
    }

    public override float GetSolid(EnemyStats initator, EnemyStats other, float magnitude)
    {
        return (initator.mass * initator.solidValue
             + other.mass * other.consumePercentage * initator.consumePercentage * other.solidValue * Consts.destroyAndBitConsumeCompressionMulipluer) 
             / (initator.mass + other.mass * other.consumePercentage * initator.consumePercentage);
    }
    public override bool IsFullyConsumed(EnemyStats initator, EnemyStats other, float magnitude)
    {
        return false;
    }
}
