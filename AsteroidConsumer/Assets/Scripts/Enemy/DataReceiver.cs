using UnityEngine;
using TimB;

public class DataReceiver : DataReciverBase
{
    public float minAngle=40;
    public override void ReceiveData(BaseDTO data)
    {
        EnemyToReplacersDTO incData = data as EnemyToReplacersDTO;
        Vector2 collisionDirection= (incData.collisionPoint - (Vector2)(incData.destroyedEnemyPosition));
        Vector2 spawnPoint;
        if (incData.numberOfObject%2==0)
        {
            spawnPoint = collisionDirection.GetRotated(MainCount.instance.FloatRandom(minAngle, 180-minAngle));
        }
        else
        {
            spawnPoint = collisionDirection.GetRotated(MainCount.instance.FloatRandom(-180 + minAngle, -minAngle));
        }
        gameObject.transform.position = new Vector2(incData.destroyedEnemyPosition.x+ spawnPoint.x, incData.destroyedEnemyPosition.y + spawnPoint.y);
        Vector2 moveDirection = spawnPoint.normalized;

        EnemyBaseEngine enemyBaseEngine = gameObject.GetComponent<EnemyBaseEngine>();
        enemyBaseEngine.incomeingData.xSpeed = moveDirection.x;
        enemyBaseEngine.incomeingData.ySpeed = moveDirection.y;
        enemyBaseEngine.incomeingData.isSecondGeneratedObject = true;
        gameObject.SetActive(true);
    }
}
