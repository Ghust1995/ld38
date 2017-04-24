using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreContextView : UIContextView
{

    public float RowPadding;
    public StoreDataObject Bound;
    public Text Title;
    public StoreRowView StoreRowViewPrefab;
    public Transform StoreRowsContainer;
    public Dictionary<Item, StoreRowView> ItemRows = new Dictionary<Item, StoreRowView>();

    public override void Bind(UIContextData bound)
    {
        Bound = (StoreDataObject)bound;
        Title.text = Bound.type.ToString();
    }

    void Update()
    {
        if (Bound == null) return;

        foreach (var kvp in Bound.Items)
        {
            if (!ItemRows.ContainsKey(kvp.Key))
            {
                if (kvp.Value.OnDisplay)
                {
                    var newRow = Instantiate<StoreRowView>(StoreRowViewPrefab, StoreRowsContainer);
                    newRow.transform.localScale = Vector2.one;
                    newRow.NameValue.text = string.Format("{0}: {1} {2}\n", kvp.Key.ToString(), kvp.Value.value, kvp.Value.currency);
                    newRow.PurchaseButton.onClick.AddListener(() => Bound.BuyItem(kvp.Key));
                    ItemRows.Add(kvp.Key, newRow);
                }
            }
            else
            {
                if (!kvp.Value.OnDisplay)
                {
                    Destroy(ItemRows[kvp.Key].gameObject);
                    ItemRows.Remove(kvp.Key);
                }
            }
        }

        OrganizeRows();
    }

    void OrganizeRows()
    {
        int count = 0;
        foreach (var kvp in ItemRows)
        {
            kvp.Value.transform.position = (Vector2)StoreRowsContainer.position + count * RowPadding * Vector2.down;
            count++;
        }
    }
}
