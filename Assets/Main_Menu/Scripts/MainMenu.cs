using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button circleGameButton;
    [SerializeField] private Button dataDisplayButton;
    private void Awake()
    {
        circleGameButton.onClick.RemoveAllListeners();
        circleGameButton.onClick.AddListener(()=>LoadScene(2));

        dataDisplayButton.onClick.RemoveAllListeners();
        dataDisplayButton.onClick.AddListener(() => LoadScene(1));
    }
    private void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
