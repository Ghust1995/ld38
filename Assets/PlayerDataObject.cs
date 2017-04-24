using System.Collections.Generic;
using UnityEngine;

public enum Resource
{
    wood,
    money,
}

public class PlayerDataObject : UIContextData
{
    public Dictionary<Resource, long> Resources = new Dictionary<Resource, long>();
    public bool CanBuild
    {
        get
        {
            return Items.Contains(Item.Hammer);
        }
    }
    public Dictionary<Building, Price> Buildings = new Dictionary<Building, Price>()
    {
        { Building.house, new Price() { value=30, currency=Resource.wood}},
        { Building.woodshop, new Price() { value=20, currency=Resource.wood}}
    };

    public void Build(Building b)
    {
        if (!TryRemoveResource(Buildings[b].currency, Buildings[b].value)) return;
        switch(b)
        {
            case Building.house:
                StartCoroutine(FindObjectOfType<ObjectSpawner>().SpawnNewHouse());
                break;
            case Building.woodshop:
                StartCoroutine(FindObjectOfType<ObjectSpawner>().SpawnNewWoodshop());
                break;
        }
    }
    public HashSet<Item> Items = new HashSet<Item>();

    public float BaseSpeed = 0.5f;
    public float SandalsSpeed = 1.0f;
    public float ShoesSpeed = 1.5f;
    public float RunningShoesSpeed = 2.0f;
    public float Speed
    {
        get
        {
            if (Items.Contains(Item.RunningShoes))
            {
                return RunningShoesSpeed;
            }
            if (Items.Contains(Item.Shoes))
            {
                return ShoesSpeed;
            }
            if (Items.Contains(Item.Sandals))
            {
                return SandalsSpeed;
            }
            return BaseSpeed;
        }
    }

    public int BaseDMG = 1;
    public int AxeDMG = 2;
    public int SawDMG = 3;
    public int ChainsawDMG = 4;
    public int ChopDMG {
        get
        {
            if (Items.Contains(Item.Chainsaw))
            {
                return ChainsawDMG;
            }
            if (Items.Contains(Item.Saw))
            {
                return SawDMG;
            }
            if (Items.Contains(Item.Axe))
            {
                return AxeDMG;
            }
            
            

            return BaseDMG;
        }
    }

    public void AddResource(Resource res, long val)
    {
        if(!Resources.ContainsKey(res))
        {
            Resources.Add(res, 0);
        }
        Resources[res] += val;
    }

    public bool TryRemoveResource(Resource res, long val)
    {
        if (!Resources.ContainsKey(res) || Resources[res] < val)
        {
            return false;

        }
        Resources[res] -= val;
        return true;
    }

}
