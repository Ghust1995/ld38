using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeContextView : UIContextView {

    public TreeDataObject Bound;
    public Text Title;
    public Button Chop;
    public Text Health;
    public Text Wood;

    public override void Bind(UIContextData bound)
    {
        Bound = (TreeDataObject)bound;
        Chop.onClick.RemoveAllListeners();
        Chop.onClick.AddListener(() => Bound.Damage(FindObjectOfType<PlayerDataObject>().ChopDMG));
    }

    void Update()
    {
        if (Bound == null) return;
        Title.text = Bound.type.ToString();
        Health.text = "Health Points: " + Bound.Life;
        Wood.text = "Wood: " + Bound.Wood;
    }
}
