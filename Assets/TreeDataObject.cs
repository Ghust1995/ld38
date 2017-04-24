using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDataObject : UIContextData {

    struct LifeWood
    {
        public int life;
        public int wood;
    }
    List<LifeWood> LifeWoodOptions = new List<LifeWood>()
    {
        new LifeWood() {life=10, wood=1 },
        new LifeWood() {life=10, wood=1 },
        new LifeWood() {life=10, wood=1 },
        new LifeWood() {life=20, wood=3 },
        new LifeWood() {life=20, wood=3 },
        new LifeWood() {life=30, wood=9 },
    };

    public int lifeWoodIndex;
    public int Life;
    public int BaseLife
    {
        get
        {
            return LifeWoodOptions[lifeWoodIndex].life;
        }
    }
    public int Wood
    {
        get
        {
            return LifeWoodOptions[lifeWoodIndex].wood;
        }
    }
    public bool Destroyed = false;
    public bool Targeted = false;

    Vector3 baseScale;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (Destroyed) return;
       
	}

    public void Damage(int damage)
    {
        if (Destroyed) return;
        Life -= damage;
        if (Life <= 0)
        {
            StartCoroutine(DestroySequence());
        }
    }

    public IEnumerator DestroySequence()
    {
        Destroyed = true;
        ContextVisible = false;
        for (int i = 0; i < AnimationTime; i ++)
        {
            transform.localScale = (float)(AnimationTime-i)/AnimationTime * Vector2.one;
            yield return null;
        }
        FindObjectOfType<PlayerDataObject>().AddResource(Resource.wood, Wood);
        Destroy(gameObject);
    }

    public override void Initialize()
    {
        lifeWoodIndex = Random.Range(0, LifeWoodOptions.Count);
        Life = BaseLife;
    }
}
