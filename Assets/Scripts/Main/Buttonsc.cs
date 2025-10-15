using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonsc : MonoBehaviour
{
    public GameObject ExitPanel;
    public GameObject SettingPanel;

    //버튼 실행 스타트

    public void Start()
    {
        ExitPanel.SetActive(false);
        SettingPanel.SetActive(false);
        Debug.Log("시작, 판넬 오프");
    }

    private void OnMouseDown()
    {
        if (gameObject.name == "Start_button")
        {
            SceneManager.LoadScene("Main");
            Debug.Log("게임 시작");
        }
        else if (gameObject.name == "End_button")
        {
            ExitPanel.SetActive(true);
            Debug.Log("종료 버튼 클릭");
        }
        else if (gameObject.name == "SETTINGBUTTON")
        {
            SettingPanel.SetActive(true);
            Debug.Log("세팅 버튼 클릭");
        }
    }
}
