using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererTool
{
    public readonly Color aimStartColor= Color.red;
    public Color startColor=Color.green;
    public Color middleColor=Color.yellow;
    public Color endColor= Color.red;
    public LineRenderer DrawLine(LineRenderer line, Vector3 startPosition, Vector3 secondPosition, float maxDistance)
    {

        float distance = Vector3.Distance(startPosition, secondPosition);
         Vector3 endPosition = secondPosition+(secondPosition-startPosition).normalized * (distance+1);//Limit Length
        line.positionCount = 2;
        line.SetPosition(0, startPosition);
        line.SetPosition(1, endPosition);
        line.endWidth = 0.05f;
        line.startWidth = 0.2f;
        #region color setting
        Color aimColor = startColor;
        if (distance > maxDistance / 3)
        {
            if (distance > maxDistance * 2 / 3)
            {
                aimColor = endColor;
            }
            else
            {
                aimColor = middleColor;
            }
        }
        line.endColor = aimColor;
        line.startColor = aimColor;
        #endregion
        return line;
    }


}
