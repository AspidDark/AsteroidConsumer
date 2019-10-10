public class CollisionResultStruggleForLife : ColllisionResultGeneratorBase
{
    public CollisionResultStruggleForLife(EnemyStats initator, EnemyStats other, float magnitude) : base(initator, other, magnitude)
    {
    }

    private InitiatorCollisionResult initiatorCollisionResult;

    public override InitiatorCollisionResult GetResult(EnemyStats initator, EnemyStats other, float magnitude)
    {
        if (initator.solidValue > other.solidValue)
        {
            if (initator.mass * initator.solidValue > other.mass * other.solidValue * Consts.hiegherSolid)
            {
                return InitiatorCollisionResult.otherDestroyed;
            }
        }
        else
        {
            if (other.solidValue > initator.solidValue * Consts.muchHigherSolid)
            {
                return InitiatorCollisionResult.initiatorDestroyed;
            }
        }

        if (magnitude > Consts.minMagnitudeValueToInteractIfStruggle)
        {
        return InitiatorCollisionResult.bouthDestroyed;
        }
        return InitiatorCollisionResult.noAction;
    }

    public override float GetMass(EnemyStats initator, EnemyStats other, float magnitude)
    {
        float consumeDecreaser = other.consumePercentage * initator.consumePercentage;
        switch (initiatorCollisionResult)
        {
            case InitiatorCollisionResult.otherDestroyed:
                return initator.mass + other.mass * consumeDecreaser;
            case InitiatorCollisionResult.initiatorDestroyed:
                return other.mass + initator.mass * consumeDecreaser;
            default:
                return 0;
        }
    }

    public override bool IsFullyConsumed(EnemyStats initator, EnemyStats other, float magnitude)
    {
        return false;
    }

}
