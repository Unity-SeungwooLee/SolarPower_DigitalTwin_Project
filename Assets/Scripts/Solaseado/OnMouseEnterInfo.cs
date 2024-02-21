using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEnterTest : MonoBehaviour
{
    public Canvas infoCanvas;
    private void Start()
    {
        infoCanvas.enabled = false;
    }
    private void OnMouseEnter()
    {
        infoCanvas.enabled = true;
        Debug.Log("���콺 ȣ��");
    }

    private void OnMouseExit()
    {
        infoCanvas.enabled = false;
    }
}