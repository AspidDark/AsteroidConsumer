using UnityEngine;
using TimB;
using System;

public class EnemyBaseEngine : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public EnemyScriptable enemyScriptable;
    public EnemyMovement enemyMovement;
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
        enemyMovement = enemyMovement ?? gameObject.GetComponent<EnemyMovement>();
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
        MainCount.instance.TimerEverySecond += MakeGravity;
        MainCount.instance.TimerEverySecond += CheckDestroy;
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
        CountMovement();
      // enemyMovement.CountAll(enemyScriptable.speedMin, enemyScriptable.speedMax);
        //IsMoveing = true;
        enemyMovement.Move(rb2d, stats);

    }
    private void OnDisable()
    {
        MainCount.instance.TimerEverySecond -= MakeGravity;
        MainCount.instance.TimerEverySecond -= CheckDestroy;
        if (ClosestObject.instance == null)
        {
            return;
        }
        ClosestObject.instance.RemoveForomArray(this.gameObject);
        // IsMoveing = false;
        RemoveForece();
      //  transform.localScale = new Vector3(1, 1, 1);
        CancelInvoke();
    }
   

    protected void CheckDestroy(object sender, EventArgs e)
    {
        if ((Mathf.Abs(this.transform.position.x - AllObjectData.instance.posX) > AllIndependentData.instance.cameraXWidth * 2) || IsAcceptableDistance())
        {
            Deactivate();
        }
    }

    protected void MakeGravity(object sender, EventArgs e)
    {
        AttractAll();
    }

    private bool IsAcceptableDistance()
    {
        return Vector3.Distance(this.transform.position, AllObjectData.instance.go.transform.position) > ConstsLibrary.maxObjectDistance;
    }

    private void Deactivate(bool fullyConsumed=false)
    {
        if (!fullyConsumed&&enemyScriptable.replacedByNames.Length > 0)
        {
            SpawnReplacers();
        }
        gameObject.SetActive(false);
    }

    private void SpawnReplacers()
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

    public void OnCollisionEnter2D(Collision2D collision)
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
    private void Rize(CollisionResult collisionResult)
    {
        ChangeMass(collisionResult.Mass);
        ChangeSolid(collisionResult.Solid);
        ChangeType(collisionResult.EnemyType);
    }



    private void ChangeMass(float newMass)
    {
        rb2d.mass = newMass;
        stats.mass = newMass;
    }

    private void ChangeSolid(float newSolid)
    {
        stats.solidValue = newSolid;
    }

    private void ChangeType(EnemyType newEnemyType)
    {
        if (stats.enemyType != newEnemyType)
        {
            EnemyAbiliteesCounter enemyAbiliteesCounter = new EnemyAbiliteesCounter();
            EnemyAbiliteesDTO enemyAbiliteesDTO = enemyAbiliteesCounter.GetEnemyAbilitees(newEnemyType);
            stats.hasGavity = enemyAbiliteesDTO.hasGavity;
            stats.gravityRange = enemyAbiliteesDTO.gravityRange;
            stats.gravityValue = enemyAbiliteesDTO.gravityValue;
        }
    }

    private void RemoveForece()
    {
        rb2d.velocity = Vector3.zero;
        rb2d.angularVelocity = 0;
    }


    private void CountMovement()
    {
        stats.xSpeed = MainCount.instance.FloatRandom(enemyScriptable.speedMin, enemyScriptable.speedMax);
        stats.ySpeed = MainCount.instance.FloatRandom(enemyScriptable.speedMin, enemyScriptable.speedMax);
        if (stats.isRandomMovement)
        {
            stats.moveRight = MainCount.instance.BoolRandom();
            stats.moveUp = MainCount.instance.BoolRandom();
        }
        else
        {
            stats.moveRight = AllObjectData.instance.posX > gameObject.transform.position.x;
            stats.moveUp = AllObjectData.instance.posY> gameObject.transform.position.y;
        }
        print("moveRight check" + stats.moveRight);
        print("moveUp check" + stats.moveUp);
    }

    private void AttractAll()
    {

    }

}
