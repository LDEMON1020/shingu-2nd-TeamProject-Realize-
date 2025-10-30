using UnityEngine;
using UnityEngine.UI;

public class StageEndManager : MonoBehaviour
{
    [Header("참조 연결 - Inspector")]
    public DialogueManager dialogueManager; // 씬에 있는 DialogueManager

    [Header("다이얼로그 데이터(SO)")]
    public DialogueDataSO initialDialogue; // 1. 타이머 종료 시 나올 초기 다이얼로그
    public DialogueDataSO dialogueChoiceA; // 3A. 선택지 A를 눌렀을 때 나올 다이얼로그
    public DialogueDataSO dialogueChoiceB; // 3B. 선택지 B를 눌렀을 때 나올 다이얼로그

    [Header("선택지 버튼 (DialogueManager의 배열 순서와 일치)")]
    // DialogueManager의 interrogationButtons 배열을 여기에 연결합니다.
    public Button choiceButtonA; // 예: 0번 버튼 (아이템 A 필요)
    public Button choiceButtonB; // 예: 1번 버튼 (아이템 B 필요)
    public Button choiceButtonC; // 예: 2번 버튼 (기본 선택지)

    [Header("아이템 ID 조건")]
    public int itemID_For_ChoiceA = 101; // 선택지 A에 필요한 아이템 ID
    public int itemID_For_ChoiceB = 102; // 선택지 B에 필요한 아이템 ID

    [Header("설명 수정 (감정)")]
    public int itemID_To_Identify = 101; // 선택지 A를 눌렀을 때 감정할 아이템 ID

    // --- 내부 상태 변수 ---
    private bool hasShownChoices = false; // 선택지를 한 번만 표시하기 위한 플래그

    void Start()
    {
        DialogueManager.OnDialogueEnd += HandleDialogueEnd;

        // 2. 버튼 클릭 리스너를 코드에서 직접 연결합니다. (인스펙터에서 연결해도 됩니다)
        if (choiceButtonA != null)
            choiceButtonA.onClick.AddListener(OnClick_ChoiceA);
        if (choiceButtonB != null)
            choiceButtonB.onClick.AddListener(OnClick_ChoiceB);
        if (choiceButtonC != null)
            choiceButtonC.onClick.AddListener(OnClick_ChoiceC);

        // 3. 시작 시 모든 선택지 버튼을 숨깁니다.
        dialogueManager.ShowChoiceButtons(false);
    }

    void OnDestroy()
    {
        // 씬이 파괴될 때 이벤트 구독을 해제합니다.
        DialogueManager.OnDialogueEnd -= HandleDialogueEnd;
    }

    public void OnTimerEnd()
    {
        hasShownChoices = false; // 상태 초기화
        dialogueManager.StartDialogue(initialDialogue);
    }
    private void HandleDialogueEnd()
    {
        if (hasShownChoices == false)
        {
            // 3. 선택지를 보여줄 차례
            ShowChoiceButtons();
        }
    }
    private void ShowChoiceButtons()
    {
        hasShownChoices = true; // 선택지를 표시했다고 플래그 설정

        // InventoryMAnager에게 아이템 소유 여부를 물어봅니다.
        bool hasItemA = InventoryMAnager.instance.HasItem(itemID_For_ChoiceA);
        bool hasItemB = InventoryMAnager.instance.HasItem(itemID_For_ChoiceB);

        // 조건에 따라 버튼 활성화/비활성화
        choiceButtonA.gameObject.SetActive(hasItemA);
        choiceButtonB.gameObject.SetActive(hasItemB);
        choiceButtonC.gameObject.SetActive(true); // 기본 선택지는 항상 활성화

        // DialogueManager의 버튼 배열을 '보이도록' 설정
        // (이미 버튼별로 SetActive를 했지만, 부모 패널을 켜는 용도)
        dialogueManager.ShowChoiceButtons(true);
    }

    // --- 버튼 클릭 함수들 ---

    public void OnClick_ChoiceA()
    {
        // 1. 선택지 버튼들을 모두 숨깁니다.
        dialogueManager.ShowChoiceButtons(false);

        // KnowledgeManager에게 이 아이템 ID를 "감정 완료"로 기록합니다.
        KnowledgeManager.Instance.MarkAsIdentified(itemID_To_Identify);

        // 3. 다음 다이얼로그(A)를 시작합니다.
        dialogueManager.StartDialogue(dialogueChoiceA);
    }

    public void OnClick_ChoiceB()
    {
        // 1. 선택지 버튼들을 모두 숨깁니다.
        dialogueManager.ShowChoiceButtons(false);

        // 2. 다음 다이얼로그(B)를 시작합니다.
        dialogueManager.StartDialogue(dialogueChoiceB);
    }

    public void OnClick_ChoiceC()
    {

        // 1. 선택지 버튼들을 모두 숨깁니다.
        dialogueManager.ShowChoiceButtons(false);

        // KnowledgeManager에게 이 아이템 ID를 "감정 완료"로 기록합니다.
        KnowledgeManager.Instance.MarkAsIdentified(itemID_To_Identify);

        // 3. 다음 다이얼로그(A)를 시작합니다.
        dialogueManager.StartDialogue(dialogueChoiceA);
    }
}

