using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StagePanelManager : MonoBehaviour
{
    [Header("�г� UI ���")]
    public GameObject stagePanel;            //ų �г�
    public Image stageImage;                 //ǥ�� �̹���
    public TextMeshProUGUI descriptionText;  //���� ����
    public Button enterButton;               //�������Թ�ư
    [Header("Exit��ư")]
    public Button exitButton;                //���׳������ư

    //���� ���� ����
    private string targetSceneName;          //������ �� �̸�

    void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ �̸� ����
        enterButton.onClick.AddListener(OnEnterButtonClick);

        // Exit ��ư�� �ִٸ� �ݱ� ��� ����
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ClosePanel);
        }

        // ������ �� �г� �ݾƵα�
        ClosePanel();
    }

    public void OpenStagePanel(StageData data)
    {
        //���޹��� �����ͷ� UI ������Ʈ
        stageImage.sprite = data.stageImage;       // �̹��� ����
        descriptionText.text = data.stageDescription; // �ؽ�Ʈ ����

        //�̵��� �� �̸� ����
        targetSceneName = data.sceneToLoad;

        //�г� ����
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
            Debug.LogError("�̵��� ���� �������� �ʾҽ��ϴ�!");
        }
    }

    public void ClosePanel()            //�г� �ݱ�
    {
        stagePanel.SetActive(false);
        targetSceneName = null; // �� �̸� �ʱ�ȭ
    }
}
