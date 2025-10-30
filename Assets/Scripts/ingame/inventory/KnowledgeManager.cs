using System.Collections.Generic;
using UnityEngine;
public class KnowledgeManager : MonoBehaviour
{
    public static KnowledgeManager Instance { get; private set; }

    // 감정 완료된 아이템 ID(int)들을 저장하는 리스트 (검색 속도가 빠른 HashSet 사용)
    private HashSet<int> identifiedItems = new HashSet<int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // 씬이 바뀌어도 이 오브젝트가 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 이미 인스턴스가 존재하면 이 오브젝트는 파괴
            Destroy(gameObject);
        }
    }

    public void MarkAsIdentified(int itemId)
    {
        if (!identifiedItems.Contains(itemId))
        {
            identifiedItems.Add(itemId);
        }
    }
    public bool HasIdentified(int itemId)
    {
        return identifiedItems.Contains(itemId);
    }
}

