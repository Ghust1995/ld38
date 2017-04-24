using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    public abstract float Speed
    {
        get;
    }
    public WorldObject worldObject;
    void Start()
    {
        worldObject = GetComponent<WorldObject>();
    }

    protected void Move(bool clockwise)
    {
        int dir = clockwise ? -1 : 1;
        worldObject.Angle += dir * Speed / FindObjectOfType<World>().Radius;
    }
}
