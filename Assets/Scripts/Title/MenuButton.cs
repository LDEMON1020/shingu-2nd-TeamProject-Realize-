using UnityEngine;

public class MenuButton : MonoBehaviour
{
    // 1. 인스펙터에서 이 버튼의 기능을 선택합니다.
    public enum ButtonType
    {
        Start,
        Settings,
        Exit,
        Quit
    }

    public ButtonType buttonType;

    [Header("패널 버튼 설정")]
    public int panelIndex = 0;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        transform.localScale = originalScale * 0.95f;

        if (MainMenuManager.Instance != null)
        {
            switch (buttonType)
            {
                case ButtonType.Start:
                    MainMenuManager.Instance.StartGame();
                    break;

                case ButtonType.Settings:
                    MainMenuManager.Instance.TogglePanel(panelIndex);
                    break;

                case ButtonType.Exit:
                    MainMenuManager.Instance.TogglePanel(panelIndex);
                    break;

                case ButtonType.Quit:
                    MainMenuManager.Instance.QuitGame();
                    break;
            }
        }
        else
        {
            Debug.LogError("MainMenuManager 인스턴스가 씬에 없습니다!");
        }
    }

    private void OnMouseUp()
    {
        transform.localScale = originalScale;
    }
}