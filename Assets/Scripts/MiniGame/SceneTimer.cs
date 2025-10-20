using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
    [Header("UI 요소")]
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

                StartCoroutine(EndSequence());
            }
        }
    }

    IEnumerator EndSequence()
    {
        // 1. 혹시 다른 대화가 있었다면 강제 종료
        if (dialogueManager.IsDialogueActive())
        {
            dialogueManager.SkipDialogue();
        }

        // 2. 연결된 다이얼로그를 시작
        dialogueManager.StartDialogue(linkedDialogue);

        // 3. 다이얼로그가 활성화될 때까지 잠시 대기
        yield return new WaitForSeconds(0.1f);

        // 4. 다이얼로그가 끝날 때(비활성화될 때)까지 계속 대기
        while (dialogueManager.IsDialogueActive())
        {
            yield return null; // 한 프레임씩 대기
        }

        // 5. 다이얼로그가 끝났으니, 지정된 씬으로 이동
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
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
}