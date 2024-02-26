using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button backButton; // Reference to the back button in the UI
    [SerializeField] private Button restartButton; // Reference to the restart button in the UI

    public static GameManager instance; // Static reference to the GameManager instance

    // Method called when the GameManager instance is created
    private void Awake()
    {
        // Ensure there's only one instance of the GameManager
        if (instance == null)
        {
            instance = this; // Set this instance as the GameManager instance if none exists
        }
        else
        {
            Destroy(this); // Destroy the duplicate instance if one exists
        }

        // Clear existing listeners and add new ones for the restart button
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(RestartGame);

        // Clear existing listeners and add new ones for the back button
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() => SceneManager.LoadScene(0)); // Load the main menu scene when the back button is clicked
    }

    // Method to restart the game by reloading the current scene
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
