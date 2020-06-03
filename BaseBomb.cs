using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BaseBomb : NetworkBehaviour
{
    [SyncVar]
    int TypeOfBomb;

    [SyncVar]
    public int PlaneDirection;

    public GameObject ref_BombPrefeb;
    GameObject Spawend_Bomb;
    BombPhysic BombClass;

    public GameObject FireEffect;
    GameObject Spawned_FE;

    public GameObject BurnEffect;
    GameObject Spawned_BurnEffect;

    AudioSource audio;

    public GameObject Plane;
    GameObject SPawend_Plane;
    Jet jetClass;

    public AudioClip ExlosiveSound;

    Transform PhysicsSpawnLocation;

    //About Appearlence;

    public float ChangeSpeed;
    Color TargetSprite;
    bool ChangeAppearlence;

    public float BombDelay;

    float Timer;
    float Timer2;
    // Start is called before the first frame update


    float Somethings = 1.f;
    void Start()
    {
        transform.parent = null;

        audio = GetComponent<AudioSource>();
        audio.clip = ExlosiveSound;

        transform.rotation = Quaternion.Euler(90,0,0);
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);

        TargetSprite = GetComponent<SpriteRenderer>().color;
        ChangeAppearlence = false;
        //PhysicsSpawnLocation = GetComponent<Transform>();

        if (TypeOfBomb == 0)
        {
            BombDelay = 1.0f;
            Destroy(gameObject, 8.0f);
        }

        else if (TypeOfBomb == 1)
        {
            BombDelay = 1.5f;
            Destroy(gameObject, 5.0f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Timer = Timer + Time.deltaTime;

        FlickeringBombMark();

        if (Timer >= BombDelay)
        {
            if (TypeOfBomb == 0)
            {
                CmdSpawnBomb();
            }


            else if(TypeOfBomb == 1)
            {
                audio.Play();

                if (PlaneDirection == 0)
                {
                    CmdSpawnBombT(-1);
                }

                else if (PlaneDirection == 1)
                {
                    CmdSpawnBombT(1);
                }


                Timer = 0;
            }
            
        }


    }

    public void SetTypeOfBomb(int Type)
    {
        TypeOfBomb = Type;
    }

    void FlickeringBombMark()
    {
        TargetSprite = GetComponent<SpriteRenderer>().color;
        //Debug.Log(TargetSprite);
        if (ChangeAppearlence == true)
        {
            TargetSprite.a = TargetSprite.a + ChangeSpeed;
            GetComponent<SpriteRenderer>().color = TargetSprite;
            if (TargetSprite.a > 1)
            {
                ChangeAppearlence = false;
            }
        }

        else if (ChangeAppearlence == false)
        {
            TargetSprite.a = TargetSprite.a - ChangeSpeed;
            GetComponent<SpriteRenderer>().color = TargetSprite;
            if (TargetSprite.a < 0)
            {
                ChangeAppearlence = true;
            }
        }
    }

    [Command]
    void CmdSpawnBomb()
    {
        float DistanceFromBomb = Random.Range(-3, 3);

        Spawend_Bomb = Instantiate(ref_BombPrefeb, GetComponent<Transform>());
        Spawend_Bomb.transform.position = new Vector3(Spawend_Bomb.transform.position.x + DistanceFromBomb, Spawend_Bomb.transform.position.y, Spawend_Bomb.transform.position.z + DistanceFromBomb);
        BombClass = Spawend_Bomb.GetComponent<BombPhysic>();
        Destroy(Spawend_Bomb, 0.2f);
        NetworkServer.Spawn(Spawend_Bomb);

        Spawned_FE = Instantiate(FireEffect, GetComponent<Transform>());
        Spawned_FE.transform.position = new Vector3(Spawned_FE.transform.position.x + DistanceFromBomb, Spawned_FE.transform.position.y, Spawned_FE.transform.position.z + DistanceFromBomb);
        Destroy(Spawned_FE, 1f);
        NetworkServer.Spawn(Spawned_FE);

        float BurnAngle = Random.Range(0, 180);
        Spawned_BurnEffect = Instantiate(BurnEffect, GetComponent<Transform>());
        Spawned_BurnEffect.transform.parent = null;
        Spawned_BurnEffect.transform.position = new Vector3(Spawned_BurnEffect.transform.position.x + DistanceFromBomb, 0.1f, Spawned_BurnEffect.transform.position.z + DistanceFromBomb);
        Spawned_BurnEffect.transform.rotation = Quaternion.Euler(90, BurnAngle, 0);
        NetworkServer.Spawn(Spawned_BurnEffect);
        Destroy(Spawned_BurnEffect, 5f);
        audio.Play();
        Timer = 0;
    }


    [Command]
    void CmdSpawnBombT(int index)
    {
        SPawend_Plane = Instantiate(Plane, GetComponent<Transform>());
        SPawend_Plane.transform.position = new Vector3(index * 30, 5.6f, transform.position.z);
        SPawend_Plane.transform.parent = null;
        jetClass = SPawend_Plane.GetComponent<Jet>();
        jetClass.Direction = PlaneDirection;
        Destroy(SPawend_Plane, 3.2f);
        NetworkServer.Spawn(jetClass.gameObject);
        for (int i = 0; i < 20; i++)
        {
            Spawend_Bomb = Instantiate(ref_BombPrefeb, GetComponent<Transform>());
            Spawend_Bomb.transform.parent = null;
            Spawend_Bomb.transform.position = new Vector3(Spawend_Bomb.transform.position.x + index*i, Spawend_Bomb.transform.position.y, Spawend_Bomb.transform.position.z);
            BombClass = Spawend_Bomb.GetComponent<BombPhysic>();
            Destroy(Spawend_Bomb, 0.2f);
            NetworkServer.Spawn(Spawend_Bomb);

            Spawned_FE = Instantiate(FireEffect, GetComponent<Transform>());
            Spawned_FE.transform.parent = null;
            Spawned_FE.transform.position = new Vector3(Spawned_FE.transform.position.x + index*i, Spawned_FE.transform.position.y, Spawned_FE.transform.position.z);
            Destroy(Spawned_FE, 1f);
            NetworkServer.Spawn(Spawned_FE);

            float BurnAngle = Random.Range(0, 180);
            Spawned_BurnEffect = Instantiate(BurnEffect, GetComponent<Transform>());
            Spawned_BurnEffect.transform.parent = null;
            Spawned_BurnEffect.transform.position = new Vector3(Spawned_BurnEffect.transform.position.x + index*i, 0.1f, Spawned_BurnEffect.transform.position.z);
            Spawned_BurnEffect.transform.rotation = Quaternion.Euler(90, BurnAngle, 0);
            Destroy(Spawned_BurnEffect, 5f);
            NetworkServer.Spawn(Spawned_BurnEffect);

        }


        Destroy(gameObject);
    }
}
