using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JsonParsing : MonoBehaviour
{
    // [SerializeField] Text text;
    // URL �������
    [SerializeField ]private string dateFileName = "2024-02-19_REMS";
    public string timeFileName = "16_50";
    public string regionFileName = "data_11";

    void Start()
    {
        StartCoroutine(GetChargeInfo());
    }

    IEnumerator GetChargeInfo()
    {
        // ��û�� url
        string url = $"https://solarpowerdata-default-rtdb.firebaseio.com/{dateFileName}/{timeFileName}/{regionFileName}.json";

        // ���� ��û�� ������.
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log($"Error : {webRequest.error}");
            }
            else
            {
                PowerDataInfoArray powerDataInfoArray = JsonUtility.FromJson<PowerDataInfoArray>("{\"powerDataInfo\":" + webRequest.downloadHandler.text + "}");
                ProcessChargeInfo(powerDataInfoArray.powerDataInfo[0]);
            }
        }
    }
    // ������ �����͸� �ؽ�Ʈ�� �����ش�.
    private void ProcessChargeInfo(PowerData powerData)
    {
        // text.text = $"���� ������: {powerData.dayGelec}";
        Debug.Log($"���� ������: {powerData.dayGelec}");
        Debug.Log($"���� ������: {powerData.accumGelec}");
        Debug.Log($"���� ��뷮: {powerData.dayPrdct}");
        Debug.Log($"���� ��뷮: {powerData.cntuAccumPowerPrdct}");
    }
}

[System.Serializable]
public class PowerDataInfoArray
{
    public List<PowerData> powerDataInfo;
}

[System.Serializable]
public class PowerData
{
    public double dayGelec; // ���Ϲ�����
    public double accumGelec; // ����������
    public int co2;
    public double dayPrdct; // ���ϻ�뷮
    public double hco2;
    public double cntuAccumPowerPrdct; // ������뷮
}

[System.Serializable]
public class InstcapaData
{
    public double gelecInstcapa;
    public double heatInstcapa;
    public double heathInstcapa;
}