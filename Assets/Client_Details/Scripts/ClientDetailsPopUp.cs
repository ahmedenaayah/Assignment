using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientDetailsPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clientNameText;
    [SerializeField] private TextMeshProUGUI clientPointsText;
    [SerializeField] private TextMeshProUGUI clientAddressText;
    [SerializeField] private Button popUpCloseButton;
    [SerializeField] private GameObject popUpParent;

    private UIManager uIManager;
    void Start()
    {
        uIManager = UIManager.instance;

        popUpCloseButton.onClick.RemoveAllListeners();
        popUpCloseButton.onClick.AddListener(ClosePopUp);

        uIManager.onShowClientDetails.RemoveAllListeners();
        uIManager.onShowClientDetails.AddListener(ShowPopup);

        ClosePopUp();
    }

    private void ShowPopup(ClientDataManager.ClientEntry client)
    {
        clientNameText.text = uIManager.clientDataManager.Clients.data[client.id].name;
        clientPointsText.text = uIManager. clientDataManager.Clients.data[client.id].points.ToString();
        clientAddressText.text = uIManager. clientDataManager.Clients.data[client.id].address;

        popUpParent.SetActive(true);
    }
    private void ClosePopUp()
    {
        popUpParent.SetActive(false);  
    }
}