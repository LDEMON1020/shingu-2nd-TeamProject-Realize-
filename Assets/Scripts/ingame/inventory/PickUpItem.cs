using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public ItemData itemData;

    private void OnMouseDown()
    {
        if (InventoryMAnager.instance != null)
        {
            bool added = InventoryMAnager.instance.Additem(itemData);

            if (added)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory Full");
            }
        }
        else
        {
            Debug.LogError("인벤토리메니져 없음 ㅇㅇ 고치셈");
        }
    }
}
