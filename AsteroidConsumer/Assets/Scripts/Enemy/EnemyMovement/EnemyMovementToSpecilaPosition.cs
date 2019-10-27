using UnityEngine;

public class EnemyMovementToSpecilaPosition : EnemyMovementBase
{
    public float toXPosition;
    public float toYPosition;

    public override void Move(Rigidbody2D rb2d, EnemyStats stats)
    {
        Vector3 toPosition = (new Vector3(toXPosition, toYPosition, 0) - new Vector3(rb2d.transform.position.x, rb2d.transform.position.y, 0)).normalized;
        rb2d.AddForce(toPosition * impulseMuliplyer * GetRandomSpeed(stats) *stats.mass, ForceMode2D.Impulse); 
    }

}
