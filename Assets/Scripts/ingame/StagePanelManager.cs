using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StagePanelManager : MonoBehaviour
{
    [Header("패널 UI 요소")]
    public GameObject stagePanel;            //킬 패널
    public Image stageImage;                 //표기 이미지
    public TextMeshProUGUI descriptionText;  //스테 설명
    public Button enterButton;               //스테진입버튼
    [Header("Exit버튼")]
    public Button exitButton;                //스테나가기버튼

    //내부 저장 변수
    private string targetSceneName;          //진입할 씬 이름

    void Start()
    {
        // 버튼 클릭 이벤트 미리 연결
        enterButton.onClick.AddListener(OnEnterButtonClick);

        // Exit 버튼이 있다면 닫기 기능 연결
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ClosePanel);
        }

        // 시작할 때 패널 닫아두기
        ClosePanel();
    }

    public void OpenStagePanel(StageData data)
    {
        //전달받은 데이터로 UI 업데이트
        stageImage.sprite = data.stageImage;       // 이미지 변경
        descriptionText.text = data.stageDescription; // 텍스트 변경

        //이동할 씬 이름 저장
        targetSceneName = data.sceneToLoad;

        //패널 열기
        stagePanel.SetActive(true);
    }

    public void OnEnterButtonClick()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("이동할 씬이 지정되지 않았습니다!");
        }
    }

    public void ClosePanel()            //패널 닫기
    {
        stagePanel.SetActive(false);
        targetSceneName = null; // 씬 이름 초기화
    }
}
