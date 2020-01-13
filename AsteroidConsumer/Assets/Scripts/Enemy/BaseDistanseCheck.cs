using System;
using TimB;
using UnityEngine;

public class BaseDistanseCheck  {

    private EnemyBaseEngine _enemyBaseEngine;
    public BaseDistanseCheck(EnemyBaseEngine enemyBaseEngine)
    {
        _enemyBaseEngine = enemyBaseEngine;
        MainCount.instance.TimerEverySecond += CheckDestroy;//2
    }

    protected virtual void CheckDestroy(object sender, EventArgs e)//2
    {
        if (/*(Mathf.Abs(_enemyBaseEngine.transform.position.x - AllObjectData.instance.posX) > AllIndependentData.instance.cameraXWidth * 4) ||*/
            MainCount.instance.
            IsOutRanged(_enemyBaseEngine.transform, AllObjectData.instance.go.transform, Consts.maxObjectDistance))
        {
            MainCount.instance.TimerEverySecond -= CheckDestroy;
            _enemyBaseEngine.Deactivate();
        }
    }
}
