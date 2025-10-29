using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("������ ���� �ĺ�����")]
    public string itenmName;
    public Sprite itemicon;
    public int itemID;
     
    [Header("������ ����")]
    [TextArea(5, 10)]
    public string itemDescription;
}
