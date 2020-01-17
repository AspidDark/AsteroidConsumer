using TimB;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    public static PlayerInteraction instance;


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
        PlayerInteractionEffect playerInteractionEffect = PlayerInteractionCounter.GerInteractionResult(enemyBaseEngine);
        switch (playerInteractionEffect)
        {
            case PlayerInteractionEffect.fullConsume:
                FullConsume(enemyBaseEngine.stats);
                enemyBaseEngine.Deactivate();
                return;
            case PlayerInteractionEffect.partialConsume:
                Vector2 collisionPointPC = collision.contacts[0].point;
                PartialConsume(enemyBaseEngine.stats);
                enemyBaseEngine.Deactivate(collisionPointPC);
                return;
        }
        Vector2 collisionPoint = collision.contacts[0].point;
        // if (enemyBaseEngine.stats.mass * enemyBaseEngine.stats.solidValue / Consts.playerStrongMutipluer < PlayerStats.instance.Mass * PlayerStats.instance.SolidValue)
        // {
        //     FullConsume(enemyBaseEngine.stats);
        //     enemyBaseEngine.Deactivate();
        //     return;
        // }
        //Vector2 collisionPoint = collision.contacts[0].point;
        // if (enemyBaseEngine.stats.mass * enemyBaseEngine.stats.solidValue / Consts.playerAlsoStrongMutipluer < PlayerStats.instance.Mass * PlayerStats.instance.SolidValue)
        // {
        //     PartialConsume(enemyBaseEngine.stats);
        //     enemyBaseEngine.Deactivate(collisionPoint);
        //     return;
        // }
        // if (enemyBaseEngine.stats.mass / Consts.massiveButNonSolidMass < PlayerStats.instance.Mass
        //     && enemyBaseEngine.stats.solidValue* Consts.massiveButNonSolidSloidValue < PlayerStats.instance.SolidValue)
        // {
        //    // Vector2 collisionPoint = collision.contacts[0].point;
        //     PartialConsume(enemyBaseEngine.stats);
        //     enemyBaseEngine.Deactivate(collisionPoint);
        //     return;
        // }
        Vector2 jumpBackDirection = (Vector2)AllObjectData.instance.go.transform.position - collisionPoint;
        print("Jump Back");
        PlayerMovement.instance.AddForce(jumpBackDirection.normalized);
        LoseMass(); //Jump back losing mass//
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
