using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSlider : MonoBehaviour
{
    public Slider timeSlider; 
    public Text timeText;

    private void Start()
    {
        // �����̴� ���� ����� ������ ȣ��
        timeSlider.onValueChanged.AddListener(UpdateTime);
    }

    // �����̴� ���� ����� �� 
    void UpdateTime(float value)
    {
        // �����̴��� ���� �ð����� ��ȯ -> �ؽ�Ʈ
        timeText.text = Mathf.RoundToInt(value).ToString() + ":00";
    }
}
