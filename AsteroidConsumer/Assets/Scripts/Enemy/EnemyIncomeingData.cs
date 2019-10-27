using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIncomeingData : MonoBehaviour {
    public bool isSecondGeneratedObject = false;

    [Range(Consts.minMass, Consts.maxMass)]
    public float enemyMassMin;
    [Range(Consts.minMass, Consts.maxMass)]
    public float enemyMassMax;


    [Range(Consts.minSpeed, Consts.maxSpeed)]
    public float speedMin;
    [Range(Consts.minSpeed, Consts.maxSpeed)]
    public float speedMax;

    public float xSpeed;
    public float ySpeed;

    public bool moveUp;
    public bool moveRight;


}
