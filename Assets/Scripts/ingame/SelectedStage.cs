using UnityEngine;

//���� ���̵��
//¥�� �츮 �۸� �ٲ�°Ŵϱ� �۸� �ٲٰ� �ϰ� �����ϱ� ��ư�� ���� �ް��� �������� �޶����� �ϰ�

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
    [Header("�����ϴ°�")]
    public GameObject ininpanel;

    void Start()
    {
        StageP.SetActive(false);
        StageP2.SetActive(false);
        StageP3.SetActive(false);
        Debug.Log("����, �ǳ� ����");
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
            Debug.Log("��������1 ����");
        }
        else if (Stage2)
        {
            StageP2.SetActive(true);
            Stage.SetActive(false);
            Stage2.SetActive(false);
            Stage3.SetActive(false);
            Debug.Log("��������2 ����");
        }
        else if (Stage3)
        {
            StageP3.SetActive(true);
            Stage.SetActive(false);
            Stage2.SetActive(false);
            Stage3.SetActive(false);
            Debug.Log("��������3 ����");
        }

        
    }

    public void endpanel()
    {
        Stage.SetActive(true);
        Stage2.SetActive(true);
        Stage3.SetActive(true);
    }
}
