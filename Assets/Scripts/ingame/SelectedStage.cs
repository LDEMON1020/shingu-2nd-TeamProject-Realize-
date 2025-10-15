using UnityEngine;

//시현 아이디어
//짜피 우리 글만 바뀌는거니까 글만 바꾸고 하고 조사하기 버튼은 층에 메겨진 변수따라 달라지게 하고

public class SelectedStage : MonoBehaviour
{
    [Header("Stage Panel")]
    public GameObject StageP;
    public GameObject StageP2;
    public GameObject StageP3;
    [Header("Stage")]
    public GameObject Stage;
    public GameObject Stage2;
    public GameObject Stage3;
    [Header("꺼야하는거")]
    public GameObject ininpanel;

    void Start()
    {
        StageP.SetActive(false);
        StageP2.SetActive(false);
        StageP3.SetActive(false);
        Debug.Log("시작, 판넬 오프");
    }

    private void OnMouseDown()
    {
        if (Stage)
        {
            StageP.SetActive(true);
            ininpanel.SetActive(false);
            Stage.SetActive(false);
            Stage2.SetActive(false);
            Stage3.SetActive(false);
            Debug.Log("스테이지1 선택");
        }
        else if (Stage2)
        {
            StageP2.SetActive(true);
            Stage.SetActive(false);
            Stage2.SetActive(false);
            Stage3.SetActive(false);
            Debug.Log("스테이지2 선택");
        }
        else if (Stage3)
        {
            StageP3.SetActive(true);
            Stage.SetActive(false);
            Stage2.SetActive(false);
            Stage3.SetActive(false);
            Debug.Log("스테이지3 선택");
        }

        
    }

    public void endpanel()
    {
        Stage.SetActive(true);
        Stage2.SetActive(true);
        Stage3.SetActive(true);
    }
}
