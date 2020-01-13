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
    [SerializeField]
    private GameObject[] staticGameObjects;

    public EnemyObjectSpawnSettings[] dynamicEnemyObjectSpawnSettings;

    public float fromPlayerToSpawnMax = 15;
    public float fromPlayerToSpawnMin = 10;
    public float rangeToCount = 100;
    #endregion
    public bool CanSpawn { get; set; }

    public Dictionary<Guid, AllActiveObjectsData> AllActiveObjects { get; private set; }
    [SerializeField]
    public List<ShowAllData> showAllData = new List<ShowAllData>();
    private void Awake()
    {
        instance = instance ?? this;
        AllActiveObjects = new Dictionary<Guid, AllActiveObjectsData>();
        staticGameObjects = new GameObject[startingEnemyObjectSpawnSettings.Length];
    }


    void Start()
    {
        //add all existing objects to AllActiveObjects
        
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
        if (AllActiveObjects.ContainsKey(activeObjectData.objectId))
        {
            RemoveObject(activeObjectData.objectId);
        }
        AllActiveObjects.Add(activeObjectData.objectId, activeObjectData);
       // UpdateList();
    }
    public void RemoveObject(Guid objectId)
    {
        AllActiveObjects.Remove(objectId);
       // UpdateList();
    }



    private void GenerateStartingEnemy()
    {
        //print
        for (int i = 0; i < startingEnemyObjectSpawnSettings.Length; i++)
        {
            staticGameObjects[i]= ObjectPoolList.instance.
                GeneratePositionedObject(startingEnemyObjectSpawnSettings[i].enemyName, new Vector3(startingEnemyObjectSpawnSettings[i].xPosition
                , startingEnemyObjectSpawnSettings[i].yPosition, 0), Quaternion.identity, false);
        }
        //foreach (var item in startingEnemyObjectSpawnSettings)
        //{
        //   GameObject generated=  ObjectPoolList.instance.
        //        GetPooledObject(item.enemyName, new Vector3(item.xPosition, item.yPosition, 0), Quaternion.identity, true, false);
        //    staticGameObjects.Add(generated);
        //}
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
            //if (!MainCount.instance.
            //IsOutRanged(item.transform, AllObjectData.instance.go.transform, ConstsLibrary.maxObjectDistance))
            //{
            //item.SetActive(true);
            //}

                item.SetActive(!MainCount.instance.IsOutRanged(item.transform, AllObjectData.instance.go.transform, Consts.maxObjectDistance));

        }
    }

    public void UpdateList()
    {
        showAllData.Clear();
        foreach (var item in AllActiveObjects.Values)
        {
            showAllData.Add(new ShowAllData
            {
                go=item.go,
                mass=item.mass,
                objectId=item.objectId.ToString()
            });
        }

    }
    private void OnDestroy()
    {
        MainCount.instance.TimerEverySecond -= GenerateDynamicEnemy;
        MainCount.instance.TimerEverySecond -= GenerateStaticEnemy;
    }
}
