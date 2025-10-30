using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("������ ���� �ĺ�����")]
    public string itemName;
    public Sprite itemicon;
    public int itemID;

    [Header("������ ����")]
    [TextArea(5, 10)]
    public string unidentifiedDescription;

    [TextArea(5, 10)]
    public string identifiedDescription;
}
