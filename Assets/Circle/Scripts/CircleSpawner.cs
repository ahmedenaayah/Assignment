using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    public GameObject circlePrefab;
    public float minDistanceBetweenObjects = 1.2f;
    public int minCircles = 5;
    public int maxCircles = 10;
    public float padding = 1.0f;

    void Start()
    {
        SpawnCircles(Random.Range(minCircles, maxCircles + 1));
    }

    void SpawnCircles(int count)
    {
        float circleRadius = circlePrefab.GetComponent<Renderer>().bounds.extents.magnitude;
        Camera mainCamera = Camera.main;

        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Minimum distance between circles to prevent overlap
        float minDistance = circleRadius * minDistanceBetweenObjects;

        for (int i = 0; i < count; i++)
        {
            // Generate random positions within the camera's viewport
            float randomX = Random.Range(-cameraWidth / 2f + circleRadius, cameraWidth / 2f - circleRadius);
            float randomY = Random.Range(-cameraHeight / 2f + circleRadius, cameraHeight / 2f - circleRadius);

            // Convert viewport coordinates to world coordinates
            Vector3 position = mainCamera.transform.position + new Vector3(randomX, randomY, mainCamera.nearClipPlane + 1f);

            // Check if the new circle is too close to existing circles
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, minDistance);
            if (colliders.Length > 0)
            {
                // If too close, skip this iteration
                continue;
            }

            // Spawn the circle at the calculated random position
            Instantiate(circlePrefab, position, Quaternion.identity);
        }
    }


}
