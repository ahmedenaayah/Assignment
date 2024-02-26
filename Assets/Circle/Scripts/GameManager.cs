using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public ClientDataManager clientDataManager;

    [SerializeField] Button restartButton;

    public UnityEvent<ClientDataManager.ClientEntry> onShowClientDetails;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        onShowClientDetails = new UnityEvent<ClientDataManager.ClientEntry>();
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(RestartGame);
    }
    
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
     
}
