using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private LineRendererTool rendererTool = new LineRendererTool();

    public float maxDragDistance = 2f;
    public float canInteractTime = 2f;

    private bool canInteract = true;
    private bool isPressed = false;
    private bool realeaseCanInteract = false;

    private bool countAimingLine = false;

    private SpringJoint2D springJoint2D;

    public float forceMultypuer;

    #region Dreving aim and circle
    LineRenderer circle;
    LineRenderer line;
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

            if (Vector3.Distance(mousePos, PlayerStats.instance.hookRigitbody.position) > maxDragDistance)
                PlayerStats.instance.rb.position = PlayerStats.instance.hookRigitbody.position + (mousePos - PlayerStats.instance.hookRigitbody.position).normalized * maxDragDistance;
            else
                PlayerStats.instance.rb.position = mousePos;
        }

        if (realeaseCanInteract)
        {
            line = rendererTool.DrawLine(PlayerStats.instance.hookAimRenderer, PlayerStats.instance.hookAim.transform.position, gameObject.transform.position, maxDragDistance);
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
            circle = PlayerStats.instance.hookRigitbody.gameObject.DrawCircle(PlayerStats.instance.hookRenderer, maxDragDistance, 0.05f, Color.red, Color.red);
            
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
            countAimingLine = false;
            realeaseCanInteract = false;
            isPressed = false;
            PlayerStats.instance.rb.isKinematic = false;
            springJoint2D.enabled = false;
            circle.positionCount = 0;
            line.positionCount = 0;
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
        float distance = Vector2.Distance(gameObject.transform.position, PlayerStats.instance.hookRigitbody.transform.position);
        Vector2 direction = (gameObject.transform.position - PlayerStats.instance.hookRigitbody.transform.position).normalized * forceMultypuer * (distance + 1) * (distance + 1);
        PlayerStats.instance.rb.AddForce(direction, ForceMode2D.Impulse);
        //Дальше юзаем замедление....
    }

    private void RemoveForece()
    {
        PlayerStats.instance.rb.velocity = Vector3.zero;
        PlayerStats.instance.rb.angularVelocity = 0;
    }
}
