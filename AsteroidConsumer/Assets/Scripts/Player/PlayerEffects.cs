using System.Collections;
using System.Collections.Generic;
using TimB;
using UnityEngine;

public class PlayerEffects : MonoBehaviour {

    public static PlayerEffects instance;
    public GameObject spriteShower;
    private void Awake()
    {
        instance = instance ?? this;
    }
    /// <summary>
    /// Decrease constantly
    /// </summary>
    /// <param name="value"></param>
    /// <param name="tr"></param>
    public void SizeAndMassDecreaser(float value, Transform tr)
    {
        float decreaseValue = value / 10;
        PlayerStats.instance.Mass -= PlayerStats.instance.Mass * decreaseValue;
        CheckMass();
    }
    /// <summary>
    /// Only for showing
    /// </summary>
    /// <param name="value"></param>
    /// <param name="tr"></param>
    public void DecreaseVisually(float value, Transform tr)
    {
        float decreaseValue = (10 - value) / 10;
        spriteShower.transform.localScale = new Vector3(decreaseValue, decreaseValue, decreaseValue);
    }

    public void RetirnVisualEffect(Transform tr)
    {
        spriteShower.transform.localScale = new Vector3(1,1,1);
    }

    public void GrowerUp(EnemyStats eatenEnemyStats, int enemyEatType)
    {
        switch (enemyEatType)
        {
            case 0:
                PlayerStats.instance.SolidValue = (PlayerStats.instance.SolidValue * PlayerStats.instance.Mass
                    + eatenEnemyStats.mass * eatenEnemyStats.solidValue * Consts.playerFullConsumeSolidMultyplyer) / (PlayerStats.instance.Mass + eatenEnemyStats.mass);
                PlayerStats.instance.Mass += eatenEnemyStats.mass * Consts.playerFullConsumeMassMultyplyer;
                break;
            case 1:
                PlayerStats.instance.SolidValue = (PlayerStats.instance.SolidValue * PlayerStats.instance.Mass
                   + eatenEnemyStats.mass * eatenEnemyStats.solidValue * Consts.playerPartialConsumeSolidMultyplyer) / (PlayerStats.instance.Mass + eatenEnemyStats.mass);
                PlayerStats.instance.Mass += eatenEnemyStats.mass * Consts.playerPartialConsumeMassMultyplyer * eatenEnemyStats.consumePercentage;
                break;
            default:
                break;
        }
    }

    public void CheckMass()
    {
        SpaceBodyType currentBotyType = PlayerStats.instance.spaceBodyType;
        var newBodyType = MapMassToBodyType();
        if (currentBotyType != newBodyType)
        {
            ChangeBodyTypeAccordingToMass(currentBotyType, newBodyType);
            PlayerStats.instance.spaceBodyType = newBodyType;
        }
    }

    private SpaceBodyType MapMassToBodyType()
    {
        for (int i = 0; i < Consts.mapBodyTypeToMass.Length; i++)
        {
            if (PlayerStats.instance.Mass < Consts.mapBodyTypeToMass[i])
            {
                return (SpaceBodyType)(i-1);
            }
        }
        return SpaceBodyType.gigantBlackHole;
    }

    private void ChangeBodyTypeAccordingToMass(SpaceBodyType currntBodyType, SpaceBodyType newBodyType)
    {
        float difference = ((float)newBodyType -(float)currntBodyType) / 10;
        AllObjectData.instance.go.transform.localScale = new Vector3(AllObjectData.instance.go.transform.localScale.x+difference, 
            AllObjectData.instance.go.transform.localScale.y+difference, AllObjectData.instance.go.transform.localScale.z+difference);
    }

    public void CheckSolid(float solidValue)
    {
        PlayerStats.instance.spriteRenderer.color = VisualEffectHelper.instance.GetFinalColor(PlayerStats.instance.minSolidColor, PlayerStats.instance.growSpeed, solidValue);
    }
}
