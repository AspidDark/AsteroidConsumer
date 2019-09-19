using UnityEngine;
using UnityEngine.UI;
namespace TimB
{
    [CreateAssetMenu(fileName = "New BackGround", menuName = "MenuItems/BackGround")]
    public class BackGroundScriptable : ScriptableObject
    {
        public bool isScrolling;
        public bool isParralax;
        public float parallaxSpeedX;
        public float parallaxSpeedY;
        public float spriteXSize;
        public string sortingLayerName;
        [Space]
        public Sprite[] sprites = new Sprite[3];

    }
}
