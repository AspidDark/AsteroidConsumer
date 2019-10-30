using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttractorEngine : MonoBehaviour {

    private EnemyAttractorBase _enemyAttractorBase;
    public void Attract(GameObject go, EnemyStats stats)
    {
        //if (stats.hasGavity)
        //{
        //    _enemyAttractorBase = new EnemyAttractorHeavyObject(go, stats);
        //}
        //else
        //{
        //    _enemyAttractorBase = new EnemyAttractorBase(go, stats);
        //}
    }

}
