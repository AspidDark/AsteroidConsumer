using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float impulseMuliplyer=4;

    public void Move(Rigidbody2D rb2d, EnemyStats enemyStats)
    {
        MoveX(rb2d, enemyStats);
        MoveY(rb2d, enemyStats);
    }

    public virtual void MoveX(Rigidbody2D rb2d, EnemyStats enemyStats)
    {
        if (enemyStats.moveRight)
        {
            rb2d.AddForce(Vector3.right* enemyStats.xSpeed * impulseMuliplyer, ForceMode2D.Impulse);
        }
        else
        {

            rb2d.AddForce(Vector3.left* enemyStats.xSpeed * impulseMuliplyer, ForceMode2D.Impulse);
        }
    }
    public virtual void MoveY(Rigidbody2D rb2d, EnemyStats enemyStats)
    {
        if (enemyStats.moveUp)
        {
            rb2d.AddForce(Vector3.up * enemyStats.ySpeed * impulseMuliplyer, ForceMode2D.Impulse);
        }
        else
        {
            rb2d.AddForce(Vector3.down * enemyStats.ySpeed * impulseMuliplyer, ForceMode2D.Impulse);
        }
    }
  
}
