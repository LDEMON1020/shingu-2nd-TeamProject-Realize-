using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; private set; }

    [Header("�г� �迭")]
    // 0��: Setting Panel, 1��: Exit Panel
    public GameObject[] panels;

    [Header("�̵��� �� �̸�")]
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
        // "Start" ��ư�� �� �Լ��� ȣ���մϴ�.
        SceneManager.LoadScene(gameSceneName);
    }

    public void TogglePanel(int panelIndex)
    {
        // "Setting", "Exit" ��ư�� �� �Լ��� ȣ��
        // ��ȿ�� �ε������� Ȯ��
        if (panelIndex >= 0 && panelIndex < panels.Length)
        {
            // �ش� �г��� �Ѱ� ��
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
        Debug.Log("���� ����"); // ����Ƽ �����Ϳ����� Log�θ� ǥ�õ˴ϴ�.
        Application.Quit();
    }
}