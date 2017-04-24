using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    [SerializeField]
    private float speed;

    // Use this for initialization
    void Start()
    {
        worldObject = GetComponent<WorldObject>();
    }

    public KeyCode clockwise;
    public KeyCode counterclockwise;

    public override float Speed
    {
        get
        {
            return ((PlayerDataObject)(worldObject.Context)).Speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Move(true);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Move(false);
        }
    }

    public UIContextData GetNearestContext()
    {
        UIContextData val = null;
        var minDistance = 9999999;
        var worldRadius = FindObjectOfType<World>().Radius;
        foreach (var wobj in FindObjectsOfType<WorldObject>())
        {
            if (wobj.Context == null) continue;
            if (wobj.Context.type == UIContextType.player) continue;
            var distance = WorldObject.Distance(wobj, worldObject);
            if (WorldObject.AreClose(wobj, worldObject))
            {
                if (distance < minDistance)
                {
                    distance = minDistance;
                    val = wobj.Context;
                }
            }
        }
        return val;
    }

    

}
