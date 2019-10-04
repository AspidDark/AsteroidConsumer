using UnityEngine;
using TimB;
using System;

public class EnemyBaseEngine : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public EnemyScriptable enemyScriptable;
    public EnemyMovement enemyMovement;
    public EnemyStats stats;

    private IEnemyBehavior behavior;

    public bool IsMoveing { get; set; }

    // Use this for initialization
    void Start()
    {
        StartingInitiation();
    }
    private void StartingInitiation()
    {
        enemyMovement = enemyMovement ?? gameObject.GetComponent<EnemyMovement>();
        EnemyFactory enemyFactory = new EnemyFactory(enemyScriptable);
        behavior = enemyFactory.GetBehaior();

      

    }

    void FixedUpdate()
    {
        if (IsMoveing)
        {
            enemyMovement.Move();
        }
    }

    private void OnEnable()
    {
        EnableInitiation();
        MainCount.instance.TimerEverySecond += CheckDestroy;
    }

    private void EnableInitiation()
    {
        float mass = MainCount.instance.FloatRandom(enemyScriptable.enemyMassMin, enemyScriptable.enemyMassMax);
        rb2d.mass = mass;
        stats.mass = mass;
        stats.consumePercentage = MainCount.instance.FloatRandom(enemyScriptable.minConsumeValue, enemyScriptable.maxConsumeValue);
        stats.solidValue = MainCount.instance.FloatRandom(enemyScriptable.minSolidValue, enemyScriptable.maxSolidValue);
        stats.enemyType = enemyScriptable.enemyType;
        stats.consumeType = enemyScriptable.consumeType;
        stats.hasGavity = enemyScriptable.hasGavity;
        if (stats.hasGavity)
        {
            stats.gravityValue = MainCount.instance.FloatRandom(enemyScriptable.minGravityValue, enemyScriptable.maxGravityValue);
        }

        enemyMovement.CountAll(enemyScriptable.speedMin, enemyScriptable.speedMax);
        IsMoveing = true;

    }
    private void OnDisable()
    {
        MainCount.instance.TimerEverySecond -= CheckDestroy;
        if (ClosestObject.instance == null)
        {
            return;
        }
        ClosestObject.instance.RemoveForomArray(this.gameObject);
        IsMoveing = false;
      //  transform.localScale = new Vector3(1, 1, 1);
        CancelInvoke();
    }
   

    private void CheckDestroy(object sender, EventArgs e)
    {
        if ((Mathf.Abs(this.transform.position.x - AllObjectData.instance.posX) > AllIndependentData.instance.cameraXWidth * 2) || IsAcceptableDistance())
        {
            Destroy();
        }
    }
    private bool IsAcceptableDistance()
    {
        return Vector3.Distance(this.transform.position, AllObjectData.instance.go.transform.position) > ConstsLibrary.maxObjectDistance;
    }

    private void Destroy()
    {
        if (enemyScriptable.replacedByNames.Length > 0)
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
                //to do moveing ti differernt ways
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
                //do
            }
        }
    }


}
