using UnityEngine;
using TimB;
using System;
using System.Collections;

public class EnemyBaseEngine : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public EnemyScriptable enemyScriptable;
    public EnemyMovementBase enemyMovement;
    public EnemyAttractorEngine enemyAttractor;
    /// <summary>
    /// If enemy is second or more generated
    /// </summary>
    public EnemyIncomeingData incomeingData;

    public EnemyStats stats;

    //private IEnemyBehavior behavior;
    private EnemyFactory enemyFactory;
    #region Sprite Size And Sound
    public SpriteRenderer spriteRenderer;
    public CircleCollider2D circleCollider;
    #endregion

    private void Awake()
    {
        stats.objectId = Guid.NewGuid();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
    }

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
        if (EnemyGenerator.instance != null)
        {
            AddObjectToAllObjectList();
        }
        else
        {
            StartCoroutine(AddObjectToObjectListTimedOut());
        }

        if (MainCount.instance != null)
        {
            float mass = MainCount.instance.FloatRandom(enemyScriptable.enemyMassMin, enemyScriptable.enemyMassMax);
            if (incomeingData != null && incomeingData.isSecondGeneratedObject)
            {
                stats.xSpeed = incomeingData.xSpeed;
                stats.ySpeed = incomeingData.ySpeed;
                stats.moveRight = incomeingData.moveRight;
                stats.moveUp = incomeingData.moveUp;
            }
            else
            {
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

            enemyMovement.Move(rb2d, stats);

            BaseDistanseCheck baseDistanseCheck = new BaseDistanseCheck(this);
        }
    }

    private IEnumerator AddObjectToObjectListTimedOut()
    {
        yield return new WaitForSeconds(.5f);
        AddObjectToAllObjectList();
    }

    private void AddObjectToAllObjectList()
    {
        EnemyGenerator.instance.AddObject(new AllActiveObjectsData
        {
            objectId = stats.objectId,
            go = gameObject,
            rb2d = rb2d,
            mass = stats.mass
        });
    }

    private void OnDisable()
    {
        EnemyGenerator.instance.RemoveObject(stats.objectId);
        enemyMovement.RemoveForece(rb2d);//3
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
        for (int i = 0; i < enemyScriptable.replacedByNames.Length; i++)
        {
            if (!string.IsNullOrEmpty(enemyScriptable.replacedByNames[i]))
            {
                EnemyToReplacersDTO enemyToReplacersDTO = new EnemyToReplacersDTO
                {
                    stats = stats,
                    destroyedEnemyPosition = this.transform.position,
                    collisionPoint = collisionPoint,
                    numberOfObject = i
                };
                ObjectPoolList.
                    instance.GetPooledObjectWithData(enemyScriptable.replacedByNames[i], gameObject.transform.position,
                  gameObject.transform.rotation, enemyToReplacersDTO, true, false);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)//2
    {
        if (collision == null)
        {
            return;
        }
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

                        if (result.FullyConsumed) { otherEngine.Deactivate(); }
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

                        if (result.FullyConsumed) { Deactivate(); }
                        else { Deactivate(collisionPoint); }

                        break;
                    default:
                        break;
                }
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

    private void ChangeType(SpaceBodyType newEnemyType)//2
    {
        EnemyAbiliteesCounter enemyAbiliteesCounter = new EnemyAbiliteesCounter();
        EnemyAbiliteesDTO enemyAbiliteesDTO = enemyAbiliteesCounter.GetEnemyAbilitees(newEnemyType);
        stats.hasGavity = enemyAbiliteesDTO.hasGavity;
        stats.gravityRange = enemyAbiliteesDTO.gravityRange;
        stats.gravityValue = enemyAbiliteesDTO.gravityValue;
        enemyAttractor.Attract(gameObject, stats);
        if (stats.enemyType != newEnemyType)
        {
            //image here
            stats.enemyType = EnemyTypeCounter.GetEnemyType(stats.mass, stats.solidValue);
            EnemyParametaers parametaers = EnemyParametersLibrary.instance.GetEnemyParametaers(stats.enemyType);
            spriteRenderer.sprite = parametaers.sprite;
            circleCollider.radius = parametaers.colliderRadius;
        }
    }
}
