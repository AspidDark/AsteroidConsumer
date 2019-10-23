using TimB;
using UnityEngine;

public class EnemyMovementBase : MonoBehaviour {

    public float impulseMuliplyer = 1;

    public virtual void Move(Rigidbody2D rb2d, EnemyStats stats, EnemyScriptable enemyScriptable)
    {
        stats.xSpeed = GetRandomSpeed(enemyScriptable);
        stats.ySpeed = GetRandomSpeed(enemyScriptable);
        if (stats.isRandomMovement)
        {
            stats.moveRight = MainCount.instance.BoolRandom();
            stats.moveUp = MainCount.instance.BoolRandom();
        }
        else
        {
            //to sceeen movement
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
        float speed = enemyStats.xSpeed * impulseMuliplyer * enemyStats.mass;
        if (enemyStats.moveRight)
        {
            rb2d.AddForce(Vector3.right * speed, ForceMode2D.Impulse);
        }
        else
        {
            rb2d.AddForce(Vector3.left * speed, ForceMode2D.Impulse);
        }
    }
    protected virtual void MoveY(Rigidbody2D rb2d, EnemyStats enemyStats)
    {
        float speed = enemyStats.xSpeed * impulseMuliplyer * enemyStats.mass;
        if (enemyStats.moveUp)
        {
            rb2d.AddForce(Vector3.up * speed, ForceMode2D.Impulse);
        }
        else
        {
            rb2d.AddForce(Vector3.down * speed, ForceMode2D.Impulse);
        }
    }

    public void RemoveForece(Rigidbody2D rb2d)
    {
        rb2d.velocity = Vector3.zero;
        rb2d.angularVelocity = 0;
    }


    protected float GetRandomSpeed(EnemyScriptable enemyScriptable)
    {
        return MainCount.instance.FloatRandom(enemyScriptable.speedMin, enemyScriptable.speedMax);
    }

}
