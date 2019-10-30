using UnityEngine;
using TimB;

public class DataReceiver : MonoBehaviour {

    public float minAngle=30;
    public void ReceiveData(object data)
    {
        EnemyToReplacersDTO incData = data as EnemyToReplacersDTO;
        // float radius = Vector2.Distance(incData.collisionPoint, incData.destroyedEnemyPosition);
        print("DataReceiver");
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
        //считаем напрвыление движения spawnPoint.normilize*на импульс
        //EnemyIncomeingData enemyIncomeingData = new EnemyIncomeingData
        //{
        //    xSpeed= moveDirection.x,
        //    ySpeed= moveDirection.y

        //};
        // вставляем в энждин EnemyIncomeingData всю инфу массу плотность скорсть всавляем в энджин Активируем объект
        EnemyBaseEngine enemyBaseEngine = gameObject.GetComponent<EnemyBaseEngine>();
        enemyBaseEngine.incomeingData.xSpeed = moveDirection.x;
        enemyBaseEngine.incomeingData.ySpeed = moveDirection.y;
        enemyBaseEngine.incomeingData.isSecondGeneratedObject = true;
        gameObject.SetActive(true);
    }
}
