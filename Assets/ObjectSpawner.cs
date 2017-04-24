using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{


    public WorldObject tree;
    public WorldObject house;
    public WorldObject store;
    public WorldObject woodshop;

    public float BaseTreeSpawnInterval = 5.0f;
    public float FertilizerTreeSpawnInterval = 3.0f;
    public float HumidifierTreeSpawnInterval = 1.0f;
    public float TreeSpawnInterval
    {
        get
        {
            var playerItems = FindObjectOfType<PlayerDataObject>().Items;
            if (playerItems.Contains(Item.Humidifier))
            {
                return HumidifierTreeSpawnInterval;
            }
            if (playerItems.Contains(Item.Fertilizer)) {
                return FertilizerTreeSpawnInterval;
            }
            return BaseTreeSpawnInterval;
        }
    }
    public int MaxTrees = 10;
    public int TreeCount
    {
        get
        {
            return
                FindObjectsOfType<TreeDataObject>().Length;
        }
    }

    public void Start()
    {
        StartCoroutine(TrySpawnNewTreeAndWait());
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
            //StartCoroutine(SpawnNewTree());
        //}
        //if (Input.GetKeyDown(KeyCode.H))
        //{
            //StartCoroutine(SpawnNewHouse());
        //}
        //if (Input.GetKeyDown(KeyCode.W))
        //{
            //StartCoroutine(SpawnNewWoodshop());
        //}
    }


    public IEnumerator SpawnNewTree()
    {
        return SpawnNew(tree, Random.Range(0.0f, 360.0f));
    }

    public IEnumerator SpawnNewHouse()
    {
        return SpawnNew(house, Random.Range(0.0f, 360.0f));
    }

    public IEnumerator SpawnNewWoodshop()
    {
        return SpawnNew(woodshop, Random.Range(0.0f, 360.0f));
    }
    public IEnumerator TrySpawnNewTreeAndWait()
    {
        yield return new WaitForSeconds(TreeSpawnInterval);
        if (TreeCount <= MaxTrees)
        {
            yield return SpawnNewTree();
        }
        yield return TrySpawnNewTreeAndWait();
    }

    public IEnumerator SpawnNew(WorldObject wobj, float angle)
    {

        var allWobj = FindObjectsOfType<WorldObject>();
        var newWobj = Instantiate<WorldObject>(wobj, this.transform);
        newWobj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        newWobj.Context.ContextVisible = false;
        var color = newWobj.sprite.color;
        newWobj.sprite.color = new Color();


        bool validPosition = false;
        var initialAngle = angle;
        int loopCount = 0;
        while (!validPosition)
        {

            angle += 0.1f/FindObjectOfType<World>().radiusMultiplier;
            newWobj.SetAngle(angle);
            foreach (var oldwobj in allWobj)
            {
                validPosition = true;
                if (!oldwobj.BlocksSpawn) continue;
                if (oldwobj != null && WorldObject.AreClose(newWobj, oldwobj))

                {
                    validPosition = false;
                    break;
                }
            }
            if (angle >= initialAngle + 360)
            {
                Destroy(newWobj.gameObject);
                Debug.Log("Cant spawn new thing");
                break;
            }

            loopCount++;
            if (loopCount % 10 == 0)
            {
                yield return null;
            }
        }

        if (validPosition)
        {
            newWobj.sprite.color = color;
            for (int i = 0; i < newWobj.Context.AnimationTime; i++)
            {
                newWobj.transform.localScale = ((float)i / newWobj.Context.AnimationTime) * new Vector3(1.0f, 1.0f, 1.0f);
                yield return null;
            }

            newWobj.Context.ContextVisible = true;
            newWobj.Context.Initialize();
        }

    }
}
