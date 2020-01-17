using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "MenuItems/Enemy")]
public class EnemyScriptable : ScriptableObject
{
    #region Enemy settings
    public string enemyName;
    public string[] replacedByNames;

    [Range(Consts.minMass, Consts.maxMass)]
    public float enemyMassMin;
    [Range(Consts.minMass, Consts.maxMass)]
    public float enemyMassMax;

    [Range(Consts.minSpeed, Consts.maxSpeed)]
    public float speedMin;
    [Range(Consts.minSpeed, Consts.maxSpeed)]
    public float speedMax;

    public bool isRandomMovement;

    [Range(Consts.minConsumePercentage, Consts.maxConsumePercentage)]
    public float minConsumeValue;
    [Range(Consts.minConsumePercentage, Consts.maxConsumePercentage)]
    public float maxConsumeValue;

    [Range(Consts.minSolidValue, Consts.maxSolidValue)]
    public float minSolidValue;
    [Range(Consts.minSolidValue, Consts.maxSolidValue)]
    public float maxSolidValue;
    //Variant
    [Space]
    public bool changeDirection;
    [Range(Consts.minChangeDirectionSpeed, Consts.maxChangeDirectionSpeed)]
    public float changeDirectionSpeedMin;
    [Range(Consts.minChangeDirectionSpeed, Consts.maxChangeDirectionSpeed)]
    public float changeDirectionSpeedMax;
    [Space]
    public bool hasGavity;
    [Range(Consts.minGravityValue, Consts.maxGravityValue)]
    public float minGravityValue;
    [Range(Consts.minGravityValue, Consts.maxGravityValue)]
    public float maxGravityValue;



    #endregion

    #region Enemy Type
    public SpaceBodyType enemyType;
    public ConsumeType consumeType;
    #endregion


}
