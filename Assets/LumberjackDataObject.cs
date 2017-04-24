using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackDataObject : Character
{

    public float BaseSpeed = 0.5f;
    public override float Speed
    {
        get
        {
            return 0.5f;
        }
    }

    public float BaseChopDelay = 2.0f;
    public float ChopDelay
    {
        get
        {
            return BaseChopDelay;
        }
    }

    public int BaseChopDMG = 1;
    public int ChopDMG
    {
        get
        {
            return BaseChopDMG;
        }
    }

    public enum LumberjackState
    {
        idle,
        goingToTree,
        chopping,
        goingBack,
    }

    public LumberjackState state = LumberjackState.idle;

    public GameObject targetTree;
    // Update is called once per frame

    public IEnumerator DelayedChop()
    {
        for (float i = 0; i < ChopDelay; i += 0.03f)
        {
            if (targetTree == null || targetTree.GetComponent<TreeDataObject>().Destroyed)
            {
                state = LumberjackState.idle;
                break;
            }
            yield return new WaitForSeconds(0.03f);
        }
        if (targetTree == null || targetTree.GetComponent<TreeDataObject>().Destroyed)
        {
            state = LumberjackState.idle;
        }
        else
        {
            targetTree.GetComponent<TreeDataObject>().Damage(ChopDMG);
            if (targetTree == null || targetTree.GetComponent<TreeDataObject>().Destroyed)
            {
                state = LumberjackState.idle;
            }
            else
            {
                yield return DelayedChop();
            }
        }
    }

    void Update()
    {
        switch (state)
        {
            case LumberjackState.idle:
                var trees = FindObjectsOfType<TreeDataObject>();
                var minDistance = 1000.0f;
                foreach (var tree in trees)
                {
                    if (tree.Destroyed || tree.Targeted) continue;
                    var treeObject = tree.gameObject.GetComponent<WorldObject>();
                    var distance = WorldObject.Distance(treeObject, worldObject);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        targetTree = tree.gameObject;
                    }
                }
                if(targetTree != null)
                {
                    state = LumberjackState.goingToTree;
                    targetTree.GetComponent<TreeDataObject>().Targeted = true;
                }
                break;
            case LumberjackState.goingToTree:
                if(targetTree == null || targetTree.GetComponent<TreeDataObject>().Destroyed )
                {
                    state = LumberjackState.idle;
                    break;
                }
                Move(((((targetTree.GetComponent<WorldObject>().Angle - worldObject.Angle + 360 + 180) % 360) - 180) < 0));
                if (WorldObject.AreClose(targetTree.GetComponent<WorldObject>(), worldObject))
                {
                    state = LumberjackState.chopping;
                    StartCoroutine(DelayedChop());
                }
                break;
            case LumberjackState.chopping:
                Move(Mathf.Sin(20 * Time.time) > 0);
                break;
            case LumberjackState.goingBack:
                break;
        }
    }
}
