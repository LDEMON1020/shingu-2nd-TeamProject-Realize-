using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; private set; }

    [Header("패널 배열")]
    // 0번: Setting Panel, 1번: Exit Panel
    public GameObject[] panels;

    [Header("이동할 씬 이름")]
    public string gameSceneName = "YourGameSceneName";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CloseAllPanels();
    }

    public void StartGame()
    {
        // "Start" 버튼이 이 함수를 호출합니다.
        SceneManager.LoadScene(gameSceneName);
    }

    public void TogglePanel(int panelIndex)
    {
        // "Setting", "Exit" 버튼이 이 함수를 호출
        // 유효한 인덱스인지 확인
        if (panelIndex >= 0 && panelIndex < panels.Length)
        {
            // 해당 패널을 켜고 끔
            panels[panelIndex].SetActive(!panels[panelIndex].activeSelf);
        }
    }

    public void CloseAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료"); // 유니티 에디터에서는 Log로만 표시됩니다.
        Application.Quit();
    }
}