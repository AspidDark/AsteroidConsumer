using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererTool : MonoBehaviour {

    public readonly Color aimStartColor= Color.red;
    public LineRenderer DrawLine(LineRenderer line, Vector3 startPosition, Vector3 secondPosition, float maxDistance)
    {
        float distance = Vector3.Distance(startPosition, secondPosition);
        Vector3 endPosition = secondPosition; //* (distance + 1)* (distance + 1)/(maxDistance);//Limit Length
        line.positionCount = 2;
        line.SetPosition(0, startPosition);
        line.SetPosition(1, endPosition);
        line.endWidth = 0.05f;
        line.startWidth = 0.2f;
        line.endColor = aimStartColor;
        line.startColor = aimStartColor;
        return line;
    }


}
