using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TimB;

public class EnemyParametersLibrary : MonoBehaviour {

    public static EnemyParametersLibrary instance;
    private void Awake()
    {
        instance = instance ?? this;
    }

    public EnemyParametaers [] enemyParametaers;

    public EnemyParametaers GetEnemyParametaers(SpaceBodyType enemyType)
    {
        List<EnemyParametaers> parametaersOfType = enemyParametaers.Where(x => x.enemyType == enemyType).ToList();
        int objectToSpawn = MainCount.instance.IntegerRandom(0, parametaersOfType.Count);
        return parametaersOfType[objectToSpawn];
    }
}
