using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonHouseDataObject : UIContextData {

    public WorldObject CommonCitizenPrefab;

    public override void Initialize()
    {
        var newCitizen = Instantiate(CommonCitizenPrefab, gameObject.transform.parent);
        newCitizen.transform.localScale = Vector2.one;
        newCitizen.GetComponent<WorldObject>().SetAngle(GetComponent<WorldObject>().Angle);
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
