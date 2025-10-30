using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class inventorySlot : MonoBehaviour, IPointerClickHandler
{
    public ItemData item;

    [Header("UI References")]
    public Image itemicon;
    public GameObject emptySlotImage;

    void Start()
    {
        UpdateSlotUI();
    }

    public void Setitem(ItemData newItem)
    {
        item = newItem;
        UpdateSlotUI();
    }

    //UI 업데이트용 함수
    void UpdateSlotUI()
    {
        if (item != null)
        {
            itemicon.sprite = item.itemicon;
            itemicon.enabled = true;

            if (emptySlotImage != null)
            {
                emptySlotImage.SetActive(false);
            }
        }
        else
        {
            itemicon.enabled = false;
            if (emptySlotImage != null)
            {
                emptySlotImage.SetActive(true);
            }
        }
    }

    public void ClearSlot()
    {
        item = null;
        UpdateSlotUI();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // 1. 데이터 관리자(InventoryMAnager)가 아닌 'UI 관리자(InventoryUI)'를 호출합니다.
        if (InventoryUI.instance == null)
        {
            Debug.LogError("InventoryUI.instance is null. Is InventoryUI.cs in this scene?");
            return;
        }

        if (item != null)
        {
            // 2. InventoryUI의 ShowItemInfo 함수를 호출합니다.
            InventoryUI.instance.ShowItemInfo(item);
        }
        else
        {
            // 3. InventoryUI의 HideDesCripitonPanel 함수를 호출합니다.
            InventoryUI.instance.HideDesCripitonPanel();
        }
    }
}

