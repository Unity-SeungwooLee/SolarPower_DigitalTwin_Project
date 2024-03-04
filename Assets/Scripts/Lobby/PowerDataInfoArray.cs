// �Ľ��� ������ ����
using System.Collections.Generic;

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