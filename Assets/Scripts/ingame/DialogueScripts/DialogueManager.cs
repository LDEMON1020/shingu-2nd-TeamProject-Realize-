using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public static event Action OnDialogueEnd;

    [Header("UI 요소 - Instector에서 연결")]
    public GameObject DialoguePanel;                    //대화창 전체 패널(처음엔 비활성화)
    public Image CharacterImage;                        //캐릭터 이미지 표시하는 Image UI
    public TextMeshProUGUI characternameText;           //캐릭터 이름 표시하는 텍스트
    public TextMeshProUGUI dialogueText;                //대화 내용 표시하는 텍스트
    public GameObject[] interrogationButtons;          //심문 선택지 버튼들 (기존 변수 활용)

    [Header("기본 설정")]
    public Sprite defaultCharacterImage;                //캐릭터 이미지가 없을 때 사용할 기본 이미지

    [Header("타이핑 효과 설정")]
    public float typingSpeed = 0.05f;                   //글자 하나당 출력 속도 (초단위)
    public bool skipTypingOnClick = true;               //클릭 시 타이핑 즉시 완료 할 지 여부

    //내부 변수들 
    private DialogueDataSO currentDialogue;             //현재 진행 중인 대화 데이터
    private int currentLineIndex = 0;                   //현재 몇 번째 대화 중인지 (0부터 시작)
    private bool isDialogueActive = false;              //대화가 진행 중인지 확인하는 플래그
    private bool isTyping = false;                      //현재 타이핑 효과가 진행 중인지 확인
    private Coroutine typingCoroutine;                  //타이핑 효과 코루틴


    void Start()
    {
        // [수정] 널 오류 방지를 위해 null 체크 강화
        if (DialoguePanel != null)
        {
            DialoguePanel.SetActive(false);
        }
        ShowChoiceButtons(false);
    }
    void Update() 
    {
        if (!isDialogueActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping && skipTypingOnClick)
            {
                CompleteCurrentLine();
            }
            else if (!isTyping)
            {
                ShowNextLine();
            }
        }
    }

    public void ShowNextLine() 
    {
        currentLineIndex++;
        if (currentLineIndex < currentDialogue.dialogueLines.Count)
        {
            ShowCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    void ShowCurrentLine() 
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        dialogueText.text = "";
        typingCoroutine = StartCoroutine(TypeDialogue(currentDialogue.dialogueLines[currentLineIndex]));
    }

    IEnumerator TypeDialogue(string line)
    {
        isTyping = true;
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        typingCoroutine = null;
    }

    void CompleteCurrentLine() 
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        dialogueText.text = currentDialogue.dialogueLines[currentLineIndex];
        isTyping = false;
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        DialoguePanel.SetActive(false);
        currentDialogue = null;
        currentLineIndex = 0;

        OnDialogueEnd?.Invoke();
    }

    public void SkipAllDialogue()
    {
        EndDialogue();
    }

    public bool IsDialogueActive() // (기존과 동일)
    {
        return isDialogueActive;
    }

    public void StartDialogue(DialogueDataSO dialogue)
    {
        if (dialogue == null || dialogue.dialogueLines.Count == 0) return;      //대화 데이터 없거나 대화 내용이 비어있으면 실행 하지 않음

        HideAllChoiceButtons();

        //대화 시작 준비 (기존과 동일)
        currentDialogue = dialogue;
        currentLineIndex = 0;
        isDialogueActive = true;

        //UI 업데이트 (기존과 동일)
        DialoguePanel.SetActive(true);
        characternameText.text = dialogue.characterName;

        if (CharacterImage != null)
        {
            if (dialogue.characterImage != null)
            {
                CharacterImage.sprite = dialogue.characterImage;        //대화 데이터의 이미지 사용
            }
            else
            {
                CharacterImage.sprite = defaultCharacterImage;          //기본 이미지 사용
            }
        }

        ShowCurrentLine();                                              //첫 번째 대화 내용 표시
    }

    public void ShowChoiceButtons(bool show)
    {
        foreach (GameObject button in interrogationButtons)
        {
            if (button != null)
            {
                button.SetActive(show);
            }
        }
    }

    private void HideAllChoiceButtons()
    {
        // interrogationButtons 배열이 비어있을 수 있으니 체크
        if (interrogationButtons == null || interrogationButtons.Length == 0) return;

        ShowChoiceButtons(false);
    }
}

