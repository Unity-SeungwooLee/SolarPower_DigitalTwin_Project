using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.GlobalIllumination;
using System.Xml.Linq;
using System;
using Unity.VisualScripting;

public class ReloadSunDataAndRotate : MonoBehaviour
{
    // sun�� �θ� ������Ʈ, �¾��� ���߰���ŭ x���� ȸ����Ų��.
    public Transform sunAltitude;
    public Light sun;
    public TMP_InputField dateInput;
    public Button button;
    public Slider slider;

    private SunData sunData;
    private string serviceKey = "dmhrSq%2BTqlzT%2BnZUeLs4aOLl034z1ORuIrI0GvJjb86PSCTT6ycLhKNmZXrGETGBOBftom48mqszKlqj%2FXMCug%3D%3D";

    void Start()
    {
        // ���� ��¥�� dateInput�� �Է�
        dateInput.text = DateTime.Now.ToString("yyyyMMdd");
        // �����̴��� �⺻���� 0.5
        slider.value = 0.5f;
        // ��ư Ŭ�� �̺�Ʈ�� �޼��� ����
        button.onClick.AddListener(OnButtonClick);
        // �����̴� �� ���� �̺�Ʈ�� �޼��� ����
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        // �ʱ� ������ �ε� �� �¾� ��ġ ������Ʈ
        FetchSunData();
    }
    // ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    private void OnButtonClick()
    {
        // ����ڰ� ������ ��¥�� �����͸� �ٽ� �����ͼ� �¾� ��ġ ������Ʈ
        FetchSunData();
    }
    // �����̴��� �¾� ȸ��
    void OnSliderValueChanged(float value)
    {
        value = slider.value;
        rotateSunAndPanel(value);
    }

    /// <summary>
    /// �¾��� ����, �� ������ �޾Ƽ� ����
    /// </summary>
    private void FetchSunData()
    {
        string url = new UriBuilder("http://apis.data.go.kr/B090041/openapi/service/SrAltudeInfoService/getAreaSrAltudeInfo")
        {
            Query = $"ServiceKey={serviceKey}&location=����&locdate={dateInput.text}"
        }.Uri.ToString();

        sunData = new SunData();

        XDocument xmlDoc = XDocument.Load(url);

        foreach (var node in xmlDoc.Descendants("item"))
        {
            if (node.Element("altitudeMeridian").Value == null)
            {
                return;
            }
            else
            {
                sunData.altitudeMeridian = GetAngle(node.Element("altitudeMeridian").Value);
            }

        }

        // ���߰��� ���� �¾� ǥ��
        rotateSunAndPanel(0.5f);
        // �����̴��� �⺻���� 0.5
        slider.value = 0.5f;
    }

    // �¾�� �¾籤 �г� ȸ��
    private void rotateSunAndPanel(float value)
    {
        sunAltitude.rotation = Quaternion.Euler(new Vector3(-sunData.altitudeMeridian, 0, 0));
        sun.transform.localRotation = Quaternion.AngleAxis(value * 360, Vector3.up);

        // �г� ȸ���κ� �����ؾ���.
    }

    // �ð��� ���� �¾��� �� ���
    private int GetAngle(string angle)
    {
        return int.Parse(angle.Split('��')[0]);
    }
}
