public class EnemyFactory
{
    private ColllisionResultGeneratorBase _colllisionResultGeneratorBase;
    //Generating enemy type by factory  //Chooseing actions is strategy?
    public EnemyFactory(EnemyStats initator, EnemyStats other, float magnitude)
    {
        _colllisionResultGeneratorBase = ChoseCollisionResultType(initator, other, magnitude);
    }
    //Use di here???
    private ColllisionResultGeneratorBase ChoseCollisionResultType(EnemyStats initator, EnemyStats other, float magnitude)
    {

        if (initator.mass / other.mass > 10)
        {
            return new ColllisionResultFullConsumeBase(initator, other, magnitude);
        }
        if (initator.mass / other.mass > 6)
        {
            return new CollisionResultConsumeAndDestroy(initator, other, magnitude);
        }
        if (initator.mass / other.mass > 4)
        {
            return new CollisionResultDestroyAndBitConsume(initator, other, magnitude);
        }
        return new CollisionResultStruggleForLife(initator, other, magnitude);
    }

    public CollisionResult GetCollisionResult()
    {
      return  _colllisionResultGeneratorBase.GetCollisionResult();
    }
}
