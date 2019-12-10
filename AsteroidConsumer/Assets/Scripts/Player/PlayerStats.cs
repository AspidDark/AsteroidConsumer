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
    public CircleCollider2D circleCollider;

    #region staring Values
    public float startingMass = 30;
    public float startingRadius = 0.3f;

    public float startMaxDragDistance = 2f;
    public float startcanInteractTime = 2f;

    public float startingSolidValue = 10;

    public float forceMultypuer = 2f;
    #endregion

    #region currentValues
    private float maxDragDistance;
    public float MaxDragDistance
    {
        get { return maxDragDistance; }

        set { maxDragDistance = value; }
    }

    private float canInteractTime;
    public float CanInteractTime
    {
        get { return canInteractTime; }

        set { canInteractTime = value; }
    }
    private float mass;

    public float Mass
    {
        get { return mass; }
        set { mass = value; rb.mass = value; }
    }
    private float radius;

    public float Radius
    {
        get { return radius; }
        set { radius = value; circleCollider.radius = value; }
    }

    private float solidValue;

    public float SolidValue
    {
        get { return solidValue; }
        set { solidValue = value; }
    }

    #endregion

    private void Awake()
    {
        instance = instance ?? this;
        StartingInitiation();
    }


    private void StartingInitiation()
    {
        rb = rb ?? GetComponent<Rigidbody2D>();
        hookRenderer = hookRenderer ?? hook.GetComponent<LineRenderer>();
        hookRigitbody = hookRigitbody ?? hook.GetComponent<Rigidbody2D>();
        hookAimRenderer = hookAimRenderer ?? hookAim.GetComponent<LineRenderer>();
        SetStaringValues();

    }
    private void SetStaringValues()
    {
        Mass = startingMass;
        Radius = startingRadius;
        MaxDragDistance = startMaxDragDistance;
        CanInteractTime = startcanInteractTime;
        SolidValue = startingSolidValue;
    }

}
