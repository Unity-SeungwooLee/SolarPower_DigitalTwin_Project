using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    
    // ��¥�� �����ִ� �ؽ�Ʈ
    public Text dateText;
    // �ð��� �����ִ� �ؽ�Ʈ
    public Text timeText;
    // ���� ������ ������, ���� ������ ������
    private JsonParsing powerData;

    private void Start()
    {
        powerData = new JsonParsing();
        FirstDate();
    }

    // �ʱ� ��¥�� �ð� ����
    public void FirstDate()
    {
        powerData.dateFileName = dateText.text + "_REMS";
        string[] splitTimeText = timeText.text.Split(":");
        powerData.timeFileName = splitTimeText[0] + "_50";
    }

    private void Update()
    {
        SetDataToText();
    }

    // ��¥�� �ð��� �����͸� �ؽ�Ʈ�� �ִ´�.
    public void SetDataToText()
    {
        // ��¥ ���� ����
        powerData.dateFileName = dateText.text + "_REMS";
        // �ð� ���� ����
        string[] splitTimeText = timeText.text.Split(":");
        powerData.timeFileName = splitTimeText[0] + "_50";

        powerData.StartCoroutine(GetChargeInfoCoroutine());
    }

    IEnumerator GetChargeInfoCoroutine()
    {
        yield return powerData.GetChargeInfo();
    }

}
