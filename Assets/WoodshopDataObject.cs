using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodshopDataObject : WorkPlace
{

    public int MaxOccupancy = 3;
    public int NumWorkers = 0;

    public WorldObject LumberjackPrefab;

    public override bool CanHire()
    {
        return NumWorkers < MaxOccupancy;
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.L))
        //{
            //Hire(new GameObject());
        //}
    }
   
    public override void Hire(GameObject commonCitizen)
    {
        if (!CanHire()) return;
        Destroy(commonCitizen);
        var newLumberjack = Instantiate(LumberjackPrefab, gameObject.transform.parent);
        newLumberjack.transform.localScale = Vector2.one;
        newLumberjack.GetComponent<WorldObject>().SetAngle(GetComponent<WorldObject>().Angle);
        NumWorkers++;
    }

    public void OnExpand()
    {
        StartCoroutine(DestroySequence());
    }

    public IEnumerator DestroySequence()
    {
        ContextVisible = false;
        for (int i = 0; i < AnimationTime; i++)
        {
            transform.localScale = (float)(AnimationTime - i) / AnimationTime * Vector2.one;
            yield return null;
        }
        Destroy(gameObject);
    }
}
