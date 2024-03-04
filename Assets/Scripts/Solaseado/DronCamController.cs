using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronCamController : MonoBehaviour
{
    private Transform dronTransform = null;
    public float distance = 2f;
    public float height = 3f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    public Transform target;

    private void Start()
    {
        dronTransform = GetComponent<Transform>();
        //Ÿ���� ���ٸ� Player��� �±׸� ������ �ִ� ���ӿ�����Ʈ�� Ÿ���̴�.
        //if (target == null)
        //{
        //    target = GameObject.FindWithTag("Player").transform;
        //}
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        //ī�޶� ��ǥ�� �ϰ� �ִ� ȸ�� Y�ప�� ���̰�
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;
        //���� ī�޶� �ٶ󺸰� �ִ� ȸ�� Y�ప�� ���̰�
        float currentRotationAngle = dronTransform.eulerAngles.y;
        float currentHeight = dronTransform.position.y;
        //���� ī�޶� �ٶ󺸰� �ִ� ȸ������ ���̰��� �����ؼ� ���ο� ������ ���
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        //������ ����� ȸ�������� ���ʹϾ� ȸ������ ����
        Quaternion currentRotation = Quaternion.Euler(0.0f, currentRotationAngle, 0.0f);
        //ī�޶� Ÿ���� ��ġ���� ȸ���ϰ��� �ϴ� ���͸�ŭ �ڷ� ��������.
        dronTransform.position = target.position;
        dronTransform.position -= currentRotation * Vector3.forward * distance;
        //�̵��� ��ġ���� ���ϴ� ���̰����� �ö󰣴�.
        dronTransform.position = new Vector3(dronTransform.position.x, currentHeight, dronTransform.position.z);
        //Ÿ���� �׻� �ٶ󺸵��� �Ѵ�. forward -> target
        dronTransform.LookAt(target);
    }
}
