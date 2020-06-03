using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BaseObject : NetworkBehaviour
{
    [SyncVar]
    public bool EffectedByExplosion;

    [SyncVar]
    public int indexCounter;

    [SyncVar]
    public int StressAmount;

    public GameObject ItemObject;
    GameObject SpawnedItemObject;
    BaseItem ItemClass;

    int playIndex;

    Vector3[] VectorDirection = new Vector3[4];

    // Start is called before the first frame update
    void Start()
    {
        VectorDirection[0] = Vector3.up;
        VectorDirection[1] = -(Vector3.up);
        VectorDirection[2] = Vector3.left;
        VectorDirection[3] = -(Vector3.left);

        //Spawnfrectures();
        indexCounter = 0;
        playIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (EffectedByExplosion == true && indexCounter >= StressAmount)
        {
            if (playIndex <= 0)
            {
                int CaniSpawn = Random.Range(0, 2);

                Spawnfrectures();
                Spawnfrectures();
                playIndex++;

                if (CaniSpawn == 0)
                {
                    CmdSpawnItem();
                }
            }
        }
    }


    public void Spawnfrectures()
    {
        int RandIndex = Random.Range(0, 4);

        GameObject[] gameObjects = MeshCut.Cut(gameObject, transform.position, VectorDirection[RandIndex], GetComponent<MeshRenderer>().material);

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = gameObjects[i].AddComponent<Rigidbody>();
                BoxCollider bc = gameObjects[i].AddComponent<BoxCollider>();

                rb.mass = 10;
                Destroy(gameObjects[i], 8f);
            }
        }
        GetComponent<Rigidbody>().mass = 10;
        BoxCollider CurrentBC = GetComponent<BoxCollider>();
        Destroy(CurrentBC);

        BoxCollider newwBC = gameObject.AddComponent<BoxCollider>();
        Destroy(gameObject, 8f);


    }

    [Command]
    void CmdSpawnItem()
    {
        SpawnedItemObject = Instantiate(ItemObject, transform);
        ItemClass = SpawnedItemObject.GetComponent<BaseItem>();
        ItemClass.TypeOfItem = Random.Range(0, 3);
        NetworkServer.Spawn(SpawnedItemObject);
    }
}
