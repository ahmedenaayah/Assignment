using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{ 
    public float delayBetweenOpenningClients = 0.2f;
    public float openDuration = 0.5f;
    public Ease openEaseType = Ease.OutBounce;

    [SerializeField] private Button  backButton;

    [SerializeField] private TMP_Dropdown filterDropDown;
     public GameObject clientListContent;

    public GameObject clientListItemPrefab;
   
    [SerializeField]
    public List<ClientListItem> clients;
 
    public ClientDataManager clientDataManager; 
    [HideInInspector]public UnityEvent<ClientDataManager.ClientEntry> onShowClientDetails;

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

        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(()=>SceneManager.LoadScene(0));
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
            obj.SetActive(false);
            var clientList = obj.GetComponent<ClientListItem>();
            clientList.SetClient(client,this);
            clients.Add(clientList);
        }
        ShowAllClients(true);
    }
    private void AddOpenDoTweenEffect(Sequence sequence,GameObject obj)
    {
        obj.transform.localScale = Vector3.zero;
        sequence.AppendInterval(delayBetweenOpenningClients)
                   .AppendCallback(() => obj.SetActive(true))
                   .Append(obj.transform.DOScale(Vector3.one, openDuration))
                   .SetEase(openEaseType);
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
    private void ShowAllClients(bool isFirstTime = false)
    {
        if (!isFirstTime)
        {
            DisableAll();
        }
        Sequence sequence = DOTween.Sequence();

        foreach (var client in clients)
        {
            AddOpenDoTweenEffect(sequence,client.gameObject);
        }
    }
    private void ShowAllManagers()
    {
        DisableAll();
        Sequence sequence = DOTween.Sequence();

        foreach (var client in clients)
        {
            if (client.client.isManager)
            {
                AddOpenDoTweenEffect(sequence, client.gameObject);
            }
        }
    }
    private void ShowNonManagers()
    {
        DisableAll();
        Sequence sequence = DOTween.Sequence();

        foreach (var client in clients)
        {
            if (!client.client.isManager)
            {
                AddOpenDoTweenEffect(sequence, client.gameObject);
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
