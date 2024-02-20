using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarController : MonoBehaviour
{
    // �޷��� ǥ���� UI ��ҵ�
    public GameObject _calendarPanel;
    public Text _yearNumText;  // �⵵�� ǥ���� �ؽ�Ʈ UI
    public Text _monthNumText; // ���� ǥ���� �ؽ�Ʈ UI

    // �޷� ��¥ ������ ������
    public GameObject _item;

    // ��¥ �����۵��� ����Ʈ
    public List<GameObject> _dateItems = new List<GameObject>();
    const int _totalDateNum = 42; // �� ȭ�鿡 ǥ���� �� ��¥ ������ ��

    private DateTime _dateTime;
    public static CalendarController _calendarInstance;

    void Start()
    {
        _calendarInstance = this;

        // ��¥ ������ �ʱ�ȭ
        Vector3 startPos = _item.transform.localPosition;
        _dateItems.Clear();
        _dateItems.Add(_item);

        //�޷� �� ��¥ �����۵��� �����ϰ� ��ġ ����
        for (int i = 1; i < _totalDateNum; i++)
        {
            GameObject item = GameObject.Instantiate(_item) as GameObject;
            //��¥ ������ ������ _item�� �����Ͽ� ���ο� GameObject�� ����

            item.name = "Item" + (i + 1).ToString();
            //: ������ GameObject�� �̸��� ����

            item.transform.SetParent(_item.transform.parent);
            //������ GameObject�� _item�� �θ�� �����Ͽ� ȭ�鿡 ǥ�õ� ��ġ�� ����

            item.transform.localScale = Vector3.one;
            //������ GameObject�� ũ�⸦ ���� ũ��� ����

            item.transform.localRotation = Quaternion.identity;
            //������ GameObject�� ȸ���� �ʱ�ȭ
            item.transform.localPosition = new Vector3((i % 7) * 36 + startPos.x, startPos.y - (i / 7) * 30, startPos.z);
            //������ GameObject�� ��ġ�� ����Ͽ� ����. �� ��¥ �������� 7���� �����ϰ�, �� �� ������ 30, �� ������ 36
            _dateItems.Add(item);
            //������ GameObject�� _dateItems ����Ʈ�� �߰��մϴ�. �� ����Ʈ�� �Ŀ� ������ ��¥ �����ۿ� �����ϱ� ���� ���
        }

        _dateTime = DateTime.Now;

        // ���� ���� �޷� ����
        CreateCalendar();

        _calendarPanel.SetActive(false); // �ʱ⿡�� �޷��� ������ �ʵ��� ����
    }

    // ���� ���� �޷��� �����ϴ� �Լ�
    void CreateCalendar()
    {
        // ���� ���� ù �� ���
        DateTime firstDay = _dateTime.AddDays(-(_dateTime.Day - 1));
        int index = GetDays(firstDay.DayOfWeek);
        //ù ���� ���Ͽ� ���� �ش� ���Ͽ� �´� �ε��� �� ������.
        //GetDays �ռ� --> ������ ���ڷ� ��ȯ

        int date = 0;
        for (int i = 0; i < _totalDateNum; i++)
        {
            Text label = _dateItems[i].GetComponentInChildren<Text>();
            _dateItems[i].SetActive(false); //dateItem��Ȱ��ȭ -> ��� ��¥ �ʱ�ȭ

            if (i >= index)
            {
                // ���� ���� �ش��ϴ� ��¥�� Ȱ��ȭ
                DateTime thatDay = firstDay.AddDays(date);
                if (thatDay.Month == firstDay.Month)
                {
                    _dateItems[i].SetActive(true); //dateItem Ȱ��ȭ

                    label.text = (date + 1).ToString(); //dateItem�� text�ȿ� ��¥ ǥ��
                    date++; //���� ��¥��
                }
            }
        }
        _yearNumText.text = _dateTime.Year.ToString();
        _monthNumText.text = _dateTime.Month.ToString("D2");  // �� �ڸ� �� ���� �� �ڸ��� ǥ�� (01, 02, ..., 12)
    }

    // ���Ͽ� ���� �ε��� ��ȯ
    int GetDays(DayOfWeek day) //GetDays �Լ� -> ������ ���ڷ� ��ȯ�ϴ� �Լ�
    {
        switch (day)
        {
            case DayOfWeek.Monday: return 1;
            case DayOfWeek.Tuesday: return 2;
            case DayOfWeek.Wednesday: return 3;
            case DayOfWeek.Thursday: return 4;
            case DayOfWeek.Friday: return 5;
            case DayOfWeek.Saturday: return 6;
            case DayOfWeek.Sunday: return 0;
        }

        return 0;
    }


    public void YearPrev()
    {
        _dateTime = _dateTime.AddYears(-1);
        CreateCalendar();
    }

    public void YearNext()
    {
        _dateTime = _dateTime.AddYears(1);
        CreateCalendar();
    }

    public void MonthPrev()
    {
        _dateTime = _dateTime.AddMonths(-1);
        CreateCalendar();
    }

    public void MonthNext()
    {
        _dateTime = _dateTime.AddMonths(1);
        CreateCalendar();
    }

    // �޷��� ���̰� �ϴ� �Լ�
    public void ShowCalendar(Text target)
    {
        _calendarPanel.SetActive(true);
        _target = target;
        //_calendarPanel.transform.position = new Vector3(965, 475, 0);//Input.mousePosition-new Vector3(0,120,0);
    }

    Text _target;

    // ������ Ŭ���� Text�� ��¥ ǥ���ϴ� �Լ�
    public void OnDateItemClick(string day)
    {
        _target.text = _yearNumText.text + "-" + _monthNumText.text + "-" + int.Parse(day).ToString("D2");
        //_calendarPanel.SetActive(true);
    }
}
