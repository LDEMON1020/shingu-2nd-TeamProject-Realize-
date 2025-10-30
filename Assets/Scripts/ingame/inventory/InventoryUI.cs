using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    // 이 '씬'의 UI 싱글톤
    public static InventoryUI instance;

    [Header("UI 설정")]
    public GameObject inventroyPanelObject; // 이 스크립트가 붙어있는 패널
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
        // 씬 전용 싱글톤 설정
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {

        // 데이터 매니저(InventoryMAnager)에서 인벤토리 크기를 가져와 슬롯 생성
        CreateInventorySlots(InventoryMAnager.instance.inventorySize);

        // inventroyUI 변수 대신 이 스크립트가 붙은 게임오브젝트를 참조
        if (inventroyPanelObject == null)
        {
            inventroyPanelObject = this.gameObject;
        }
        inventroyPanelObject.SetActive(isInventoryOpen);

        // 설명 패널 숨기기 (기존 오타 'HideDesCripitonPanel' 유지)
        HideDesCripitonPanel();
    }

    void Update()
    {
        if (Input.GetKeyDown(inventoryToggleKey))
        {
            ToggleInventory();
        }
    }

    void CreateInventorySlots(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject slotObject = Instantiate(itemSlotPrefab, itemSlotParent);
            inventorySlot slot = slotObject.GetComponent<inventorySlot>();
            itemSlots.Add(slot);
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventroyPanelObject.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            UpdateInventoryUI();
        }

        if (!isInventoryOpen)
        {
            HideDesCripitonPanel();
        }
    }
    public void UpdateInventoryUI()
    {
        // 1. 데이터 매니저(InventoryMAnager)에서 현재 아이템 리스트를 가져옴
        List<ItemData> currentItems = InventoryMAnager.instance.items;

        // 2. 이 씬의 모든 UI 슬롯(itemSlots)을 순회
        for (int i = 0; i < itemSlots.Count; i++)
        {
            // 3. 데이터 리스트에 아이템이 존재하면 슬롯에 채움
            if (i < currentItems.Count)
            {
                itemSlots[i].Setitem(currentItems[i]);
            }
            // 4. 데이터가 없으면 슬롯을 비움
            else
            {
                itemSlots[i].ClearSlot();
            }
        }
    }
    public void ShowItemInfo(ItemData data)
    {
        if (data == null)
        {
            HideDesCripitonPanel();
            return;
        }

        descriptionImage.sprite = data.itemicon;
        descriptionName.text = data.itemName; // (ItemData.cs의 변수명 사용)

        if (KnowledgeManager.Instance.HasIdentified(data.itemID))
        {
            // 감정 완료 시: 'identifiedDescription' 표시
            descriptionText.text = data.identifiedDescription;
        }
        else
        {
            // 감정 전: 'unidentifiedDescription' 표시
            descriptionText.text = data.unidentifiedDescription;
        }

        descriptionPanel.SetActive(true);
        descriptionImage.enabled = true;
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

