using UnityEngine;
using UnityEngine.UI;

public class WoodshopContextView : UIContextView
{

    public WoodshopDataObject Bound;
    public Text Title;
    public Text Occupancy;

    public override void Bind(UIContextData bound)
    {
        Bound = (WoodshopDataObject)bound;
        Title.text = "Lumberyard";
    }

    void Update()
    {
        if (Bound == null) return;
        Occupancy.text = string.Format("Workers : {0} / {1}", Bound.NumWorkers, Bound.MaxOccupancy);
    }
}