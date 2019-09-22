using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public Rigidbody2D hook;


    public float releaseTime = .15f;
    public float maxDragDistance = 2f;
    public float canInteractTime = 2f;

    private bool canInteract = true;
    private bool isPressed = false;
    private bool realeaseCanInteract = false;

    private SpringJoint2D springJoint2D;

    public float forceMultypuer;

    public Vector2 showVector2;

    #region Dreving aim and circle
    LineRenderer circle;
    #endregion

    private void Start()
    {
        springJoint2D = GetComponent<SpringJoint2D>();
    }

    void FixedUpdate()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
            else
                rb.position = mousePos;
        }
    }


    void OnMouseDown()
    {
        Pressed();
    }


    public void Pressed()
    {
        if (canInteract)
        {
            realeaseCanInteract = true;
            hook.transform.position = gameObject.transform.position;
            springJoint2D.connectedAnchor = hook.transform.position;
            springJoint2D.enabled = true;
            RemoveForece();
            this.enabled = true;
            circle = hook.gameObject.DrawCircle(maxDragDistance, 0.05f, Color.red, Color.red);
            isPressed = true;
            rb.isKinematic = true;
        }
    }

    void OnMouseUp()
    {
        Released();
    }
    public void Released()
    {
        if (realeaseCanInteract)
        {
            realeaseCanInteract = false;
            isPressed = false;
            rb.isKinematic = false;
            springJoint2D.enabled = false;
            circle.positionCount = 0;
            AddForce();
            canInteract = false;
            StartCoroutine(IteractTimer());
        }
    }



    IEnumerator IteractTimer()
    {
        yield return new WaitForSeconds(canInteractTime);
        canInteract = true;
    }

    private void AddForce()
    {
        float distance = Vector2.Distance(gameObject.transform.position, hook.transform.position);
        Vector2 direction = (gameObject.transform.position - hook.transform.position).normalized * forceMultypuer * (distance + 1) * (distance + 1);
        showVector2 = direction;
        rb.AddForce(direction, ForceMode2D.Impulse);
        //Дальше юзаем замедление....
    }

    private void RemoveForece()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
    }
}
