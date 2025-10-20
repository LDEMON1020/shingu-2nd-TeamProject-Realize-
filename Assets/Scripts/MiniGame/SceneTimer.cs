using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
    [Header("UI ���")]
    public TextMeshProUGUI timerText;

    [Header("Ÿ�̸� ���� �� ���̾�α�")]
    public DialogueManager dialogueManager;
    public DialogueDataSO linkedDialogue;

    [Header("�� �̵� ����")]
    public string sceneToLoad; //�̵��� �� �̸�

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

                StartCoroutine(EndSequence());
            }
        }
    }

    IEnumerator EndSequence()
    {
        // 1. Ȥ�� �ٸ� ��ȭ�� �־��ٸ� ���� ����
        if (dialogueManager.IsDialogueActive())
        {
            dialogueManager.SkipDialogue();
        }

        // 2. ����� ���̾�α׸� ����
        dialogueManager.StartDialogue(linkedDialogue);

        // 3. ���̾�αװ� Ȱ��ȭ�� ������ ��� ���
        yield return new WaitForSeconds(0.1f);

        // 4. ���̾�αװ� ���� ��(��Ȱ��ȭ�� ��)���� ��� ���
        while (dialogueManager.IsDialogueActive())
        {
            yield return null; // �� �����Ӿ� ���
        }

        // 5. ���̾�αװ� ��������, ������ ������ �̵�
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    // �ð��� ��:�� �������� �ؽ�Ʈ�� ǥ���ϴ� �Լ�
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // 1�ʺ��� ����

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}