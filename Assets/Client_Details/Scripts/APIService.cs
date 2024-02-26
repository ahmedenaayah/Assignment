using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;

public class APIService
{
    private string apiUrl = "https://qa.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";

    public IEnumerator GetClientData(Action<string> successCallback, Action<string> errorCallback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("API request failed: " + webRequest.error);
                errorCallback?.Invoke("Failed to fetch data from API.");
            }
            else
            {
                string responseData = webRequest.downloadHandler.text;
                successCallback?.Invoke(responseData);
            }
        }
    }
}
