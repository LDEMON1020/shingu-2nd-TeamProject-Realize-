using JetBrains.Annotations;
using TMPro.EditorUtilities;
using UnityEditor.UIElements;
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
        if (InventoryMAnager.instance == null) return;

        if (item != null)
        {
            InventoryMAnager.instance.ShowItemInfo(item);
        }
        else
        {
            InventoryMAnager.instance.HideDesCripitonPanel();
        }
    }
}
