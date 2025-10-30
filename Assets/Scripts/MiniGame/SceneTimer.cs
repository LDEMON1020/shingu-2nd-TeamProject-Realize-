using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
    [Header("UI 요소")]
    public TextMeshProUGUI timerText;

    [Header("스테이지 종료 로직 연결")]
    public StageEndManager stageEndManager;

    private float timeRemaining = 60f;
    private bool timerIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        // 씬이 시작되면 바로 타이머를 60초로 설정하고 시작
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
                // 1초씩 시간을 감소시킴
                timeRemaining -= Time.deltaTime;
                // 시간 표시 함수 호출
                DisplayTime(timeRemaining);
            }
            else
            {
                // 시간이 0이 되면 타이머 종료
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayTime(timeRemaining);

                // 2. StageEndManager를 직접 호출합니다.
                if (stageEndManager != null)
                {
                    // StageEndManager에게 "타이머 끝났다"고 신호를 보냄
                    stageEndManager.OnTimerEnd();
                }
                else
                {
                    Debug.LogError("StageEndManager가 SceneTimer에 연결되지 않았습니다!");
                }
            }
        }
    }
    // 시간을 분:초 형식으로 텍스트에 표시하는 함수 (기존과 동일)
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // 1초부터 시작

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
