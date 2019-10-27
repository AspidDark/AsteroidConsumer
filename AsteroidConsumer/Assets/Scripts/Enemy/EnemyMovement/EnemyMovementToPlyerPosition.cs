using TimB;
using UnityEngine;

public class EnemyMovementToPlyerPosition : EnemyMovementToSpecilaPosition
{
    public override void Move(Rigidbody2D rb2d, EnemyStats stats)
    {
        toXPosition = AllObjectData.instance.posX;
        toYPosition = AllObjectData.instance.posY;
        base.Move(rb2d, stats);
    }

}
