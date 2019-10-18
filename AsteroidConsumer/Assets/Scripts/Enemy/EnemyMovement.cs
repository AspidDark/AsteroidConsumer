using UnityEngine;

public class EnemyMovement : EnemyMovementBase
{
    protected override void MoveX(Rigidbody2D rb2d, EnemyStats enemyStats)
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
    protected override void MoveY(Rigidbody2D rb2d, EnemyStats enemyStats)
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
