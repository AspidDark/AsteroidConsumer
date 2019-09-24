using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectEx
{
    const int numberOfSegments = 360;
    public static LineRenderer DrawCircle(this GameObject go, float radius, float lineWidth, Color startColor, Color endColor, bool lineRendererExists=true)
    {
        LineRenderer circle = lineRendererExists ? go.GetComponent<LineRenderer>() : go.AddComponent<LineRenderer>();
        circle.useWorldSpace = false;
        circle.startWidth = lineWidth;
        circle.endWidth = lineWidth;
        circle.endColor = endColor;
        circle.startColor = startColor;
        circle.positionCount = numberOfSegments + 1;
        Vector3 [] points = new Vector3[numberOfSegments + 1];

        for (int i = 0; i < numberOfSegments + 1; i++)
        {
            float rad = Mathf.Deg2Rad * i;
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }
        circle.SetPositions(points);

        return circle;
    }


    public static LineRenderer DrawCircle(this GameObject go, LineRenderer circle, float radius, float lineWidth, Color startColor, Color endColor)
    {
        circle.useWorldSpace = false;
        circle.startWidth = lineWidth;
        circle.endWidth = lineWidth;
        circle.endColor = endColor;
        circle.startColor = startColor;
        circle.positionCount = numberOfSegments + 1;
        Vector3[] points = new Vector3[numberOfSegments + 1];

        for (int i = 0; i < numberOfSegments + 1; i++)
        {
            float rad = Mathf.Deg2Rad * i;
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }
        circle.SetPositions(points);

        return circle;
    }

}
