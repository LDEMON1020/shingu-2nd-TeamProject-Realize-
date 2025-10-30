using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public static event Action OnDialogueEnd;

    [Header("UI ��� - Instector���� ����")]
    public GameObject DialoguePanel;                    //��ȭâ ��ü �г�(ó���� ��Ȱ��ȭ)
    public Image CharacterImage;                        //ĳ���� �̹��� ǥ���ϴ� Image UI
    public TextMeshProUGUI characternameText;           //ĳ���� �̸� ǥ���ϴ� �ؽ�Ʈ
    public TextMeshProUGUI dialogueText;                //��ȭ ���� ǥ���ϴ� �ؽ�Ʈ
    public GameObject[] interrogationButtons;          //�ɹ� ������ ��ư�� (���� ���� Ȱ��)

    [Header("�⺻ ����")]
    public Sprite defaultCharacterImage;                //ĳ���� �̹����� ���� �� ����� �⺻ �̹���

    [Header("Ÿ���� ȿ�� ����")]
    public float typingSpeed = 0.05f;                   //���� �ϳ��� ��� �ӵ� (�ʴ���)
    public bool skipTypingOnClick = true;               //Ŭ�� �� Ÿ���� ��� �Ϸ� �� �� ����

    //���� ������ 
    private DialogueDataSO currentDialogue;             //���� ���� ���� ��ȭ ������
    private int currentLineIndex = 0;                   //���� �� ��° ��ȭ ������ (0���� ����)
    private bool isDialogueActive = false;              //��ȭ�� ���� ������ Ȯ���ϴ� �÷���
    private bool isTyping = false;                      //���� Ÿ���� ȿ���� ���� ������ Ȯ��
    private Coroutine typingCoroutine;                  //Ÿ���� ȿ�� �ڷ�ƾ


    void Start()
    {
        // [����] �� ���� ������ ���� null üũ ��ȭ
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

    public bool IsDialogueActive() // (������ ����)
    {
        return isDialogueActive;
    }

    public void StartDialogue(DialogueDataSO dialogue)
    {
        if (dialogue == null || dialogue.dialogueLines.Count == 0) return;      //��ȭ ������ ���ų� ��ȭ ������ ��������� ���� ���� ����

        HideAllChoiceButtons();

        //��ȭ ���� �غ� (������ ����)
        currentDialogue = dialogue;
        currentLineIndex = 0;
        isDialogueActive = true;

        //UI ������Ʈ (������ ����)
        DialoguePanel.SetActive(true);
        characternameText.text = dialogue.characterName;

        if (CharacterImage != null)
        {
            if (dialogue.characterImage != null)
            {
                CharacterImage.sprite = dialogue.characterImage;        //��ȭ �������� �̹��� ���
            }
            else
            {
                CharacterImage.sprite = defaultCharacterImage;          //�⺻ �̹��� ���
            }
        }

        ShowCurrentLine();                                              //ù ��° ��ȭ ���� ǥ��
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
        // interrogationButtons �迭�� ������� �� ������ üũ
        if (interrogationButtons == null || interrogationButtons.Length == 0) return;

        ShowChoiceButtons(false);
    }
}

