using UnityEngine;
using TimB;

public class EnemyToReplacersDTO : BaseDTO {
    public EnemyStats stats;
    public Vector3 destroyedEnemyPosition;
    public Vector2 collisionPoint;
    public int numberOfObject;
}
