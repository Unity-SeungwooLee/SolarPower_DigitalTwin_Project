
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using XCharts.Runtime;

public class ChartManager : MonoBehaviour
{
    public BarChart barChart;

    private string[] regionFileNames = { "data_11", "data_26", "data_27", "data_28", "data_29", "data_30", "data_31", "data_41", "data_42", "data_43", "data_44", "data_45", "data_46", "data_47", "data_48" };
    private DateTime lastCheckedDate;

    // Start is called before the first frame update
    void Start()
    {
        lastCheckedDate = DateTime.Now.Date; // ���� ��¥�� ��¥ �κи� ������
        CreateBarChart();
        StartCoroutine(CheckDateChange());
    }

    IEnumerator CheckDateChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f); // �� �и��� ��¥ ���� Ȯ��

            DateTime currentDate = DateTime.Now.Date;
            if (currentDate != lastCheckedDate)
            {
                lastCheckedDate = currentDate;
                CreateBarChart();
            }
        }
    }

    // ����׷����� �����մϴ�.
    void CreateBarChart()
    {
        DateTime today = DateTime.Now;

        // x���� index��ȣ�� �̸� ����
        for (int i = 0; i < 5; i++)
        {
            DateTime setDate = today.AddDays(i - 5);
            Debug.Log(setDate.ToString("yyyy-MM-dd"));
            barChart.UpdateXAxisData(i, setDate.Day.ToString() + "��");
            StartCoroutine(GetChartData(setDate.ToString("yyyy-MM-dd"), i));
        }
    }

    IEnumerator GetChartData(string date, int dataIndex)
    {
        double totalData = 0;

        foreach (string regionFileName in regionFileNames)
        {
            string url = $"https://solarpowerdata-default-rtdb.firebaseio.com/{date}_REMS/23_50/{regionFileName}.json";

            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log("Error : " + webRequest.error);
                    totalData = 0;
                }
                else
                {
                    PowerDataInfoArray powerDataInfoArray = JsonUtility.FromJson<PowerDataInfoArray>("{\"powerDataInfo\":" + webRequest.downloadHandler.text + "}");
                    totalData += powerDataInfoArray.powerDataInfo[0].dayGelec;
                }
            }
        }
        double result = Math.Round(totalData, 1);
        barChart.UpdateData(0, dataIndex, result);
    }
}