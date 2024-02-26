using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ClientListItem : MonoBehaviour
{
    [SerializeField] private GameObject noDataAlert;

    [SerializeField] private TextMeshProUGUI nameLabel;
    [SerializeField] private TextMeshProUGUI pointsLabel;
    [SerializeField] private Button clientDetailsShowButton;

    public ClientDataManager.ClientEntry client;
    private UIManager uIManager;
    private bool isClientDataAvailable;

    public void SetClient(ClientDataManager.ClientEntry client, UIManager uIManager)
    {
        noDataAlert.SetActive(false);

        this.client = client;
        this.uIManager = uIManager;

        isClientDataAvailable = uIManager.clientDataManager.Clients.data.ContainsKey(client.id);

        nameLabel.text = client.label;
        pointsLabel.text = isClientDataAvailable? uIManager.clientDataManager.Clients.data[client.id].points.ToString(): "Null";
 
        clientDetailsShowButton.onClick.RemoveAllListeners();
        clientDetailsShowButton.onClick.AddListener(ShowClientDetails);
    }

    private void ShowClientDetails()
    {
        if (isClientDataAvailable)
        {
            uIManager.onShowClientDetails.Invoke(client);
        }
        else
        {
            noDataAlert.SetActive(true);

        }
    }
}
