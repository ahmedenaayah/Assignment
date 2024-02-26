using System;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private LayerMask objectColliderlayer;
    [SerializeField] private LineRenderer lineRenderer;
    [Range(0,1)]
    [SerializeField] private float lineWidth = 0.1f;

    private CollisionDetector collisionDetector;

    void Start()
    {
        collisionDetector = new CollisionDetector(objectColliderlayer, lineRenderer);
        // Initialize LineRenderer properties
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {   
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;
             // If this is the first point of the line, add it to the LineRenderer
            if (lineRenderer.positionCount == 0)
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(0, currentPosition);
            }
            else
            {
                // Add new points to the LineRenderer
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPosition);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            collisionDetector.MouseIsUp();
            // If the mouse button is released, reset the LineRenderer
            lineRenderer.positionCount = 0;
        }
    }
}
