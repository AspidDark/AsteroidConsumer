using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (initator.mass / other.mass > 1.5)
        {
            return new CollisionResultStruggleForLife(initator, other, magnitude);
        }
        return new CollisionResultDoSmthOrNot(initator, other, magnitude);
    }

    public CollisionResult GetCollisionResult()
    {
      return  _colllisionResultGeneratorBase.GetCollisionResult();
    }
}
