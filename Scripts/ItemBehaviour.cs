using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    [SerializeField]
    Item item;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Inventory inventory))
        {
            inventory.AddItem(item);
            Destroy(this.gameObject);
        }
        
    }
}