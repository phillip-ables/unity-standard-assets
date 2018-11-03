using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaunchArcRenderer : MonoBehaviour {
    LineRenderer lr;

    public float velocity;
    public float angle;
    public int resolution;  // how many segments our arch is gonna have

    float g;  // force of gravity on the y
    //look at the value of the gravity in the physics engine, negative, we want net value
    float radianAngle;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics2D.gravity.y);
    }

    private void Start()
    {
        RenderArc();
    }

    //populating the line renderer with the appropriate settings
    void RenderArc()
    {
        //lr.SetVertexCount(resolution + 1);  // this is obsolete
        lr.positionCount = resolution + 1;
        lr.SetPositions(CalculateArcArray());
    }

    //create an array of vector 3 positions for arc
    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for ( int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;  // floating point value between 0 and 1, all points evenly spaced out in the arc
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    //calculate height and distance of each vertex
    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t / maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        return new Vector3(x, y);
    }

}
