﻿using UnityEngine;
using TimB;
using System;

public class EnemyBaseEngine : MonoBehaviour
{
    public Vector2 shower;

    public Rigidbody2D rb2d;
    public EnemyScriptable enemyScriptable;
    public EnemyMovementBase enemyMovement;
    public EnemyAttractorEngine enemyAttractor;
    /// <summary>
    /// If enemy is second or more generated
    /// </summary>
    public EnemyIncomeingData incomeingData;//отсюда данные тянуть или из scriptable

    public EnemyStats stats;

    //private IEnemyBehavior behavior;
    private EnemyFactory enemyFactory;
    // public bool IsMoveing { get; set; }

    // Use this for initialization
    void Start()
    {

        StartingInitiation();
    }
    private void StartingInitiation()
    {
        enemyMovement = enemyMovement ?? gameObject.GetComponent<EnemyMovementBase>();
        enemyAttractor = enemyAttractor ?? gameObject.GetComponent<EnemyAttractorEngine>();
        EnableInitiation();
        enemyAttractor.Attract(gameObject, stats);
    }

    private void OnEnable()
    {
        EnableInitiation();
    }

    private void EnableInitiation()
    {
        if (MainCount.instance != null)
        {
            float mass;
            if (incomeingData != null && incomeingData.isSecondGeneratedObject)
            {
                mass = MainCount.instance.FloatRandom(incomeingData.enemyMassMin, incomeingData.enemyMassMax);
                stats.xSpeed = incomeingData.xSpeed;
                stats.ySpeed = incomeingData.ySpeed;
                stats.moveRight = incomeingData.moveRight;
                stats.moveUp = incomeingData.moveUp;
            }
            else
            {
                mass = MainCount.instance.FloatRandom(enemyScriptable.enemyMassMin, enemyScriptable.enemyMassMax);
                stats.speedMin = enemyScriptable.speedMin;
                stats.speedMax = enemyScriptable.speedMax;
            }
            rb2d.mass = mass;
            stats.mass = mass;
            stats.consumePercentage = MainCount.instance.FloatRandom(enemyScriptable.minConsumeValue, enemyScriptable.maxConsumeValue) / 100;
            stats.solidValue = MainCount.instance.FloatRandom(enemyScriptable.minSolidValue, enemyScriptable.maxSolidValue);
            stats.consumeType = enemyScriptable.consumeType;
            stats.enemyType = EnemyTypeCounter.GetEnemyType(mass, stats.solidValue);
            stats.isRandomMovement = enemyScriptable.isRandomMpvement;
            ChangeType(stats.enemyType);
            
            // CountMovement();
            enemyMovement.Move(rb2d, stats);

            BaseDistanseCheck baseDistanseCheck = new BaseDistanseCheck(this);
           // MainCount.instance.TimerEverySecond += MakeGravity;
        }


    }
    private void OnDisable()
    {
        //MainCount.instance.TimerEverySecond -= MakeGravity;//1

        if (ClosestObject.instance == null)
        {
            return;
        }
        ClosestObject.instance.RemoveForomArray(this.gameObject);
        // IsMoveing = false;
        enemyMovement.RemoveForece(rb2d);//3
                                         //  transform.localScale = new Vector3(1, 1, 1);
        CancelInvoke();
    }


    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Deactivate(Vector2 collisionPoint)
    {

        if (enemyScriptable.replacedByNames.Length > 0)
        {
            SpawnReplacers(collisionPoint);
            //TO DO set to new objects     public bool moveUp;  public bool moveRight;
            // move to different locations!!!
        }
        Deactivate();
    }

    private void SpawnReplacers(Vector2 collisionPoint)//2
    {
        foreach (var item in enemyScriptable.replacedByNames)
        {
            if (!string.IsNullOrEmpty(item))
            {
                //В объект передать EnemyIncomeingData считать его тут...
                //to do moveing to differernt ways
                ObjectPoolList.instance.GetPooledObjectWithData(item, gameObject.transform.position, gameObject.transform.rotation, "TestText", true, false);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)//2
    {
        if (!collision.gameObject.CompareTag(TagLibrary.playerTag))
        {
            EnemyStats otherStats = collision.gameObject.GetComponent<EnemyStats>();
            if (stats.mass > otherStats.mass)
            {
                Vector2 collisionPoint = collision.contacts[0].point;
                EnemyBaseEngine otherEngine = collision.gameObject.GetComponent<EnemyBaseEngine>();
                enemyFactory = new EnemyFactory(stats, otherStats, collision.relativeVelocity.magnitude);
                var result = enemyFactory.GetCollisionResult();
                switch (result.initiatorCollisionResult)
                {
                    case InitiatorCollisionResult.otherDestroyed:
                        Rize(result);

                        if (result.FullyConsumed) { otherEngine.Deactivate();}
                        else { otherEngine.Deactivate(collisionPoint); }

                        break;
                    case InitiatorCollisionResult.noAction:
                        //do nothing???
                        break;
                    case InitiatorCollisionResult.bouthDestroyed:
                        otherEngine.Deactivate(collisionPoint);
                        Deactivate(collisionPoint);
                        break;
                    case InitiatorCollisionResult.initiatorDestroyed:
                        otherEngine.Rize(result);//&&

                        if (result.FullyConsumed) { Deactivate();}
                        else { Deactivate(collisionPoint);}

                        break;
                    default:
                        break;
                }
                //otherEngine.Deactivate();
            }

        }
    }
    private void Rize(CollisionResult collisionResult)//2
    {
        ChangeMass(collisionResult.Mass);
        ChangeSolid(collisionResult.Solid);
        ChangeType(collisionResult.EnemyType);
    }



    private void ChangeMass(float newMass)//2
    {
        rb2d.mass = newMass;
        stats.mass = newMass;
    }

    private void ChangeSolid(float newSolid)//2
    {
        stats.solidValue = newSolid;
    }

    private void ChangeType(EnemyType newEnemyType)//2
    {
        //if (stats.enemyType != newEnemyType)
        //{
            EnemyAbiliteesCounter enemyAbiliteesCounter = new EnemyAbiliteesCounter();
            EnemyAbiliteesDTO enemyAbiliteesDTO = enemyAbiliteesCounter.GetEnemyAbilitees(newEnemyType);
            stats.hasGavity = enemyAbiliteesDTO.hasGavity;
            stats.gravityRange = enemyAbiliteesDTO.gravityRange;
            stats.gravityValue = enemyAbiliteesDTO.gravityValue;
            enemyAttractor.Attract(gameObject, stats);
       // }
    }
 

    private void AttractAll()
    {

    }

}
