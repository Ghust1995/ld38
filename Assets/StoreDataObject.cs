using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    Axe,
    Hammer,
    Saw,
    Chainsaw,
    Sandals,
    Shoes,
    RunningShoes,
    Fertilizer,
    Humidifier,
    GrowthRay,
    Test = 1000,
}
public class StoreDataObject : UIContextData
{

    public float ValueMultiplier = 1.0f;

    public Dictionary<Item, Price> Items = InitialItems();

    public static Func<bool> PlayerHasItemFunc(Item item)
    {
        return () => FindObjectOfType<PlayerDataObject>().Items.Contains(item);
    }

    public static Func<bool> Fand(Func<bool> func1, Func<bool> func2)
    {
        return () => func1() && func2();
    }

    public static Func<bool> Fnot(Func<bool> func)
    {
        return () => !func();
    }

    public static Dictionary<Item, Price> InitialItems(float scaling = 1.0f)
    {
        return new Dictionary<Item, Price>
        {
            {Item.Axe,          new Price() {value =  (int)( (float)10 * scaling), currency=Resource.wood, condition = Fnot(PlayerHasItemFunc(Item.Axe)) } },
            {Item.Saw,          new Price() {value =  (int)( (float)50 * scaling), currency=Resource.wood, condition = Fand(PlayerHasItemFunc(Item.Axe), Fnot(PlayerHasItemFunc(Item.Saw))) } },
            {Item.Chainsaw,     new Price() {value = (int)( (float)100 * scaling), currency=Resource.wood, condition = Fand(PlayerHasItemFunc(Item.Saw), Fnot(PlayerHasItemFunc(Item.Chainsaw))) } },
            {Item.Hammer,       new Price() {value =  (int)( (float)15 * scaling), currency=Resource.wood, condition = Fnot(PlayerHasItemFunc(Item.Hammer)) } },
            {Item.Sandals,      new Price() {value =  (int)( (float)10 * scaling), currency=Resource.wood, condition = Fnot(PlayerHasItemFunc(Item.Sandals)) } },
            {Item.Shoes,        new Price() {value =  (int)( (float)50 * scaling), currency=Resource.wood, condition = Fand(PlayerHasItemFunc(Item.Sandals), Fnot(PlayerHasItemFunc(Item.Shoes))) } },
            {Item.RunningShoes, new Price() {value = (int)( (float)100 * scaling), currency=Resource.wood, condition = Fand(PlayerHasItemFunc(Item.Shoes), Fnot(PlayerHasItemFunc(Item.RunningShoes))) } },
            {Item.Fertilizer,   new Price() {value =  (int)( (float)20 * scaling), currency=Resource.wood, condition = Fnot(PlayerHasItemFunc(Item.Fertilizer)) } },
            {Item.Humidifier,   new Price() {value =  (int)( (float)60 * scaling), currency=Resource.wood, condition = Fand(PlayerHasItemFunc(Item.Fertilizer), Fnot(PlayerHasItemFunc(Item.Humidifier))) } },
            {Item.GrowthRay,    new Price() {value =  (int)( (float)200 * scaling * scaling), currency=Resource.wood, condition = Fnot(PlayerHasItemFunc(Item.GrowthRay)) } },
        };
    }


    public void BuyItem(Item item)
    {
        var playerData = FindObjectOfType<PlayerDataObject>();
        if (playerData.TryRemoveResource(Items[item].currency, Items[item].value))
        {
            //Items.Remove(item);
            playerData.Items.Add(item);
        }
    }

    public void Reset(float scaling)
    {
        Items = InitialItems(scaling);
    }
}
