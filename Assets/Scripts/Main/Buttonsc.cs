using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonsc : MonoBehaviour
{
    public GameObject ExitPanel;
    public GameObject SettingPanel;

    //��ư ���� ��ŸƮ

    public void Start()
    {
        ExitPanel.SetActive(false);
        SettingPanel.SetActive(false);
        Debug.Log("����, �ǳ� ����");
    }

    private void OnMouseDown()
    {
        if (gameObject.name == "Start_button")
        {
            SceneManager.LoadScene("Main");
            Debug.Log("���� ����");
        }
        else if (gameObject.name == "End_button")
        {
            ExitPanel.SetActive(true);
            Debug.Log("���� ��ư Ŭ��");
        }
        else if (gameObject.name == "SETTINGBUTTON")
        {
            SettingPanel.SetActive(true);
            Debug.Log("���� ��ư Ŭ��");
        }
    }
}
