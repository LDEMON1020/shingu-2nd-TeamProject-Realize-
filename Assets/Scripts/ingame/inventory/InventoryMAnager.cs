using System.Collections.Generic;
using UnityEngine;
using System.Linq; 


public class InventoryMAnager : MonoBehaviour
{
    public static InventoryMAnager instance;

    [Header("�κ��丮 ����")]
    public int inventorySize = 20;

    public List<ItemData> items = new List<ItemData>();


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // �� [�߰�] ���� �ٲ� �ı����� �ʵ��� ����
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool Additem(ItemData item)
    {
        // ������ ����Ʈ(items)�� ������ Ȯ��
        if (items.Count >= inventorySize)
        {
            Debug.Log("�κ��丮�� ���� á���ϴ�.");
            return false;
        }

        // ������ ����Ʈ�� �ٷ� �߰�
        items.Add(item);
        return true;
    }

    public void RemoveItem(ItemData item)
    {
        // ������ ����Ʈ(items)���� ���� ����
        if (items.Contains(item))
        {
            items.Remove(item);
        }
    }

    public bool HasItem(int itemID)
    {
        // items ����Ʈ���� itemID�� ��ġ�ϴ� �������� �ִ��� �˻�
        return items.Any(item => item.itemID == itemID);
    }
}

