using DG.Tweening;
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

    [SerializeField] private float popupOpenDuration = 0.5f;
    [SerializeField] private float popupCloseDuration = 0.3f;


    private UIManager uIManager;
    void Start()
    {
        uIManager = UIManager.instance;

        popUpCloseButton.onClick.RemoveAllListeners();
        popUpCloseButton.onClick.AddListener(ClosePopUp);

        uIManager.onShowClientDetails.RemoveAllListeners();
        uIManager.onShowClientDetails.AddListener(ShowPopup);

        //ClosePopUp();
    }

    public void ShowPopup(ClientDataManager.ClientEntry client)
    {
        // Populate the popup with client data
        clientNameText.text = uIManager.clientDataManager.Clients.data[client.id].name;
        clientPointsText.text = uIManager.clientDataManager.Clients.data[client.id].points.ToString();
        clientAddressText.text = uIManager.clientDataManager.Clients.data[client.id].address;

        // Open popup with animation
        popUpParent.SetActive(true);
        popUpParent.transform.localScale = Vector3.zero;
        popUpParent.transform.DOScale(Vector3.one, popupOpenDuration);
    }

    public void ClosePopUp()
    {
        // Close popup with animation
        popUpParent.transform.DOScale(Vector3.zero, popupCloseDuration)
            .OnComplete(() => popUpParent.SetActive(false));
    }
}