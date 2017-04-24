using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Building
{
    house,
    woodshop,
}
public class PlayerContextView : UIContextView {

    public float RowPadding;
    public PlayerDataObject Bound;
    public Text Title;
    public Text Data;
    public Transform BuildRowsContainer;
    public Button GrowthRayButton;
    public BuildRowView buildRowViewPrefab;
    public Dictionary<Building, BuildRowView> BuildRows = new Dictionary<Building, BuildRowView>();
    public bool hasGrowthRay;

    public override void Bind(UIContextData bound)
    {
        Bound = (PlayerDataObject)bound;
        GrowthRayButton.onClick.AddListener(FindObjectOfType<WorldEnlarger>().GrowWorld);
    }

    void Update()
    {
        if (Bound == null) return;
        Title.text = Bound.type.ToString();
        Data.text = "Resources:\n";
        foreach(var kvp in Bound.Resources)
        {
            Data.text += string.Format(" -{0}: {1}\n", kvp.Key, kvp.Value);
        }
        Data.text += "\nItems:\n";
        foreach (var item in Bound.Items)
        {
            Data.text += string.Format(" -{0}\n", item.ToString());
        }
        Data.text += "\nStats:\n";
        Data.text += string.Format(" -Chop damage: {0}\n", Bound.ChopDMG);
        Data.text += string.Format(" -Speed: {0}\n", Bound.Speed);

        if (Bound.CanBuild)
        {
            Data.text += "\nBuild:\n";
            foreach (var kvp in Bound.Buildings)
            {
                if (!BuildRows.ContainsKey(kvp.Key))
                {
                    if (kvp.Value.OnDisplay)
                    {
                        var newRow = Instantiate<BuildRowView>(buildRowViewPrefab, BuildRowsContainer);
                        newRow.transform.localScale = Vector2.one;
                        newRow.NameValue.text = string.Format(" -{0}: {1} {2}\n", kvp.Key.ToString(), kvp.Value.value, kvp.Value.currency);
                        newRow.BuildButton.onClick.AddListener(() => Bound.Build(kvp.Key));
                        BuildRows.Add(kvp.Key, newRow);
                    }
                }
            }
            OrganizeRows();
        }

        GrowthRayButton.gameObject.SetActive(Bound.Items.Contains(Item.GrowthRay));


    }

    void OrganizeRows()
    {
        int count = 0;
        foreach (var kvp in BuildRows)
        {
            kvp.Value.transform.position = (Vector2)BuildRowsContainer.position + count * RowPadding * Vector2.down;
            count++;
        }
    }
}
