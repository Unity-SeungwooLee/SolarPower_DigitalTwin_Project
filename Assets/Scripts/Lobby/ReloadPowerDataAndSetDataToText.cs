using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Security.Policy;
using XCharts.Runtime;

public class ReloadPowerDataAndSetDataToText : MonoBehaviour
{
    // ��¥�� �����ִ� �ؽ�Ʈ
    public Text dateText;
    // �ð��� �����ִ� �ؽ�Ʈ
    public Text timeText;
    // ������ ž3 �ؽ�Ʈ ���� �̸�
    public TextMeshProUGUI[] topThreeTextName;
    // ������ ž3 �ؽ�Ʈ ������
    public TextMeshProUGUI[] topThreeText;
    // Pie �׷���
    public PieChart pieChart;
    // ���ӱ׷��� ž4 �ؽ�Ʈ
    public TextMeshProUGUI[] topFourText;
    // �ǽð� ���� �� ���귮
    public double total = 0;

    // URL �������
    public string dateFileName;
    public string timeFileName;
    private string[] DashboardRegionNames = { "data_11", "data_26", "data_27", "data_28", "data_29", "data_30", "data_31", "data_41", "data_42", "data_43", "data_44", "data_45", "data_46", "data_47", "data_48" };
    string[] RegionKoreaName = { "����Ư����", "�λ걤����", "�뱸������", "��õ������", "���ֱ�����", "����������", "��걤����", "��⵵", "������", "��û�ϵ�", "��û����", "����ϵ�", "���󳲵�", "���ϵ�", "��󳲵�" };
    private Dictionary<string, double> topThreeRegionData = new Dictionary<string, double>();
    private Dictionary<string, double> topFourRegionData = new Dictionary<string, double>();

    // �������� ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI[] powerText;


    private void Start()
    {
        SetDataToText();
    }

    public IEnumerator GetChargeInfo()
    {
        // ���������� �ε���
        int regionFileIndex = 0;
        // �ǽð� ���� �� ���귮
        total = 0;

        foreach (string regionFileName in DashboardRegionNames)
        {
            // ��û�� url
            string url = $"https://solarpowerdata-default-rtdb.firebaseio.com/{dateFileName}/{timeFileName}/{regionFileName}.json";

            // ���� ��û�� ������.
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log($"Error : {webRequest.error}");
                }
                else
                {
                    // ������ JSON������ �Ľ��Ѵ�.
                    PowerDataInfoArray powerDataInfoArray = JsonUtility.FromJson<PowerDataInfoArray>("{\"powerDataInfo\":" + webRequest.downloadHandler.text + "}");
                    total += powerDataInfoArray.powerDataInfo[0].dayGelec;
                    powerText[regionFileIndex].text = powerDataInfoArray.powerDataInfo[0].dayGelec.ToString();
                    regionFileIndex++;
                }
            }
        }
        DescendingTopThreeDictionary(topThreeRegionData);
        DescendingTopFourDictionary(topFourRegionData);
        topThreeRegionData.Clear();
        topFourRegionData.Clear();
    }
    // ������ ž3 ��ųʸ� ������������ �����
    void DescendingTopThreeDictionary(Dictionary<string, double> dict)
    {
        // ��ųʸ��� �� �߰�
        for (int i = 0; i < 7; i++)
        {
            dict.Add(RegionKoreaName[i], double.Parse(powerText[i].text));
        }
        // ��ųʸ��� ������������ ����
        var sortedDictionary = dict.OrderByDescending(kvp => kvp.Value);
        // �ε�����ȣ
        int index = 0;
        // 1, 2, 3�� �ؽ�Ʈ�� ǥ��
        foreach (var kvp in sortedDictionary)
        {
            topThreeTextName[index].text = $"{kvp.Key}";
            topThreeText[index].text = $"{kvp.Value}MWh";
            index++;
            if (index == 3)
                break;
        }
        dict.Clear();
    }
    // ���� ž4 ��ųʸ� �������� �����
    void DescendingTopFourDictionary(Dictionary<string, double> dict)
    {
        // ��ųʸ��� �� �߰�
        for (int i = 7; i < 15; i++)
        {
            dict.Add(RegionKoreaName[i], double.Parse(powerText[i].text));
        }
        // ��ųʸ��� ������������ ����
        var sortedDictionary = dict.OrderByDescending(kvp => kvp.Value);
        // �ε�����ȣ
        int index = 0;
        // 1 ~ 4�� PieChart�� ǥ���Ѵ�.
        foreach (var kvp in sortedDictionary)
        {
            double value = 360 * kvp.Value / total;
            pieChart.UpdateData(index, 0, value);
            pieChart.UpdateDataName(index, 0, kvp.Key);
            pieChart.UpdateDataName(index, 1, kvp.Key);
            topFourText[index].text = (value * 100 / 360 ).ToString("F1") + "%";
            index++;
            if (index == 4)
                break;
        }
    }
    // �ʱ� ��¥�� �ð� ����
    public void SetText()
    {
        // ��¥ ���� ����
        dateFileName = dateText.text + "_REMS";
        // �ð� ���� ����
        string[] splitTimeText = timeText.text.Split(":");
        // �ѽð� �� �����͸� �ҷ��´�.
        int splitTime = int.Parse(splitTimeText[0]) - 1;
        // �ð��� 10�� �����̶��
        if (splitTime < 10)
        {
            timeFileName = "0" + splitTime.ToString() + "_50";
        }
        // 
        else if (splitTime < 0)
        {
            timeFileName = "00_50";
        }
        else
        {
            timeFileName = splitTime + "_50";
        }
    }
    // ��¥�� �ð��� �����͸� �ؽ�Ʈ�� �ִ´�.
    public void SetDataToText()
    {
        // ��¥�� �ð� ���� ����
        SetText();
        StartCoroutine(GetChargeInfo());
    }
    
}