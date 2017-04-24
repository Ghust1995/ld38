using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CommonCitizenState
{
     idle, 
     goingToWork,
}

public class CommonCitizenDataObject : Character
{

    public float BaseSpeed = 0.5f;

    public override float Speed
    {
        get
        {
            return BaseSpeed;
        }
    }

    public CommonCitizenState state = CommonCitizenState.idle;
    public GameObject targetWorkPlace;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case CommonCitizenState.idle:
                var workplaces = FindObjectsOfType<WorkPlace>();
                var minDistance = 1000.0f;
                foreach (var workplace in workplaces)
                {
                    if (!workplace.ContextVisible || !workplace.CanHire()) continue;
                    var workPlaceObject = workplace.gameObject.GetComponent<WorldObject>();
                    var distance = WorldObject.Distance(workPlaceObject, worldObject);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        targetWorkPlace = workplace.gameObject;
                    }
                }
                if (targetWorkPlace != null)
                {
                    state = CommonCitizenState.goingToWork;
                }
                break;
            case CommonCitizenState.goingToWork:
                if (targetWorkPlace == null || !targetWorkPlace.GetComponent<WorkPlace>().CanHire())
                {
                    state = CommonCitizenState.idle;
                    break;
                }
                Move(((((targetWorkPlace.GetComponent<WorldObject>().Angle - worldObject.Angle + 360 + 180) % 360) - 180) < 0));
                if (WorldObject.AreClose(targetWorkPlace.GetComponent<WorldObject>(), worldObject))
                {
                    targetWorkPlace.GetComponent<WorkPlace>().Hire(gameObject);
                }
                break;
        }
    }
}
