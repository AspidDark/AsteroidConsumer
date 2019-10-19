using System;
using System.Collections;
using System.Collections.Generic;
using TimB;
using UnityEngine;

public class EnemyAttractorHeavyObject : EnemyAttractorBase
{
    public EnemyAttractorHeavyObject(GameObject go, EnemyStats stats):base(go, stats)
    {
        MainCount.instance.TimerEverySecond += DoAttarct;
    }
    protected virtual void DoAttarct(object sender, EventArgs e)
    {
        //Attrct here
    }
    private void OnDisable()
    {
        UnSign();
    }

    private void OnDestroy()
    {
        UnSign();
    }

    private void UnSign()
    {
        MainCount.instance.TimerEverySecond -= DoAttarct;
    }

}
