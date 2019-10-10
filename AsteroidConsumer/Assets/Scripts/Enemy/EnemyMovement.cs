using TimB;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float impulseMuliplyer=4;

    private float xSpeed;
    private float ySpeed;
    
    private bool moveUp;
    private bool moveRight;


    public virtual void CountAll(float minSpeed, float maxSpeed)
    {
        xSpeed = MainCount.instance.FloatRandom(minSpeed, maxSpeed);
        moveRight= MainCount.instance.BoolRandom();
        print("moveRight check" + moveRight);

        ySpeed = MainCount.instance.FloatRandom(minSpeed, maxSpeed);
        moveUp = MainCount.instance.BoolRandom();
        print("moveUp check" + moveUp);

    }

    public void Move(Rigidbody2D rb2d)
    {
        MoveX(rb2d);
        MoveY(rb2d);
    }

    public virtual void MoveX(Rigidbody2D rb2d)
    {
        if (moveRight)
        {
           // transform.Translate(Vector3.right * MainCount.instance.fixedDeltaTime * xSpeed);
            rb2d.AddForce(Vector3.right*xSpeed* impulseMuliplyer, ForceMode2D.Impulse);
        }
        else
        {

          //  transform.Translate(Vector3.left * MainCount.instance.fixedDeltaTime * xSpeed);
            rb2d.AddForce(Vector3.left*xSpeed* impulseMuliplyer, ForceMode2D.Impulse);
        }
    }
    public virtual void MoveY(Rigidbody2D rb2d)
    {
        if (moveUp)
        {
           // transform.Translate(Vector3.up * MainCount.instance.fixedDeltaTime * xSpeed);
            rb2d.AddForce(Vector3.up * xSpeed * impulseMuliplyer, ForceMode2D.Impulse);
        }
        else
        {
          //  transform.Translate(Vector3.down * MainCount.instance.fixedDeltaTime * xSpeed);
            rb2d.AddForce(Vector3.down * xSpeed * impulseMuliplyer, ForceMode2D.Impulse);
        }
    }
  
}
