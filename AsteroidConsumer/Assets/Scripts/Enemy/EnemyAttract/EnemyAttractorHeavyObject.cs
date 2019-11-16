using System;
using TimB;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class EnemyAttractorHeavyObject : EnemyAttractorBase
{
    public override void StartAttraction(GameObject go, EnemyStats stats)
    {
        base.StartAttraction(go, stats);
        MainCount.instance.TimerEvery250Millisecond += DoAttarct;
    }
 

    protected virtual void DoAttarct(object sender, EventArgs e)
    {
        float force = CountForce();
        var goInAttractionRange = EnemyGenerator.instance.AllActiveObjects.
            Where(x => !MainCount.instance.IsOutRanged(x.Value.go.transform, _go.transform, _stats.gravityRange)
            &&(x.Value.objectId!=_stats.objectId))
            .Select(x=>x.Value);//.ToDictionary(x => x.Key, x => x.Value).Values.ToList();
        //TO DO Attrct here
        foreach (var item in goInAttractionRange)
        {
            AddForce(item, force);
        }
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


    private float CountForce()
    {
       return _stats.gravityValue / _stats.gravityRange;
    }

    public const float every250MillisecondMultypuer = .25f;

    private void AddForce(AllActiveObjectsData intracted, float force)
    {
        Vector3 toPosition= (new Vector3(_go.transform.position.x, _go.transform.position.y, 0)
            - new Vector3(intracted.rb2d.transform.position.x, intracted.rb2d.transform.position.y, 0)).normalized;
        intracted.rb2d.AddForce(toPosition * force * intracted.mass* every250MillisecondMultypuer, ForceMode2D.Impulse); 
        //TO DO ForceMode to Force??
    }



}
