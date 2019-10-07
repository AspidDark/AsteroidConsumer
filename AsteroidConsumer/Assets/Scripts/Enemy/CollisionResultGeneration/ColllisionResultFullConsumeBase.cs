public class ColllisionResultFullConsumeBase : ColllisionResultGeneratorBase
{
    public ColllisionResultFullConsumeBase(EnemyStats initator, EnemyStats other, float magnitude) : base(initator, other, magnitude)
    {
    }
    public override float GetSolid(EnemyStats initator, EnemyStats other, float magnitude)
    {
        return (initator.mass * initator.solidValue
            + other.mass * other.solidValue*Consts.fullConsumeCompressionMulipluer) / (initator.mass + other.mass);
    }

    //public CollisionResult GetResult(EnemyStats initator, EnemyStats other)
    //{
    //    CollisionResult collisionResult = new CollisionResult();
    //    collisionResult.initiatorCollisionResult = InitiatorCollisionResult.otherDestroyed;
    //    collisionResult.Mass = initator.mass + other.mass;
    //    collisionResult.Solid = (initator.mass * initator.solidValue 
    //        + other.mass * other.solidValue*Consts.fullConsumeCompressionMulipluer) / collisionResult.Mass;

    //    collisionResult.EnemyType = EnemyTypeCounter.GetEnemyType(collisionResult.Mass, collisionResult.Solid);
    //    collisionResult.FullyConsumed = true;
    //    return collisionResult;
    //}
}
