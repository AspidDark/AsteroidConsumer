using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float mass;
    public float consumePercentage;
    public float solidValue;

    public EnemyType enemyType;
    public ConsumeType consumeType;

    public bool hasGavity;
    public float gravityValue;
    public float gravityRange;

    [Header("Movement Settings")]
    public bool isRandomMovement;
    public float xSpeed;
    public float ySpeed;

    public bool moveUp;
    public bool moveRight;

}
