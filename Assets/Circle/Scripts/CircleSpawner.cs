using DG.Tweening;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    // Time delay between opening each client
    public float delayBetweenOpenningClients = 0.2f;
    // Duration of the opening animation
    public float openDuration = 0.5f;
    // Easing type for the opening animation
    public Ease openEaseType = Ease.OutBounce;

    // Prefab for the circle
    public GameObject circlePrefab;
    // Minimum distance between circles to prevent overlap
    public float minDistanceBetweenObjects = 1.2f;
    // Minimum number of circles to spawn
    public int minCircles = 5;
    // Maximum number of circles to spawn
    public int maxCircles = 10;
    // Padding to avoid spawning circles near the screen edges
    public float padding = 1.0f;

    void Start()
    {
        // Spawn a random number of circles
        SpawnCircles(Random.Range(minCircles, maxCircles + 1));
    }

    // Method to spawn circles
    void SpawnCircles(int count)
    {
        // Create a sequence for opening circles with DOTween
        Sequence sequence = DOTween.Sequence();

        // Calculate the radius of the circle prefab
        float circleRadius = circlePrefab.GetComponent<Renderer>().bounds.extents.magnitude;
        // Get the main camera
        Camera mainCamera = Camera.main;

        // Calculate the height and width of the camera's viewport
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
            var obj = Instantiate(circlePrefab, position, Quaternion.identity);
            var originalScale = obj.transform.localScale;
            obj.transform.localScale = Vector3.zero;

            // Add animation to the sequence for opening the circle
            sequence.AppendInterval(delayBetweenOpenningClients)
                    .AppendCallback(() => obj.SetActive(true))
                    .Append(obj.transform.DOScale(originalScale, openDuration))
                    .SetEase(openEaseType);
        }
        sequence.Play();
    }
}
