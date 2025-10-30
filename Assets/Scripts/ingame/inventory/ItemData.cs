using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("아이템 고유 식별정보")]
    public string itemName;
    public Sprite itemicon;
    public int itemID;
     
    [Header("아이템 설명")]
    [TextArea(5, 10)]
    public string itemDescription;
}
