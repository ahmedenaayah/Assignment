using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Events;

public class ClientDataManager : MonoBehaviour
{
    private APIService apiService;
    private ClientsData clientsData;
    public ClientsData Clients => clientsData;
    internal UnityEvent onDataFetched;
    private void Awake()
    {
        onDataFetched = new UnityEvent();
        apiService = new APIService();
        StartCoroutine(FetchClientData());
    }

    private IEnumerator FetchClientData()
    {
        yield return apiService.GetClientData(ProcessClientData, HandleError);
    }

    private void ProcessClientData(string json)
    {
        try
        {
            clientsData = JsonConvert.DeserializeObject<ClientsData>(json);
           
            Debug.Log("Label: " + clientsData.label);

            // Process clients data
            foreach (ClientEntry client in clientsData.clients)
            {
                Debug.Log("Client ID: " + client.id + ", Label: " + client.label + ", Is Manager: " + client.isManager);
            }

            // Process data entries
            foreach (KeyValuePair<int, DataEntry> entry in clientsData.data)
            {
                var key = entry.Key;
                DataEntry clientData = entry.Value;

                Debug.Log("ID: " + key + ", Name: " + clientData.name + ", Address: " + clientData.address + ", Points: " + clientData.points);
            }

            Debug.Log("Number of Entries: " + clientsData.data.Count);
            onDataFetched.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error processing client data: " + ex.Message);
        }
    }

    private void HandleError(string errorMessage)
    {
        Debug.LogError("API Error: " + errorMessage);
    }

    [System.Serializable]
    public class DataEntry
    {
        public string address;
        public string name;
        public int points;
    }

    [System.Serializable]
    public class ClientEntry
    {
        public bool isManager;
        public int id;
        public string label;
    }

    [System.Serializable]
    public class ClientsData
    {
        public List<ClientEntry> clients;
        public Dictionary<int, DataEntry> data;
        public string label;
    }
}
