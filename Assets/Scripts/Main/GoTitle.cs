using UnityEngine;
using UnityEngine.SceneManagement;

public class GoTitle : MonoBehaviour
{
    public GameObject GoTOT;

    private void OnMouseDown()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
