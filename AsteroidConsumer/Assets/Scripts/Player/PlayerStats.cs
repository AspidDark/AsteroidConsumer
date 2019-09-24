using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static PlayerStats instance;
    public Rigidbody2D rb;
    [Space]
    public GameObject hookAim;
    public LineRenderer hookAimRenderer;
    [Space]
    public GameObject hook;
    public Rigidbody2D hookRigitbody;
    public LineRenderer hookRenderer;

    public float PlayerMass
    {
        get
        { return playerMass; }
        set
        {
            playerMass = value;
            rb.mass = value;
        }
    }
    private float playerMass;


    private void Start()
    {
        StartingInitiation();
    }


    private void StartingInitiation()
    {
        instance = instance ?? this;
        rb = rb ?? GetComponent<Rigidbody2D>();
        hookRenderer = hookRenderer ?? hook.GetComponent<LineRenderer>();
        hookRigitbody = hookRigitbody ?? hook.GetComponent<Rigidbody2D>();
        hookAimRenderer = hookAimRenderer ?? hookAim.GetComponent<LineRenderer>();
    }


    #region debug
    public float playerMassShow;
    private void Update()
    {
        playerMassShow = PlayerMass;
    }
    #endregion

}
