using UnityEngine;
using UnityEngine.UI;

public class StageEndManager : MonoBehaviour
{
    [Header("���� ���� - Inspector")]
    public DialogueManager dialogueManager; // ���� �ִ� DialogueManager

    [Header("���̾�α� ������(SO)")]
    public DialogueDataSO initialDialogue; // 1. Ÿ�̸� ���� �� ���� �ʱ� ���̾�α�
    public DialogueDataSO dialogueChoiceA; // 3A. ������ A�� ������ �� ���� ���̾�α�
    public DialogueDataSO dialogueChoiceB; // 3B. ������ B�� ������ �� ���� ���̾�α�

    [Header("������ ��ư (DialogueManager�� �迭 ������ ��ġ)")]
    // DialogueManager�� interrogationButtons �迭�� ���⿡ �����մϴ�.
    public Button choiceButtonA; // ��: 0�� ��ư (������ A �ʿ�)
    public Button choiceButtonB; // ��: 1�� ��ư (������ B �ʿ�)
    public Button choiceButtonC; // ��: 2�� ��ư (�⺻ ������)

    [Header("������ ID ����")]
    public int itemID_For_ChoiceA = 101; // ������ A�� �ʿ��� ������ ID
    public int itemID_For_ChoiceB = 102; // ������ B�� �ʿ��� ������ ID

    [Header("���� ���� (����)")]
    public int itemID_To_Identify = 101; // ������ A�� ������ �� ������ ������ ID

    // --- ���� ���� ���� ---
    private bool hasShownChoices = false; // �������� �� ���� ǥ���ϱ� ���� �÷���

    void Start()
    {
        DialogueManager.OnDialogueEnd += HandleDialogueEnd;

        // 2. ��ư Ŭ�� �����ʸ� �ڵ忡�� ���� �����մϴ�. (�ν����Ϳ��� �����ص� �˴ϴ�)
        if (choiceButtonA != null)
            choiceButtonA.onClick.AddListener(OnClick_ChoiceA);
        if (choiceButtonB != null)
            choiceButtonB.onClick.AddListener(OnClick_ChoiceB);
        if (choiceButtonC != null)
            choiceButtonC.onClick.AddListener(OnClick_ChoiceC);

        // 3. ���� �� ��� ������ ��ư�� ����ϴ�.
        dialogueManager.ShowChoiceButtons(false);
    }

    void OnDestroy()
    {
        // ���� �ı��� �� �̺�Ʈ ������ �����մϴ�.
        DialogueManager.OnDialogueEnd -= HandleDialogueEnd;
    }

    public void OnTimerEnd()
    {
        hasShownChoices = false; // ���� �ʱ�ȭ
        dialogueManager.StartDialogue(initialDialogue);
    }
    private void HandleDialogueEnd()
    {
        if (hasShownChoices == false)
        {
            // 3. �������� ������ ����
            ShowChoiceButtons();
        }
    }
    private void ShowChoiceButtons()
    {
        hasShownChoices = true; // �������� ǥ���ߴٰ� �÷��� ����

        // InventoryMAnager���� ������ ���� ���θ� ����ϴ�.
        bool hasItemA = InventoryMAnager.instance.HasItem(itemID_For_ChoiceA);
        bool hasItemB = InventoryMAnager.instance.HasItem(itemID_For_ChoiceB);

        // ���ǿ� ���� ��ư Ȱ��ȭ/��Ȱ��ȭ
        choiceButtonA.gameObject.SetActive(hasItemA);
        choiceButtonB.gameObject.SetActive(hasItemB);
        choiceButtonC.gameObject.SetActive(true); // �⺻ �������� �׻� Ȱ��ȭ

        // DialogueManager�� ��ư �迭�� '���̵���' ����
        // (�̹� ��ư���� SetActive�� ������, �θ� �г��� �Ѵ� �뵵)
        dialogueManager.ShowChoiceButtons(true);
    }

    // --- ��ư Ŭ�� �Լ��� ---

    public void OnClick_ChoiceA()
    {
        // 1. ������ ��ư���� ��� ����ϴ�.
        dialogueManager.ShowChoiceButtons(false);

        // KnowledgeManager���� �� ������ ID�� "���� �Ϸ�"�� ����մϴ�.
        KnowledgeManager.Instance.MarkAsIdentified(itemID_To_Identify);

        // 3. ���� ���̾�α�(A)�� �����մϴ�.
        dialogueManager.StartDialogue(dialogueChoiceA);
    }

    public void OnClick_ChoiceB()
    {
        // 1. ������ ��ư���� ��� ����ϴ�.
        dialogueManager.ShowChoiceButtons(false);

        // 2. ���� ���̾�α�(B)�� �����մϴ�.
        dialogueManager.StartDialogue(dialogueChoiceB);
    }

    public void OnClick_ChoiceC()
    {

        // 1. ������ ��ư���� ��� ����ϴ�.
        dialogueManager.ShowChoiceButtons(false);

        // KnowledgeManager���� �� ������ ID�� "���� �Ϸ�"�� ����մϴ�.
        KnowledgeManager.Instance.MarkAsIdentified(itemID_To_Identify);

        // 3. ���� ���̾�α�(A)�� �����մϴ�.
        dialogueManager.StartDialogue(dialogueChoiceA);
    }
}

