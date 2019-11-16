using UnityEngine;

public class EnemyAttractorEngine : MonoBehaviour {

    private EnemyAttractorBase _enemyAttractorBase;
    public void Attract(GameObject go, EnemyStats stats)
    {
        _enemyAttractorBase = go.GetComponent<EnemyAttractorBase>();
        if (_enemyAttractorBase == null)
        {
            if (stats.enemyType > EnemyType.moon)
            {
                _enemyAttractorBase = go.AddComponent<EnemyAttractorHeavyObject>();
            }
            else
            {
            _enemyAttractorBase = go.AddComponent<EnemyAttractorBase>();
            }
        }
        _enemyAttractorBase.StartAttraction(go, stats);
    }

}
