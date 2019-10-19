using UnityEngine;
using TimB;
using System;

public class EnemyBaseEngine : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public EnemyScriptable enemyScriptable;
    public EnemyMovementBase enemyMovement;
    public EnemyAttractorEngine enemyAttractor;
    
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
        enemyAttractor.Attract(gameObject, stats);
    }


    //void FixedUpdate()
    //{
    //    if (IsMoveing)
    //    {
    //        enemyMovement.Move();
    //    }
    //}

    private void OnEnable()
    {
        EnableInitiation();
        BaseDistanseCheck baseDistanseCheck = new BaseDistanseCheck(this);
        MainCount.instance.TimerEverySecond += MakeGravity;
       
    }

    private void EnableInitiation()
    {
        float mass = MainCount.instance.FloatRandom(enemyScriptable.enemyMassMin, enemyScriptable.enemyMassMax);
        rb2d.mass = mass;
        stats.mass = mass;
        stats.consumePercentage = MainCount.instance.FloatRandom(enemyScriptable.minConsumeValue, enemyScriptable.maxConsumeValue)/100;
        stats.solidValue = MainCount.instance.FloatRandom(enemyScriptable.minSolidValue, enemyScriptable.maxSolidValue);
        stats.consumeType = enemyScriptable.consumeType;
        stats.enemyType = EnemyTypeCounter.GetEnemyType(mass, stats.solidValue);
        ChangeType(stats.enemyType);
       // CountMovement();
        enemyMovement.Move(rb2d, stats, enemyScriptable);//3
      

    }
    private void OnDisable()
    {
        MainCount.instance.TimerEverySecond -= MakeGravity;//1
        
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
   
    protected void MakeGravity(object sender, EventArgs e)//1
    {
        AttractAll();
    }

    public void Deactivate(bool fullyConsumed=false)//2
    {
        if (!fullyConsumed&&enemyScriptable.replacedByNames.Length > 0)
        {
            SpawnReplacers();
        }
        gameObject.SetActive(false);
    }

    private void SpawnReplacers()//2
    {
        foreach (var item in enemyScriptable.replacedByNames)
        {
            if (!string.IsNullOrEmpty(item))
            {
                //to do moveing to differernt ways
                ObjectPoolList.instance.GetPooledObject(item, gameObject.transform.position, gameObject.transform.rotation);
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
               // if()
                EnemyBaseEngine otherEngine= collision.gameObject.GetComponent<EnemyBaseEngine>();
                //float add mass
                enemyFactory = new EnemyFactory(stats, otherStats, collision.relativeVelocity.magnitude);
                var result= enemyFactory.GetCollisionResult();
                switch (result.initiatorCollisionResult)
                {
                    case InitiatorCollisionResult.otherDestroyed:
                        Rize(result);
                        otherEngine.Deactivate(result.FullyConsumed);
                        break;
                    case InitiatorCollisionResult.noAction:
                        //do nothing???
                        break;
                    case InitiatorCollisionResult.bouthDestroyed:
                        otherEngine.Deactivate();
                        Deactivate();
                        break;
                    case InitiatorCollisionResult.initiatorDestroyed:
                        otherEngine.Rize(result);//&&
                        Deactivate(result.FullyConsumed);
                        break;
                    default:
                        break;
                }
                otherEngine.Deactivate();
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
        if (stats.enemyType != newEnemyType)
        {
            EnemyAbiliteesCounter enemyAbiliteesCounter = new EnemyAbiliteesCounter();
            EnemyAbiliteesDTO enemyAbiliteesDTO = enemyAbiliteesCounter.GetEnemyAbilitees(newEnemyType);
            stats.hasGavity = enemyAbiliteesDTO.hasGavity;
            stats.gravityRange = enemyAbiliteesDTO.gravityRange;
            stats.gravityValue = enemyAbiliteesDTO.gravityValue;
            enemyAttractor.Attract(gameObject, stats);
        }
    }

    //private void RemoveForece()//3
    //{
        
    //}


    //private void CountMovement()//3
    //{
    //    stats.xSpeed = MainCount.instance.FloatRandom(enemyScriptable.speedMin, enemyScriptable.speedMax);
    //    stats.ySpeed = MainCount.instance.FloatRandom(enemyScriptable.speedMin, enemyScriptable.speedMax);
    //    if (stats.isRandomMovement)
    //    {
    //        stats.moveRight = MainCount.instance.BoolRandom();
    //        stats.moveUp = MainCount.instance.BoolRandom();
    //    }
    //    else
    //    {
    //        stats.moveRight = AllObjectData.instance.posX > gameObject.transform.position.x;
    //        stats.moveUp = AllObjectData.instance.posY> gameObject.transform.position.y;
    //    }
    //    print("moveRight check" + stats.moveRight);
    //    print("moveUp check" + stats.moveUp);
    //}

    private void AttractAll()
    {

    }

}
