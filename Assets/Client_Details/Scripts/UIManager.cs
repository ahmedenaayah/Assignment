using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown filterDropDown;
     public GameObject clientListContent;

    public GameObject clientListItemPrefab;
   
    [SerializeField]
    public List<ClientListItem> clients;
 
    public ClientDataManager clientDataManager; 
    public UnityEvent<ClientDataManager.ClientEntry> onShowClientDetails;

    public static UIManager instance;

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

        filterDropDown.onValueChanged.RemoveAllListeners();
        filterDropDown.onValueChanged.AddListener(ApplyFilter); 
    }
    private void Start()
    {
        clientDataManager.onDataFetched.AddListener(PlaceAllClientsInPanel);
    }
    private void PlaceAllClientsInPanel()
    {
        foreach (var client in clientDataManager.Clients.clients)
        {
            GameObject obj = Instantiate(clientListItemPrefab, clientListContent.transform);
            var clientList = obj.GetComponent<ClientListItem>();
            clientList.SetClient(client,this);
            clients.Add(clientList);
        }
    }
    private void ApplyFilter(int filterType)
    {
        switch (filterType)
        {
            case 0:
                ShowAllClients();
                break;
            case 1:
                ShowAllManagers();
                break;
            case 2:
                ShowNonManagers();
                break;
        }
    }
    private void ShowAllClients()
    {
        DisableAll();
        foreach (var client in clients)
        {          
           client.gameObject.SetActive(true);            
        }
    }
    private void ShowAllManagers()
    {
        DisableAll();
        foreach (var client in clients)
        {
            if (client.client.isManager)
            {
                client.gameObject.SetActive(true); 
            }
        }
    }
    private void ShowNonManagers()
    {
        DisableAll();
        foreach (var client in clients)
        {
            if (!client.client.isManager)
            {
                client.gameObject.SetActive(true);
            }
        }
    }
    private void DisableAll()
    {
        foreach (var client in clients)
        {
           client.gameObject.SetActive(false);
        }
    }
    
}
