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

    //UI ������Ʈ�� �Լ�
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
        // 1. ������ ������(InventoryMAnager)�� �ƴ� 'UI ������(InventoryUI)'�� ȣ���մϴ�.
        if (InventoryUI.instance == null)
        {
            Debug.LogError("InventoryUI.instance is null. Is InventoryUI.cs in this scene?");
            return;
        }

        if (item != null)
        {
            // 2. InventoryUI�� ShowItemInfo �Լ��� ȣ���մϴ�.
            InventoryUI.instance.ShowItemInfo(item);
        }
        else
        {
            // 3. InventoryUI�� HideDesCripitonPanel �Լ��� ȣ���մϴ�.
            InventoryUI.instance.HideDesCripitonPanel();
        }
    }
}

