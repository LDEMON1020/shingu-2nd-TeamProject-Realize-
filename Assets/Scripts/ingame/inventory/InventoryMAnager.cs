using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMAnager : MonoBehaviour
{
    public static InventoryMAnager instance;

    [Header("인벤토리 설정")]
    public int inventorySize = 20;
    public GameObject inventroyUI;
    public Transform itemSlotParent;
    public GameObject itemSlotPrefab;

    [Header("키 입력")]
    public KeyCode inventoryToggleKey = KeyCode.I;
    public List<inventorySlot> itemSlots = new List<inventorySlot>();
    private bool isInventoryOpen = false;

    [Header("아이템 정보패널")]
    public Image descriptionImage;
    public TextMeshProUGUI descriptionName;
    public TextMeshProUGUI descriptionText;
    public GameObject descriptionPanel;

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        CreateInventorySlots();
        inventroyUI.SetActive(isInventoryOpen);
        HideDesCripitonPanel();
    }

    void Update()
    {
        if (Input.GetKeyDown(inventoryToggleKey))
        {
            ToggleInventory();
        }
    }

    void CreateInventorySlots()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject slotObject = Instantiate(itemSlotPrefab, itemSlotParent);
            inventorySlot slot = slotObject.GetComponent<inventorySlot>();
            itemSlots.Add(slot);
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventroyUI.SetActive(isInventoryOpen);

        if (!isInventoryOpen)
        {
            HideDesCripitonPanel();
        }
    }

    public bool Additem(ItemData item)
    {
       foreach (inventorySlot slot in itemSlots)
       {
           if (slot.item == null)
           {
               slot.Setitem(item);
               return true;
           }
        }
       Debug.Log("인벤토리가 가득 찼습니다.");
        return false;
    }

    public void RemoveItem(ItemData item)
    {
        foreach (inventorySlot slot in itemSlots)
        {
            if (slot.item == item)
            {
                slot.ClearSlot();
                return;
            }
        }
    }

    //아이템 정보 패널 표시

    public void ShowItemInfo(ItemData data)
    {
        if (data == null)
        {
            return;
        }

        descriptionImage.sprite = data.itemicon;
        descriptionName.text = data.itenmName;
        descriptionText.text = data.itemDescription;

        descriptionPanel.SetActive(true);
        descriptionImage .enabled = true;
    }

    public void HideDesCripitonPanel()
    {
        descriptionPanel.SetActive(false);
        descriptionName.text = "";
        descriptionText.text = "";
        descriptionImage.sprite = null;
        descriptionImage.enabled = false;
    }
}
