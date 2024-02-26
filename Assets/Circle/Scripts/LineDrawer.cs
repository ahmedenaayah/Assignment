using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private LayerMask objectColliderlayer; // The layer mask to detect collisions with objects
    [SerializeField] private LineRenderer lineRenderer; // Reference to the LineRenderer component
    [Range(0, 1)]
    [SerializeField] private float lineWidth = 0.1f; // The width of the line

    private CollisionDetector collisionDetector; // Reference to the CollisionDetector class for handling collisions

    void Start()
    {
        // Initialize the CollisionDetector with the specified layer and LineRenderer
        collisionDetector = new CollisionDetector(objectColliderlayer, lineRenderer);

        // Initialize LineRenderer properties
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 0; // Start with no points
    }

    void Update()
    {
        // Check if the left mouse button is held down
        if (Input.GetMouseButton(0))
        {
            // Convert mouse position to world position
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
        // Check if the left mouse button is released
        else if (Input.GetMouseButtonUp(0))
        {
            // Handle collisions and remove intersecting objects
            collisionDetector.MouseIsUp();

            // Reset the LineRenderer when mouse button is released
            lineRenderer.positionCount = 0;
        }
    }
}
