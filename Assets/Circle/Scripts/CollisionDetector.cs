using UnityEngine;
using System.Collections.Generic;

public class CollisionDetector 
{
    private LayerMask layer;
    private LineRenderer lineRenderer;

    private List<GameObject> collidedObjects = new List<GameObject>();

    public CollisionDetector(LayerMask layer, LineRenderer lineRenderer)
    {
        this.layer = layer;
        this.lineRenderer = lineRenderer;
        collidedObjects.Clear();
    }
 

    public void MouseIsUp()
    {
        DetectCollisions();
        HandleCollisions();
    }

    private void DetectCollisions()
    {
        collidedObjects.Clear();

        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {

            Vector3 startPosition = lineRenderer.GetPosition(i);
            Vector3 endPosition = lineRenderer.GetPosition(i + 1);

            RaycastHit2D[] hits = Physics2D.LinecastAll(startPosition, endPosition, layer);
            foreach (RaycastHit2D hit in hits)
            {

                if (!collidedObjects.Contains(hit.collider.gameObject))
                {

                    collidedObjects.Add(hit.collider.gameObject);
                }
            }
        }
    }

    private void HandleCollisions()
    {
        foreach (GameObject obj in collidedObjects)
        {
            obj.SetActive(false);
        }
    }
}