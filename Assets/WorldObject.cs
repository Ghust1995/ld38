using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WorldObject : MonoBehaviour
{
    public static float Distance(WorldObject obj1, WorldObject obj2)
    {
        float deltaAngle = Mathf.Abs(obj1.Angle - obj2.Angle);
        while (deltaAngle < 0)
        {
            deltaAngle += 360;
        }
        return obj1.world.Radius * deltaAngle * Mathf.PI / 180.0f;
    }

    public static bool AreClose(WorldObject obj1, WorldObject obj2)
    {

        return Distance(obj1, obj2) < obj1.minDistance / obj1.world.radiusMultiplier + obj2.minDistance / obj1.world.radiusMultiplier;
    }


    public bool BlocksSpawn;
    public UIContextData Context
    {
        get
        {
            return GetComponent<UIContextData>();
        }
    }

    public float minDistance
    {
        get
        {
            return container != null ? container.size.x : 0.0f;
        }
    }
    public BoxCollider2D container
    {
        get
        {
            return GetComponent<BoxCollider2D>();
        }
    }
    public float Angle;
    World world
    {
        get
        {
            return FindObjectOfType<World>();
        }
    }

    public SpriteRenderer sprite
    {
        get
        {
            return GetComponent<SpriteRenderer>();
        }
    }
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SetAngle(Angle);
    }

    public void SetAngle(float angle)
    {
        while(angle < 0)
        {
            angle += 360;
        }
        Angle = angle % 360;
        this.transform.position = (world.Radius) * (new Vector3(Mathf.Cos(Angle * Mathf.PI / 180), Mathf.Sin(Angle * Mathf.PI / 180)));
        this.transform.rotation = Quaternion.Euler(0, 0, -90.0f + Angle);
    }
}
