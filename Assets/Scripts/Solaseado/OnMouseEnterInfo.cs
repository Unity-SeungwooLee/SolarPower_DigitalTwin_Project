using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseEnterTest : MonoBehaviour
{
    public Canvas infoCanvas;

    private void Start()
    {
        SetCanvasAndChildrenActive(false);
    }

    private void OnMouseEnter()
    {
        SetCanvasAndChildrenActive(true);
        Debug.Log("���콺 ȣ��");
    }

    private void OnMouseExit()
    {
        SetCanvasAndChildrenActive(false);
    }

    private void SetCanvasAndChildrenActive(bool active)
    {
        infoCanvas.enabled = active;

        // �ڽ� ������Ʈ�鿡 ���� Ȱ��ȭ/��Ȱ��ȭ ó��
        foreach (Transform child in infoCanvas.transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}