using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ObejctRotate : MonoBehaviour
{
    // �¾��� ���� ������ ������Ʈ (�θ�) (x�� ȸ��)
    public Transform testAltitude;
    // �¾��� �������� ������ ������Ʈ (y�� ȸ��)
    public Transform testAzimuth;
    // �¾��� ���� API�� ���� �ҷ��ͼ� ������ �����ϱ�

    // �¾��� �� ȸ���ϱ�
    public void RotateAltitude(float value)
    {
        testAltitude.rotation = Quaternion.Euler(new Vector3(value, 0, 0));
    }
    // �¾��� ������ ȸ���ϱ�
    public void RotateAzimuth(float value)
    {
        testAzimuth.localRotation = Quaternion.AngleAxis(value, Vector3.up);
    }

    private void Start()
    {
        RotateAltitude(20);
        RotateAzimuth(30);

    }
    private void Update()
    {
        
    }



}
