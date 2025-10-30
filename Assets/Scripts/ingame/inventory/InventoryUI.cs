using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    // �� '��'�� UI �̱���
    public static InventoryUI instance;

    [Header("UI ����")]
    public GameObject inventroyPanelObject; // �� ��ũ��Ʈ�� �پ��ִ� �г�
    public Transform itemSlotParent;
    public GameObject itemSlotPrefab;

    [Header("Ű �Է�")]
    public KeyCode inventoryToggleKey = KeyCode.I;

    public List<inventorySlot> itemSlots = new List<inventorySlot>();
    private bool isInventoryOpen = false;

    [Header("������ �����г�")]
    public Image descriptionImage;
    public TextMeshProUGUI descriptionName;
    public TextMeshProUGUI descriptionText;
    public GameObject descriptionPanel;

    void Awake()
    {
        // �� ���� �̱��� ����
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {

        // ������ �Ŵ���(InventoryMAnager)���� �κ��丮 ũ�⸦ ������ ���� ����
        CreateInventorySlots(InventoryMAnager.instance.inventorySize);

        // inventroyUI ���� ��� �� ��ũ��Ʈ�� ���� ���ӿ�����Ʈ�� ����
        if (inventroyPanelObject == null)
        {
            inventroyPanelObject = this.gameObject;
        }
        inventroyPanelObject.SetActive(isInventoryOpen);

        // ���� �г� ����� (���� ��Ÿ 'HideDesCripitonPanel' ����)
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
        // 1. ������ �Ŵ���(InventoryMAnager)���� ���� ������ ����Ʈ�� ������
        List<ItemData> currentItems = InventoryMAnager.instance.items;

        // 2. �� ���� ��� UI ����(itemSlots)�� ��ȸ
        for (int i = 0; i < itemSlots.Count; i++)
        {
            // 3. ������ ����Ʈ�� �������� �����ϸ� ���Կ� ä��
            if (i < currentItems.Count)
            {
                itemSlots[i].Setitem(currentItems[i]);
            }
            // 4. �����Ͱ� ������ ������ ���
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
        descriptionName.text = data.itemName; // (ItemData.cs�� ������ ���)

        if (KnowledgeManager.Instance.HasIdentified(data.itemID))
        {
            // ���� �Ϸ� ��: 'identifiedDescription' ǥ��
            descriptionText.text = data.identifiedDescription;
        }
        else
        {
            // ���� ��: 'unidentifiedDescription' ǥ��
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

