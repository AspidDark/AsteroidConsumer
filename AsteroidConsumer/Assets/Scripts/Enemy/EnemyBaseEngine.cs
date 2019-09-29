using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseEngine : MonoBehaviour {

    public EnemyScriptable enemyScriptable;
    private IEnemyBehavior behavior;
	// Use this for initialization
	void Start () {
        EnemyFactory enemyFactory = new EnemyFactory(enemyScriptable);
        behavior = enemyFactory.GetBehaior();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
