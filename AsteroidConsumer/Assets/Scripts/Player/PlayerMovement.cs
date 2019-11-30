﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private LineRendererTool rendererTool = new LineRendererTool();
    #region Drawing aim and circle
    LineRenderer circle;
    LineRenderer line;
    public GameObject spriteShower;
    #endregion
    private SpringJoint2D springJoint2D;

    #region Private
    private float distance;

    private bool canInteract = true;
    private bool isPressed = false;
    private bool realeaseCanInteract = false;

    private bool countAimingLine = false;
    #endregion


    private void Awake()
    {
        instance = instance ?? this;
    }

    

    private void Start()
    {
        springJoint2D = GetComponent<SpringJoint2D>();
    }

    void FixedUpdate()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(mousePos, PlayerStats.instance.hookRigitbody.position) > PlayerStats.instance.MaxDragDistance)
                PlayerStats.instance.rb.position = PlayerStats.instance.hookRigitbody.position + (mousePos - PlayerStats.instance.hookRigitbody.position).normalized * PlayerStats.instance.MaxDragDistance;
            else
                PlayerStats.instance.rb.position = mousePos;
        }

        if (realeaseCanInteract)
        {
            line = rendererTool.DrawLine(PlayerStats.instance.hookAimRenderer, PlayerStats.instance.hook.transform.position, gameObject.transform.position, PlayerStats.instance.MaxDragDistance);
            #region Player SizeChanger
            distance = Vector3.Distance(PlayerStats.instance.hook.transform.position, gameObject.transform.position);
            DecreaseVisually(distance);
            #endregion
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
            countAimingLine = true;
            realeaseCanInteract = true;
            PlayerStats.instance.hookRigitbody.transform.position = gameObject.transform.position;
            springJoint2D.connectedAnchor = PlayerStats.instance.hookRigitbody.transform.position;
            springJoint2D.enabled = true;
            RemoveForece();
            this.enabled = true;
            circle = PlayerStats.instance.hookRigitbody.gameObject.DrawCircle(PlayerStats.instance.hookRenderer, PlayerStats.instance.MaxDragDistance, 0.05f, Color.red, Color.red);
            
            isPressed = true;
            PlayerStats.instance.rb.isKinematic = true;
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
            #region Player SizeChanger
            SizeDecreaser(distance);


            distance = 0;
            #endregion
            countAimingLine = false;
            realeaseCanInteract = false;
            isPressed = false;
            PlayerStats.instance.rb.isKinematic = false;
            springJoint2D.enabled = false;
            circle.positionCount = 0;
            // line.positionCount = 0;
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
            AddForce();
            canInteract = false;
            StartCoroutine(IteractTimer());
        }
    }



    IEnumerator IteractTimer()
    {
        yield return new WaitForSeconds(PlayerStats.instance.CanInteractTime);
        canInteract = true;
    }

    private void AddForce()
    {
        float distance = Vector2.Distance(gameObject.transform.position, PlayerStats.instance.hookRigitbody.transform.position);
        Vector2 direction = (gameObject.transform.position - PlayerStats.instance.hookRigitbody.transform.position).normalized * PlayerStats.instance.Mass * PlayerStats.instance.forceMultypuer * (distance + 1);// * (distance + 1);
        PlayerStats.instance.rb.AddForce(direction, ForceMode2D.Impulse);
        //Дальше юзаем замедление....
    }

    private void RemoveForece()
    {
        PlayerStats.instance.rb.velocity = Vector3.zero;
        PlayerStats.instance.rb.angularVelocity = 0;
    }

    private void SizeDecreaser(float value)
    {
        float decreaseValue = value / 10;
        float colliderRadiusDecreaser = PlayerStats.instance.circleCollider.radius * decreaseValue;
        PlayerStats.instance.circleCollider.radius -= colliderRadiusDecreaser;
        Vector3 gameObjectSacaleDecreaser = gameObject.transform.localScale * decreaseValue;
        gameObject.transform.localScale -= gameObjectSacaleDecreaser;
        spriteShower.transform.localScale = new Vector3(1,1,1);

    }

    private void DecreaseVisually(float value)
    {
        Vector3 gameObjectSacaleDecreaser = gameObject.transform.localScale * value/10;
        spriteShower.transform.localScale= gameObject.transform.localScale- gameObjectSacaleDecreaser;
    }
}
