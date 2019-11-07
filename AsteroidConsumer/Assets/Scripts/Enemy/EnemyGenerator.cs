using System;
using System.Collections.Generic;
using System.Linq;
using TimB;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public static EnemyGenerator instance;

    public float objectSpawnOffset = 2;

    #region HeightCheck
    public EnemyObjectSpawnSettings[] startingEnemyObjectSpawnSettings;
    private List<GameObject> staticGameObjects;
    public EnemyObjectSpawnSettings[] dynamicEnemyObjectSpawnSettings;

    public float fromPlayerToSpawnMax = 15;
    public float fromPlayerToSpawnMin = 10;
    public float rangeToCount = 100;
    #endregion
    public bool CanSpawn { get; set; }

    public List<AllActiveObjectsData> AllActiveObjects { get; private set; }
    private void Awake()
    {
        AllActiveObjects = new List<AllActiveObjectsData>();
    }


    void Start()
    {
        instance = instance ?? this;
        staticGameObjects = new List<GameObject>();
        MainCount.instance.TimerEverySecond += GenerateDynamicEnemy;
        MainCount.instance.TimerEverySecond += GenerateStaticEnemy;
        if (startingEnemyObjectSpawnSettings == null || startingEnemyObjectSpawnSettings.Length == 0)
        {
            return;
        }
        else
        {
            GenerateStartingEnemy();
        }
      //  HeightCheckTimerUpdate();

    }

    public void AddObject(AllActiveObjectsData activeObjectData)
    {
        if(!AllActiveObjects.Contains(activeObjectData))
        AllActiveObjects.Add(activeObjectData);
    }
    public void RemoveObject(AllActiveObjectsData activeObjectData)
    {
        AllActiveObjects.Remove(activeObjectData);
    }



    private void GenerateStartingEnemy()
    {
        foreach (var item in startingEnemyObjectSpawnSettings)
        {
           GameObject generated=  ObjectPoolList.instance.
                GetPooledObject(item.enemyName, new Vector3(item.xPosition, item.yPosition, 0), Quaternion.identity, true, false);
            staticGameObjects.Add(generated);
        }
        GenerateStaticEnemy(new object(), new EventArgs());
    }
    //проверяем позицию игрока и если он близко к краю сгенеренного поля генерим еще объекты
    // есть объекты которые если далеко исчезают но при приближении игрока появляются но в пул передаются их фиксированные координаты
    //

   
        //не нравится...
    private void GenerateDynamicEnemy(object sender, EventArgs e)
    {
        if (CanSpawn)
        {
            //What object to generate
            int enemyToSpawn = MainCount.instance.DifferentWeightRandom(dynamicEnemyObjectSpawnSettings.Select(x => x.spawnChance).ToArray());
            //generate up?
            float enemyYposition = AllObjectData.instance.posY + (MainCount.instance.FloatRandom(fromPlayerToSpawnMin, fromPlayerToSpawnMax) + AllIndependentData.instance.cameraYHeight)
                *(MainCount.instance.BoolRandom() ? 1 : -1);
            //generate Left??
            float enemyXposition = AllObjectData.instance.posX + (MainCount.instance.FloatRandom(fromPlayerToSpawnMin, fromPlayerToSpawnMax) + AllIndependentData.instance.cameraXWidth)
                * (MainCount.instance.BoolRandom() ? 1 : -1);
            ObjectPoolList.instance.GetPooledObject(dynamicEnemyObjectSpawnSettings[enemyToSpawn].enemyName, new Vector3(enemyXposition, enemyYposition, 0), Quaternion.identity, true);
        }
    }


    private void GenerateStaticEnemy(object sender, EventArgs e)
    {
        foreach (var item in staticGameObjects)
        {
            item.SetActive(!MainCount.instance.
            IsOutRanged(item.transform, AllObjectData.instance.go.transform, ConstsLibrary.maxObjectDistance));
        }
    }

    //public void HeightCheckTimerUpdate()
    //{
    //    foreach (var item in dynamicEnemyObjectSpawnSettings)
    //    {
    //        item.heightCheckTimer = item.timeBetweenHeightChecks;
    //    }
    //}

    //private void SpawnFlyingObject(string enemyName)
    //{
    //    //print(poolName);
    //    //Geting List of Objects whitch can be spawned on player's Height
    //    List<EnemyObjectSpawnSettings> listOfObjectsOnThisHeight = new List<EnemyObjectSpawnSettings>();
    //    var listOfAirObjects = enemyObjectSpawnSettings.FirstOrDefault(n => n.enemyName == enemyName);
    //    foreach (var item in listOfAirObjects.airObjectList)
    //    {
    //        if (AllObjectData.instance.posY <= item.maxHeight
    //            && AllObjectData.instance.posY >= item.minHeight)
    //        {
    //            listOfObjectsOnThisHeight.Add(item);
    //        }
    //    }
    //    //If no object found
    //    if (listOfObjectsOnThisHeight == null || listOfObjectsOnThisHeight.Count == 0)
    //    {
    //        return;
    //    }
    //    //Object flies from left to right?
    //    float objectXposition;
    //    if (MainCount.instance.BoolRandom())
    //    {
    //        objectXposition = -nodeInformer.cameraXWidth - objectSpawnOffset;
    //    }
    //    else
    //    {
    //        objectXposition = nodeInformer.cameraXWidth + objectSpawnOffset;
    //    }
    //    int[] airObjectWeights = new int[listOfObjectsOnThisHeight.Count];
    //    for (int i = 0; i < listOfObjectsOnThisHeight.Count; i++)
    //    {
    //        airObjectWeights[i] = listOfObjectsOnThisHeight[i].spawnChance;
    //    }
    //    int numberOfObjectToSpawn = MainCount.instance.DifferentWeightRandom(airObjectWeights);//mainCount.IntegerRandom(0, listOfObjectsOnThisHeight.Count);

    //    float startingYposToSpawn = MainCount.instance.FloatRandom(higherFromPlayerToSpawnMin, higherFromPlayerToSpawnMax);

    //    if ((AllObjectData.instance.gameobjectVelocity.x * AllObjectData.instance.gameobjectVelocity.x > ConstsLibrary.speedSquareAferSpawnObjectOnMinHeight))
    //    {
    //        startingYposToSpawn = MainCount.instance.FloatRandom(-ConstsLibrary.heightToSpawnWhenXisHigh, ConstsLibrary.heightToSpawnWhenXisHigh);

    //    }
    //    if (AllObjectData.instance.gameobjectVelocity.y < 0 && AllObjectData.instance.posY > higherFromPlayerToSpawnMax)
    //    {
    //        startingYposToSpawn *= -1;
    //    }
    //    string name = listOfObjectsOnThisHeight[numberOfObjectToSpawn].objectName;
    //    float xPosition;
    //    if (AllObjectData.instance.gameobjectVelocity.x >= 0)
    //    {
    //        if (objectXposition < 0)
    //        {
    //            xPosition = AllObjectData.instance.posX + objectXposition;
    //        }
    //        else
    //        {
    //            xPosition = AllObjectData.instance.posX + objectXposition + AllObjectData.instance.gameobjectVelocity.x;
    //        }
    //    }
    //    else
    //    {
    //        if (objectXposition > 0)
    //        {
    //            xPosition = AllObjectData.instance.posX + objectXposition;
    //        }
    //        else
    //        {
    //            xPosition = AllObjectData.instance.posX + objectXposition + AllObjectData.instance.gameobjectVelocity.x;
    //        }
    //    }


    //    Vector3 position = new Vector3(xPosition//AllObjectData.instance.posX + objectXposition + AllObjectData.instance.gameobjectVelocity.x
    //        , AllObjectData.instance.posY + startingYposToSpawn + AllObjectData.instance.gameobjectVelocity.y);
    //    objectPoolList.GetPooledObject(name, position, Quaternion.identity, true); ///SpawningObject
    //}

    private void OnDestroy()
    {
        MainCount.instance.TimerEverySecond -= GenerateDynamicEnemy;
        MainCount.instance.TimerEverySecond -= GenerateStaticEnemy;
    }
}
