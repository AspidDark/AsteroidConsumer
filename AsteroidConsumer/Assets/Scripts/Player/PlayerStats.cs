using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public float PlayerMass { get; set; }



    #region debug
    public float playerMassShow;
    private void Update()
    {
        playerMassShow = PlayerMass;
    }
    #endregion

}
