using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerTest : MonoBehaviour
{
    [Header("UI 요소")]
    public GameObject miniGamePanel;
    public TextMeshProUGUI timerText;

    [Header("타이머 끝난 뒤 다이얼로그")]
    public DialogueManager dialogueManager;
    public DialogueDataSO linkedDialogue;

    [Header("씬 이동 설정")] 
    public string sceneToLoad; //이동할 씬 이름

    private float timeRemaining = 60f;
    private bool timerIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        
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

                StartCoroutine(EndSequence());
                // 여기서 다른 이벤트 발생 가능
            }
        }
    }

    IEnumerator EndSequence()
    {
        // 1. 미니게임 패널을 끔
        miniGamePanel.SetActive(false);

        // 2. 혹시 다른 대화가 있었다면 강제 종료
        if (dialogueManager.IsDialogueActive())
        {
            dialogueManager.SkipDialogue();
        }

        // 3. 연결된 다이얼로그를 시작
        dialogueManager.StartDialogue(linkedDialogue);

        // 4. 다이얼로그가 활성화될 때까지 잠시 대기
        yield return new WaitForSeconds(0.1f);

        // 5. 다이얼로그가 끝날 때(비활성화될 때)까지 계속 대기
        while (dialogueManager.IsDialogueActive())
        {
            yield return null; // 한 프레임씩 대기
        }

        // 6. 다이얼로그가 끝났으니, 지정된 씬으로 이동
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // 씬 이름이 지정되지 않았으면 이동하지 않고 넘어감
        }
    }

    // 시간을 분:초 형식으로 텍스트에 표시하는 함수
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // 1초부터 시작

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //버튼 누르고 호출되는 함수
    public void OpenPanelAndStartTimer()
    {
        // 패널 활성화
        miniGamePanel.SetActive(true);

        // 타이머 초기화 및 시작
        timeRemaining = 60f;
        timerIsRunning = true;
    }
}
