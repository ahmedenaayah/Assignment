using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class CollisionDetector
{
    private LayerMask layer; // Layer mask used to filter collision detection
    private LineRenderer lineRenderer; // LineRenderer component used for drawing lines

    private List<GameObject> collidedObjects = new List<GameObject>(); // List to store collided game objects

    // Constructor to initialize the CollisionDetector with the layer mask and LineRenderer
    public CollisionDetector(LayerMask layer, LineRenderer lineRenderer)
    {
        this.layer = layer;
        this.lineRenderer = lineRenderer;
        collidedObjects.Clear(); // Clear the list of collided objects when a new CollisionDetector is created
    }

    // Method to handle collision detection when the mouse is released
    public void MouseIsUp()
    {
        DetectCollisions();
        HandleCollisions();
    }

    // Method to detect collisions between the line and objects
    private void DetectCollisions()
    {
        collidedObjects.Clear(); // Clear the list of collided objects before detecting new collisions

        // Iterate through each segment of the line
        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            Vector3 startPosition = lineRenderer.GetPosition(i); // Get the start position of the line segment
            Vector3 endPosition = lineRenderer.GetPosition(i + 1); // Get the end position of the line segment

            // Perform a linecast between the start and end positions to detect collisions
            RaycastHit2D[] hits = Physics2D.LinecastAll(startPosition, endPosition, layer);

            // Iterate through each hit detected by the linecast
            foreach (RaycastHit2D hit in hits)
            {
                // Add the collided object to the list if it's not already included
                if (!collidedObjects.Contains(hit.collider.gameObject))
                {
                    collidedObjects.Add(hit.collider.gameObject);
                }
            }
        }
    }

    // Method to handle the collision effects
    private void HandleCollisions()
    {
        Sequence sequence = DOTween.Sequence(); // Create a new DOTween sequence to manage the animation

        // Iterate through each collided object
        foreach (GameObject obj in collidedObjects)
        {
            // Define the animation sequence for each collided object
            sequence.AppendInterval(0.2f) // Wait for a short interval before starting the animation
                    .Append(obj.transform.DOScale(Vector3.zero, 0.5f)) // Scale down the object to zero over a duration
                    .SetEase(Ease.OutBounce); // Apply an easing function to the animation
        }
    }
}
