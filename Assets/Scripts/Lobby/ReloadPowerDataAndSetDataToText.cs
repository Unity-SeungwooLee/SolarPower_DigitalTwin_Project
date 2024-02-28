using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ReloadPowerDataAndSetDataToText : MonoBehaviour
{
    // ��¥�� �����ִ� �ؽ�Ʈ
    public Text dateText;
    // �ð��� �����ִ� �ؽ�Ʈ
    public Text timeText;

    // URL �������
    public string dateFileName;
    public string timeFileName;
    public string[] regionFileNames = { "data_11", "data_26", "data_27", "data_28", "data_29", "data_30", "data_31", "data_36", "data_41", "data_42", "data_43", "data_44", "data_45", "data_46", "data_47", "data_48", "data_50" };
    // �������� ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI[] powerText;

    private void Start()
    {
        FirstDate();
    }

    public IEnumerator GetChargeInfo()
    {
        int regionFileIndex = 0;

        foreach (string regionFileName in regionFileNames)
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
                    powerText[regionFileIndex].text = powerDataInfoArray.powerDataInfo[0].dayGelec.ToString();
                    regionFileIndex++;
                }
            }
        }

    }
    // �ʱ� ��¥�� �ð� ����
    public void FirstDate()
    {
        dateFileName = dateText.text + "_REMS";
        string[] splitTimeText = timeText.text.Split(":");
        timeFileName = splitTimeText[0] + "_50";
    }
    // ��¥�� �ð��� �����͸� �ؽ�Ʈ�� �ִ´�.
    public void SetDataToText()
    {
        // ��¥ ���� ����
        dateFileName = dateText.text + "_REMS";
        // �ð� ���� ����
        string[] splitTimeText = timeText.text.Split(":");
        timeFileName = splitTimeText[0] + "_50";

        StartCoroutine(GetChargeInfoCoroutine());
    }
    IEnumerator GetChargeInfoCoroutine()
    {
        yield return GetChargeInfo();
    }
}

// �Ľ��� ������ ����
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