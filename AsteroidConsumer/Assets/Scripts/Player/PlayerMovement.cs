using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody2D rb;
    public Rigidbody2D hook;


    public float releaseTime = .15f;
    public float maxDragDistance = 2f;
    public float canInteractTime = 2f;

    private bool canInteract = true;
    private bool isPressed = false;

    private bool isRunOnce=true;
    private SpringJoint2D springJoint2D;

    public float forceMultypuer;

    public Vector2 showVector2;

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
        if (canInteract)
        {
            if (isRunOnce)
            {
                RemoveFoece();
                print("isRunOnce");
                isRunOnce = false;
                hook.transform.position = gameObject.transform.position;
                springJoint2D.connectedAnchor = hook.transform.position;
                springJoint2D.enabled = true;
                this.enabled = true;
            }
            isPressed = true;
            rb.isKinematic = true;
        }
    }

    void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        springJoint2D.enabled = false;
        
        AddForce();
        canInteract = false;
        isRunOnce = true;

        StartCoroutine(IteractTimer());
    }
    //при отпускании кнопки считаем расстояние между  хуком и объектом
    //от этого зависит скорость
    //добавить затуханее движения

    IEnumerator IteractTimer()
    {
       // yield return new WaitForSeconds(releaseTime);

       // springJoint2D.enabled = false;
       // this.enabled = false;

        yield return new WaitForSeconds(canInteractTime);
        canInteract = true;
    }

    private void AddForce()
    {
        Vector2 direction = (gameObject.transform.position - hook.transform.position).normalized * forceMultypuer;
        showVector2 = direction;
        rb.AddForce(direction, ForceMode2D.Impulse);
    }

    private void RemoveFoece()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
    }
}
