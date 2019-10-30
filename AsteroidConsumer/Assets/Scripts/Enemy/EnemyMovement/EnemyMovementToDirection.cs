using UnityEngine;

public class EnemyMovementToDirection : EnemyMovementBase
{
    public override void Move(Rigidbody2D rb2d, EnemyStats stats)
    {
        rb2d.AddForce(new Vector2(stats.xSpeed, stats.ySpeed) * impulseMuliplyer * stats.mass, ForceMode2D.Impulse);
        //* impulseMuliplyer * enemyStats.mass
    }
}
