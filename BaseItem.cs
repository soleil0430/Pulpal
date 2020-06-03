using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BaseItem : NetworkBehaviour
{
    [SyncVar]
    public int TypeOfItem;

    public Mesh[] BaseMesh;
    public Material[] BaseMaterial;

    BasePlayer PlayerClass;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        GetComponent<MeshFilter>().mesh = BaseMesh[TypeOfItem];
        GetComponent<MeshRenderer>().material = BaseMaterial[TypeOfItem];
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerClass = other.gameObject.GetComponent<BasePlayer>(); 

            switch (TypeOfItem)
            {
                case 0:
                    PlayerClass.SetHealth(1);
                    Destroy(gameObject);
                    break;

                case 1:
                    PlayerClass.CmdAddAttackPoint(300);
                    Destroy(gameObject);
                    break;

                case 2:
                    PlayerClass.AddMovementSpeed(1);
                    Destroy(gameObject);
                    break;
               

            }
        }
    }
}
