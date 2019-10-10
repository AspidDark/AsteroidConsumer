public class CollisionResultConsumeAndDestroy : ColllisionResultGeneratorBase
{
    public CollisionResultConsumeAndDestroy(EnemyStats initator, EnemyStats other, float magnitude) : base(initator, other, magnitude) 
    {
    }

    public override float GetMass(EnemyStats initator, EnemyStats other, float magnitude)
    {
        return initator.mass+other.mass*other.consumePercentage;
    }

    public override float GetSolid(EnemyStats initator, EnemyStats other, float magnitude)
    {
        return (initator.mass * initator.solidValue
             + other.mass * other.consumePercentage * other.solidValue * Consts.consumeAndDestroyCompressionMulipluer) 
             / (initator.mass + other.mass* other.consumePercentage);
    }
    public override bool IsFullyConsumed(EnemyStats initator, EnemyStats other, float magnitude)
    {
        return false;
    }
}
