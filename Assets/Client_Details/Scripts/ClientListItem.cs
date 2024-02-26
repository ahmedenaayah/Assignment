using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
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
            // Activate the noDataAlert object
            noDataAlert.SetActive(true);

            // Apply Dotween animation to shake the noDataAlert
            noDataAlert.transform.DOShakePosition(0.5f, new Vector3(10, 0, 0), 10, 90, false)
                .OnComplete(() =>
                {
                    // After shaking animation, disable the noDataAlert
                    noDataAlert.SetActive(false);
                });
        }
    }
}
