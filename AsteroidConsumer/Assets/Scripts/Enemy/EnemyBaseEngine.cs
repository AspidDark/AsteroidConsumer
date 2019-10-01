using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimB;

public class EnemyBaseEngine : MonoBehaviour
{

    public Rigidbody2D rb2d;
    public EnemyScriptable enemyScriptable;
    public EnemyMovement enemyMovement;

    private IEnemyBehavior behavior;

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
        rb2d.mass = MainCount.instance.FloatRandom(enemyScriptable.enemyMassMin, enemyScriptable.enemyMassMax);

    }

    private void OnEnable()
    {
        EnableInitiation();
    }

    private void EnableInitiation()
    {
        enemyMovement.CountAll(enemyScriptable.speedMin, enemyScriptable.speedMax);
        enemyMovement.IsMoveing = true;

    }
    private void OnDisable()
    {
        if (ClosestObject.instance == null)
        {
            return;
        }
        ClosestObject.instance.RemoveForomArray(this.gameObject);
        enemyMovement.IsMoveing = false;
        transform.localScale = new Vector3(1, 1, 1);
        CancelInvoke();
    }
    // Update is called once per frame
    void Update()
    {

    }

  


    private void CheckDestroy()
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
        gameObject.SetActive(false);
    }
}
