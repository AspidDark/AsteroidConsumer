using UnityEngine;

[CreateAssetMenu(fileName = "New SpwanSettings", menuName = "MenuItems/EnemySpwanSettings")]
public class EnemyObjectSpawnSettings : ScriptableObject
{
    public string enemyName;
    public float xPosition;
    public float yPosition;

    public int spawnChance;
}
