using System.Collections.Generic;
using UnityEngine;
public class KnowledgeManager : MonoBehaviour
{
    public static KnowledgeManager Instance { get; private set; }

    // ���� �Ϸ�� ������ ID(int)���� �����ϴ� ����Ʈ (�˻� �ӵ��� ���� HashSet ���)
    private HashSet<int> identifiedItems = new HashSet<int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // ���� �ٲ� �� ������Ʈ�� �ı����� �ʵ��� ����
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // �̹� �ν��Ͻ��� �����ϸ� �� ������Ʈ�� �ı�
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

