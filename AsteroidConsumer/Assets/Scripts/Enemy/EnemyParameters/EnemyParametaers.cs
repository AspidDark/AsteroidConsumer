using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyVisualAndSizeSettings", menuName = "MenuItems/EnemyVisualAndSizeSettings")]
public class EnemyParametaers : ScriptableObject
{
    public SpaceBodyType enemyType;
    public float colliderRadius;
    public Sprite sprite;
    public bool isChangingColor;
}
