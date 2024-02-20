using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSlider : MonoBehaviour
{
    public Slider timeSlider; // Unity Inspector â���� Slider�� �����մϴ�.
    public Text timeText; // Unity Inspector â���� Text�� �����մϴ�.

    private void Start()
    {
        // �����̴� ���� ����� ������ ȣ��Ǵ� �̺�Ʈ�� �����մϴ�.
        timeSlider.onValueChanged.AddListener(UpdateTime);
    }

    // �����̴� ���� ����� �� ȣ��Ǵ� �Լ�
    void UpdateTime(float value)
    {
        // �����̴��� ���� �ð� �������� ��ȯ�Ͽ� �ؽ�Ʈ�� ǥ���մϴ�.
        timeText.text = Mathf.RoundToInt(value).ToString() + ":00";
    }
}
