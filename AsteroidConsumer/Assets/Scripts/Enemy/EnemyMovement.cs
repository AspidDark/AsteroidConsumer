using TimB;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private float xSpeed;
    private float ySpeed;
    
    public bool IsMoveing { get; set; }

    private bool moveUp;
    private bool moveRight;

    // Use this for initialization
    void Start () {
        //IsMoveing = false;
    }

    public void CountAll(float minSpeed, float maxSpeed)
    {
        xSpeed = MainCount.instance.FloatRandom(minSpeed, maxSpeed);
        moveRight= MainCount.instance.BoolRandom();
        print("moveRight check" + moveRight);

        ySpeed = MainCount.instance.FloatRandom(minSpeed, maxSpeed);
        moveUp = MainCount.instance.BoolRandom();
        print("moveUp check" + moveUp);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsMoveing)
        {
        MoveX();
        MoveY();
        }
    }

    public virtual void MoveX()
    {
        if (moveRight)
        {
            transform.Translate(Vector3.right * MainCount.instance.fixedDeltaTime * xSpeed);
        }
        else
        {
            transform.Translate(Vector3.left * MainCount.instance.fixedDeltaTime * xSpeed);
        }
    }
    public virtual void MoveY()
    {
        if (moveUp)
        {
            transform.Translate(Vector3.up * MainCount.instance.fixedDeltaTime * xSpeed);
        }
        else
        {
            transform.Translate(Vector3.down * MainCount.instance.fixedDeltaTime * xSpeed);
        }
    }
}
