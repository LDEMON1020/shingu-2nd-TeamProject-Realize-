using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
    [Header("UI ���")]
    public TextMeshProUGUI timerText;

    [Header("�������� ���� ���� ����")]
    public StageEndManager stageEndManager;

    private float timeRemaining = 60f;
    private bool timerIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        // ���� ���۵Ǹ� �ٷ� Ÿ�̸Ӹ� 60�ʷ� �����ϰ� ����
        timeRemaining = 60f;
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // 1�ʾ� �ð��� ���ҽ�Ŵ
                timeRemaining -= Time.deltaTime;
                // �ð� ǥ�� �Լ� ȣ��
                DisplayTime(timeRemaining);
            }
            else
            {
                // �ð��� 0�� �Ǹ� Ÿ�̸� ����
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayTime(timeRemaining);

                // 2. StageEndManager�� ���� ȣ���մϴ�.
                if (stageEndManager != null)
                {
                    // StageEndManager���� "Ÿ�̸� ������"�� ��ȣ�� ����
                    stageEndManager.OnTimerEnd();
                }
                else
                {
                    Debug.LogError("StageEndManager�� SceneTimer�� ������� �ʾҽ��ϴ�!");
                }
            }
        }
    }
    // �ð��� ��:�� �������� �ؽ�Ʈ�� ǥ���ϴ� �Լ� (������ ����)
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // 1�ʺ��� ����

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
