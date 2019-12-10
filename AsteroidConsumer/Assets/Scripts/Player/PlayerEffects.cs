using System.Collections;
using System.Collections.Generic;
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
        //  float colliderRadiusDecreaser = 
        PlayerStats.instance.circleCollider.radius -= PlayerStats.instance.circleCollider.radius * decreaseValue;
        // Vector3 gameObjectSacaleDecreaser = go.transform.localScale * decreaseValue;
        tr.localScale -= tr.localScale * decreaseValue;
        spriteShower.transform.localScale = new Vector3(1, 1, 1);
        PlayerStats.instance.Mass -= PlayerStats.instance.Mass * decreaseValue;

    }
    /// <summary>
    /// Only for showing
    /// </summary>
    /// <param name="value"></param>
    /// <param name="tr"></param>
    public void DecreaseVisually(float value, Transform tr)
    {
        // Vector3 gameObjectSacaleDecreaser = tr.localScale * value / 10;
        spriteShower.transform.localScale = tr.localScale * (10 - value) / 10;
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
        CheckMassAndSolid();

        //add mass
        //add solid
        //check mass=> change size
    }


    private void CheckMassAndSolid()
    {
        //В зависимости от массы увеличиваем размер
        // в зависимости от плотности меняем цвет
    }
}
