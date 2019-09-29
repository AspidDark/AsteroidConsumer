using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    IEnemyBehavior behavior;
    public EnemyFactory(EnemyScriptable enemyScriptable)
    {

    }
    //Generating enemy type by factory  //Chooseing actions is strategy?
    public IEnemyBehavior GetBehaior()
    {
        EnemyBehavior enb = new EnemyBehavior();
        return enb;
    }

}
