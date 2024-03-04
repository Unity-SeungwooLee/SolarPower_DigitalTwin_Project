using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DroneController : MonoBehaviour
{
    public DronCamController dronCamController;

    public GameObject myDrone;
    public Transform[] waypoints; 
    public float flightSpeed;
    public Button startButton;
    public float flightHeight = 10.0f; // ���� ����
    public float readySpeed = 2.5f; // ������ �ӵ�

    private int waypointIndex; // waypoint���� �ε���

    bool isDroneStart = false;

    public enum State { TakeOff, Flight, Return, Landing } //���� ����
    public State droneState; //�������� ���� ����

    private void Start()
    {
        Debug.Log(isDroneStart);
        // waypoint���� y���� ����� flightHeight��ŭ ���δ�.
        foreach (Transform waypoint in waypoints)
        {
            waypoint.position = new Vector3(waypoint.position.x, flightHeight, waypoint.position.z);
        }
        // ����� ó�� ���¸� �̷����·� �����.
        droneState = State.TakeOff;
        // ���۹�ư�� OnClickButton�Լ��� �Ҵ��Ѵ�.
         //startButton.onClick.AddListener(OnClickButton);

        dronCamController = Camera.main.GetComponent<DronCamController>();
        dronCamController.enabled = false;
    }

    private void Update()
    {
        if (isDroneStart)
        {
            SwitchDroneState();
            Debug.Log(isDroneStart);
        }
    }

    public void OnClickButton()
    {
        isDroneStart = true;
        dronCamController.enabled = true;
    }

    // ����� ���¿� ���� �ൿ����.
    public void SwitchDroneState()
    {
        

        //��� ���º�ȭ
        switch (droneState)
        {
            //�̷�
            case State.TakeOff:
                if (waypoints.Length == 0)
                {
                    Debug.Log("��������Ʈ�� �������ּ���.");
                    break;
                }
                else if (waypoints.Length > 0)
                {
                    // �����緯 ������� ȸ����Ų��.
                    StartPropeller();
                    // �̷�
                    myDrone.transform.Translate(Vector3.up * readySpeed * Time.deltaTime);
                    if (myDrone.transform.position.y > flightHeight)
                    {
                        droneState = State.Flight;
                    }
                }
                break;
            //����
            case State.Flight:
                //�� �Ÿ��� ���� �ڿ� �Ÿ��� ���̰� �ִٸ� �ش� waypoint�� �̵�
                if (Vector3.Distance(waypoints[waypointIndex].transform.position, myDrone.transform.position) > 0.1f)
                {
                    Move(myDrone, waypoints[waypointIndex].transform.position, flightSpeed);

                    StartPropeller();
                }
                //���� waypoint�� ������ ���¶��(�� �Ÿ��� ���� 0.1 ���϶��)
                else
                {
                    waypointIndex++;
                    // waypoint�� �� ��������.
                    if (waypointIndex > waypoints.Length - 1)
                    {
                        droneState = State.Return;
                    }
                }
                break;
            // ���� �ڸ��� ����
            case State.Return:

                Move(myDrone, waypoints[0].transform.position, flightSpeed);

                if (Vector3.Distance(waypoints[0].transform.position, myDrone.transform.position) < 0.1f)
                {
                    droneState = State.Landing;
                }

                break;
            //����
            case State.Landing:

                myDrone.transform.Translate(Vector3.down * readySpeed * Time.deltaTime);
                if (myDrone.transform.position.y < 2)
                {
                    readySpeed = 0;
                }
                break;
        }

    }
    // Ÿ������Ʈ�� �̵��Ѵ�.
    void Move(GameObject gameobject, Vector3 targetPoint, float speed)
    {
        // gameObject�� �ִ� �������� Ÿ������Ʈ������ ������ ���Ѵ�.
        Vector3 relativePosition = targetPoint - gameobject.transform.position;
        relativePosition.Normalize();

        //�� ���� ������ �������� normal, �� ���� ���̸� RotateTowards�� ������. �� �����ӿ� 1����
        gameobject.transform.rotation = Quaternion.RotateTowards(gameobject.transform.rotation, Quaternion.LookRotation(relativePosition), 1);
        // gameObject�� ��ǥ������ ���� ���ư���.
        gameobject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // �����緯�� ȸ����Ų��.
    private void StartPropeller()
    {
        Debug.Log("StartPropeller method is called.");
        // �����緯 �ִϸ����� ������Ʈ ��������.
        Animator propAnim = myDrone.GetComponent<Animator>();

        if (propAnim == null )
        {
            Debug.Log("�����緯 �ִϸ��̼��� �������ּ���.");
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                propAnim.SetLayerWeight(i, 1);
            }
        }
    }
    // �����緯�� �����.
    private void PausePropeller()
    {
        // �����ؾ���.
    }

}
