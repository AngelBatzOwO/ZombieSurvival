using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIBehaviour : MonoBehaviour
{

    [SerializeField]
    Inventory inventory;

    [SerializeField]
    GameObject itemButtonPrefab;

    [SerializeField]
    Transform itemButtonContainer;
    void OnEnable()
    {
        UpdateUIButtons();
        inventory.OnAddItem += UpdateUIButtons;
    }

    private void OnDisable()
    {
        inventory.OnAddItem -= UpdateUIButtons;
    }

    private void UpdateUIButtons()
    {

        for (int i = 0; i < itemButtonContainer.childCount; i++)
        {
            Destroy(itemButtonContainer.GetChild(i));
        }
        for (int i = 0; i < inventory.items.Count; i++)
        {
            GameObject _itemButton = Instantiate(itemButtonPrefab, itemButtonContainer);
            _itemButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = inventory.items[i].name;
     //     _itemButton.GetComponent<Image>().sprite = inventory.items[i].icon;
        }
    }
}