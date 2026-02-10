using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField]
    public List<Item> items = new List<Item>();

    public System.Action OnAddItem;
    
    public void AddItem(Item _item)
    {
        items.Add(_item);
        OnAddItem?.Invoke();
    }
}
