using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyVisualAndSizeSettings", menuName = "MenuItems/EnemyVisualAndSizeSettings")]
public class EnemyParametaers : ScriptableObject
{
    public EnemyType enemyType;
    public float colliderRadius;
    public Sprite sprite;
}
