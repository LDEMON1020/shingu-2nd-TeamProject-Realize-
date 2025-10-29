using UnityEngine;

[RequireComponent(typeof(StageData))]
[RequireComponent(typeof(Collider2D))]
public class StageSecelted : MonoBehaviour
{
    private StageData myData;                       //������Ʈ ���� ������
    private static StagePanelManager panelManager;  
    void Start()
    {
        myData = GetComponent<StageData>();              //�ڽ��� StageData ��������

        if (panelManager == null)                           //stagemanagerã��
        {
            panelManager = FindObjectOfType<StagePanelManager>();

            if (panelManager == null)
            {
                Debug.LogError("��(Scene)�� StagePanelManager�� �����ϴ�!");
            }
        }
    }

    void OnMouseDown()
    {
        if (panelManager != null)
        {
            panelManager.OpenStagePanel(myData);
        }
    }
}
