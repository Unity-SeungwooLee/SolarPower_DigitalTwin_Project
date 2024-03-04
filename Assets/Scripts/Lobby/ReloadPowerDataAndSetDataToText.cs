using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class ReloadPowerDataAndSetDataToText : MonoBehaviour
{
    // ��¥�� �����ִ� �ؽ�Ʈ
    public Text dateText;
    // �ð��� �����ִ� �ؽ�Ʈ
    public Text timeText;
    // URL �������
    public string dateFileName;
    public string timeFileName;
    private string[] regionFileNames = { "data_11", "data_26", "data_27", "data_28", "data_29", "data_30", "data_31", "data_36", "data_41", "data_42", "data_43", "data_44", "data_45", "data_46", "data_47", "data_48", "data_50" };
    // ���ù�ȣ�� �������� ��� ��ųʸ�
    Dictionary<string, double> CityAndChargeData;
    // �������� ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI[] powerText;

    private void Start()
    {
        // SetText();
    }

    public IEnumerator GetChargeInfo()
    {
        // ���������� �ε���
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
                    // ������ JSON������ �Ľ��Ѵ�.
                    PowerDataInfoArray powerDataInfoArray = JsonUtility.FromJson<PowerDataInfoArray>("{\"powerDataInfo\":" + webRequest.downloadHandler.text + "}");
                    powerText[regionFileIndex].text = powerDataInfoArray.powerDataInfo[0].dayGelec.ToString();
                    regionFileIndex++;
                }
            }
        }
    }
    // �ʱ� ��¥�� �ð� ����
    public void SetText()
    {
        // ��¥ ���� ����
        dateFileName = dateText.text + "_REMS";
        // �ð� ���� ����
        string[] splitTimeText = timeText.text.Split(":");
        // �ð��� 10�� �����̶��
        if (int.Parse(splitTimeText[0]) < 10) timeFileName = "0" + splitTimeText[0] + "_50";
        else timeFileName = splitTimeText[0] + "_50";
    }
    // ��¥�� �ð��� �����͸� �ؽ�Ʈ�� �ִ´�.
    public void SetDataToText()
    {
        // ��¥�� �ð� ���� ����
        SetText();

        StartCoroutine(GetChargeInfoCoroutine());
    }
    // ������ �����͸� �� �������� �����͸� �ؽ�Ʈ�� �ִ� �ڷ�ƾ�Դϴ�.
    IEnumerator GetChargeInfoCoroutine()
    {
        yield return GetChargeInfo();
    }
}