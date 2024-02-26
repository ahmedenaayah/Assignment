using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    // Variables for animation parameters
    public float delayBetweenOpenningClients = 0.2f;
    public float openDuration = 0.5f;
    public Ease openEaseType = Ease.OutBounce;

    // UI elements
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_Dropdown filterDropDown;
    public GameObject clientListContent;
    public GameObject clientListItemPrefab;

    // List of client items
    [SerializeField] public List<ClientListItem> clients;

    // Reference to ClientDataManager
    public ClientDataManager clientDataManager;

    // Event for showing client details
    [HideInInspector] public UnityEvent<ClientDataManager.ClientEntry> onShowClientDetails;

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

        // Initialize event
        onShowClientDetails = new UnityEvent<ClientDataManager.ClientEntry>();

        // Add listeners to UI elements
        filterDropDown.onValueChanged.RemoveAllListeners();
        filterDropDown.onValueChanged.AddListener(ApplyFilter);

        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() => SceneManager.LoadScene(0));
    }

    private void Start()
    {
        // Add listener for when data is fetched
        clientDataManager.onDataFetched.AddListener(PlaceAllClientsInPanel);
    }

    // Place all clients in the panel
    private void PlaceAllClientsInPanel()
    {
        foreach (var client in clientDataManager.Clients.clients)
        {
            GameObject obj = Instantiate(clientListItemPrefab, clientListContent.transform);
            obj.SetActive(false);
            var clientList = obj.GetComponent<ClientListItem>();
            clientList.SetClient(client, this);
            clients.Add(clientList);
        }
        ShowAllClients(true);
    }

    // Add opening animation effect using DOTween
    private void AddOpenDoTweenEffect(Sequence sequence, GameObject obj)
    {
        obj.transform.localScale = Vector3.zero;
        obj.SetActive(true);
        sequence.AppendInterval(delayBetweenOpenningClients)                  
                   .Append(obj.transform.DOScale(Vector3.one, openDuration))
                   .SetEase(openEaseType);
    }

    // Apply filter based on dropdown selection
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

    // Show all clients
    private void ShowAllClients(bool isFirstTime = false)
    {
        if (!isFirstTime)
        {
            DisableAll();
        }
        Sequence sequence = DOTween.Sequence();

        foreach (var client in clients)
        {
            AddOpenDoTweenEffect(sequence, client.gameObject);
        }
    }

    // Show all managers
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

    // Show all non-managers
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

    // Disable all client objects
    private void DisableAll()
    {
        foreach (var client in clients)
        {
            client.gameObject.SetActive(false);
        }
    }
}
