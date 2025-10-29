using UnityEngine;

public class MenuButton : MonoBehaviour
{
    // 1. �ν����Ϳ��� �� ��ư�� ����� �����մϴ�.
    public enum ButtonType
    {
        Start,
        Settings,
        Exit,
        Quit
    }

    public ButtonType buttonType;

    [Header("�г� ��ư ����")]
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
            Debug.LogError("MainMenuManager �ν��Ͻ��� ���� �����ϴ�!");
        }
    }

    private void OnMouseUp()
    {
        transform.localScale = originalScale;
    }
}