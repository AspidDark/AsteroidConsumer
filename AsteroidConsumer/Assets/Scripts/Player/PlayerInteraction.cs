using System.Collections;
using System.Collections.Generic;
using TimB;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    public static PlayerInteraction instance;


    private const float playerStrongMutipluer = 1.5f;
    private const float playerAlsoStrongMutipluer = 2f;
    private const float massiveButNonSolidMass = 8f;
    private const float massiveButNonSolidSloidValue = 3f;

    private void Awake()
    {
        instance = instance ?? this;

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyBaseEngine enemyBaseEngine= collision.gameObject.GetComponent<EnemyBaseEngine>();
        if (enemyBaseEngine == null)
        {
            return;
        }
        if (enemyBaseEngine.stats.mass * enemyBaseEngine.stats.solidValue / playerStrongMutipluer < PlayerStats.instance.Mass * PlayerStats.instance.SolidValue)
        {
            FullConsume(enemyBaseEngine.stats);
            enemyBaseEngine.Deactivate();
            return;
        }
       Vector2 collisionPoint = collision.contacts[0].point;
        if (enemyBaseEngine.stats.mass * enemyBaseEngine.stats.solidValue / playerAlsoStrongMutipluer < PlayerStats.instance.Mass * PlayerStats.instance.SolidValue)
        {
            PartialConsume(enemyBaseEngine.stats);
            enemyBaseEngine.Deactivate(collisionPoint);
            return;
        }
        if (enemyBaseEngine.stats.mass / massiveButNonSolidMass< PlayerStats.instance.Mass
            && enemyBaseEngine.stats.solidValue* massiveButNonSolidSloidValue < PlayerStats.instance.SolidValue)
        {
           // Vector2 collisionPoint = collision.contacts[0].point;
            PartialConsume(enemyBaseEngine.stats);
            enemyBaseEngine.Deactivate(collisionPoint);
            return;
        }
        Vector2 jumpBackDirection = (Vector2)AllObjectData.instance.go.transform.position - collisionPoint;
        print("Jump Back");
        PlayerMovement.instance.AddForce(jumpBackDirection.normalized);
        LoseMass();
        //Jump back losing mass//




    }

    private void FullConsume(EnemyStats enemyStats)
    {
        PlayerEffects.instance.GrowerUp(enemyStats, 0);
    }

    private void PartialConsume(EnemyStats enemyStats)
    {
        PlayerEffects.instance.GrowerUp(enemyStats, 1);
    }

    private void LoseMass()
    {
        PlayerEffects.instance.SizeAndMassDecreaser(1, AllObjectData.instance.go.transform);
    }
}
