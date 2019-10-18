using TimB;
using UnityEngine;

public class EnemyMovementBase : MonoBehaviour {

    public float impulseMuliplyer = 4;

    public void Move(Rigidbody2D rb2d, EnemyStats stats, EnemyScriptable enemyScriptable)
    {
        stats.xSpeed = MainCount.instance.FloatRandom(enemyScriptable.speedMin, enemyScriptable.speedMax);
        stats.ySpeed = MainCount.instance.FloatRandom(enemyScriptable.speedMin, enemyScriptable.speedMax);
        if (stats.isRandomMovement)
        {
            stats.moveRight = MainCount.instance.BoolRandom();
            stats.moveUp = MainCount.instance.BoolRandom();
        }
        else
        {
            stats.moveRight = AllObjectData.instance.posX > gameObject.transform.position.x;
            stats.moveUp = AllObjectData.instance.posY > gameObject.transform.position.y;
        }
        print("moveRight check" + stats.moveRight);
        print("moveUp check" + stats.moveUp);
        MoveX(rb2d, stats);
        MoveY(rb2d, stats);
    }

    protected virtual void MoveX(Rigidbody2D rb2d, EnemyStats enemyStats)
    {
        //simple move here
    }
    protected virtual void MoveY(Rigidbody2D rb2d, EnemyStats enemyStats)
    {
        //simple move here
    }

    public void RemoveForece(Rigidbody2D rb2d)
    {
        rb2d.velocity = Vector3.zero;
        rb2d.angularVelocity = 0;
    }
}
