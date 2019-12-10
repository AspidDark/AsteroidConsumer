using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualEffects : MonoBehaviour {

    public static PlayerVisualEffects instance;
    //public GameObject spriteShower;
    //private void Awake()
    //{
    //    instance = instance ?? this;
    //}
    ///// <summary>
    ///// Decrease constantly
    ///// </summary>
    ///// <param name="value"></param>
    ///// <param name="tr"></param>
    //public void SizeDecreaser(float value, Transform tr)
    //{
    //    float decreaseValue = value / 10;
    //  //  float colliderRadiusDecreaser = 
    //    PlayerStats.instance.circleCollider.radius -= PlayerStats.instance.circleCollider.radius * decreaseValue;
    //    // Vector3 gameObjectSacaleDecreaser = go.transform.localScale * decreaseValue;
    //    tr.localScale -= tr.localScale * decreaseValue;
    //    spriteShower.transform.localScale = new Vector3(1, 1, 1);

    //}
    ///// <summary>
    ///// Only for showing
    ///// </summary>
    ///// <param name="value"></param>
    ///// <param name="tr"></param>
    //public void DecreaseVisually(float value, Transform tr)
    //{
    //   // Vector3 gameObjectSacaleDecreaser = tr.localScale * value / 10;
    //    spriteShower.transform.localScale = tr.localScale*(10 -  value )/10 ;
    //}
}
