using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEnlarger : MonoBehaviour {

    public float animationTime;
    public Vector2 baseScale;
	// Use this for initialization
	void Start () {
        baseScale = transform.localScale;
		
	}
	
	// Update is called once per frame
	void Update () {
	    //if(Input.GetKeyDown(KeyCode.O))
        //{
            //StartCoroutine(EnlargeWorld());
        //} 
	}

    public void GrowWorld()
    {
        FindObjectOfType<PlayerDataObject>().Items = new HashSet<Item>();
        FindObjectOfType<PlayerDataObject>().Resources = new Dictionary<Resource, long>();
        FindObjectOfType<StoreDataObject>().Reset(FindObjectOfType<World>().Radius);

        foreach(var house in FindObjectsOfType<CommonHouseDataObject>())
        {
            house.OnExpand();
        }

        foreach (var woodshop in FindObjectsOfType<WoodshopDataObject>())
        {
            woodshop.OnExpand();
        }

        foreach (var citizen in FindObjectsOfType<CommonCitizenDataObject>())
        {
            Destroy(citizen.gameObject);
        }
        foreach (var lumberjack in FindObjectsOfType<LumberjackDataObject>())
        {
            Destroy(lumberjack.gameObject);
        }
        StartCoroutine(EnlargeWorld());
        FindObjectOfType<StoreDataObject>().Reset(FindObjectOfType<World>().radiusMultiplier);
    }

    private IEnumerator EnlargeWorld()
    {
        Debug.Log("enlarging");
        var world = FindObjectOfType<World>();
        var nextRadius = world.radiusMultiplier * 1.2f;
        var initialRadius = world.radiusMultiplier;

        Func<float, float> tween = (float k) =>
        {
            if (k == 0) return 0;
            if (k == 1) return 1;
            return Mathf.Pow(2f, -10f * k) * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f) + 1f;
        };

        for(float t = 0.0f; t < animationTime; t += 0.03f)
        {
            world.radiusMultiplier = initialRadius + tween(t) * (nextRadius - initialRadius) / animationTime;
            transform.localScale = baseScale / (initialRadius + t * (nextRadius - initialRadius) / animationTime);
            
            yield return new WaitForSeconds(0.03f);
        }
        

        world.radiusMultiplier = nextRadius;
    }
        
}
