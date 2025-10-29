using UnityEngine;

[RequireComponent(typeof(StageData))]
[RequireComponent(typeof(Collider2D))]
public class StageSecelted : MonoBehaviour
{
    private StageData myData;                       //오브젝트 고유 데이터
    private static StagePanelManager panelManager;  
    void Start()
    {
        myData = GetComponent<StageData>();              //자신의 StageData 가져오기

        if (panelManager == null)                           //stagemanager찾기
        {
            panelManager = FindObjectOfType<StagePanelManager>();

            if (panelManager == null)
            {
                Debug.LogError("씬(Scene)에 StagePanelManager가 없습니다!");
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
