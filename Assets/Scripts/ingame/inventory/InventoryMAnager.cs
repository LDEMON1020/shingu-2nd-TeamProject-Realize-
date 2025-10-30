using System.Collections.Generic;
using UnityEngine;
using System.Linq; 


public class InventoryMAnager : MonoBehaviour
{
    public static InventoryMAnager instance;

    [Header("인벤토리 설정")]
    public int inventorySize = 20;

    public List<ItemData> items = new List<ItemData>();


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // ▼ [추가] 씬이 바뀌어도 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool Additem(ItemData item)
    {
        // 데이터 리스트(items)의 개수를 확인
        if (items.Count >= inventorySize)
        {
            Debug.Log("인벤토리가 가득 찼습니다.");
            return false;
        }

        // 데이터 리스트에 바로 추가
        items.Add(item);
        return true;
    }

    public void RemoveItem(ItemData item)
    {
        // 데이터 리스트(items)에서 직접 제거
        if (items.Contains(item))
        {
            items.Remove(item);
        }
    }

    public bool HasItem(int itemID)
    {
        // items 리스트에서 itemID가 일치하는 아이템이 있는지 검색
        return items.Any(item => item.itemID == itemID);
    }
}

