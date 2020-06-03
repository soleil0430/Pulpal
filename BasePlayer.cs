using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class BasePlayer : NetworkBehaviour
{



    public string Upkey;
    public string DownKey;
    public string RightKey;
    public string Leftkey;
    public string AttackKey;
    public string AttackChangekey;

    public GameObject ref_BombPrefeb;
    GameObject Spawend_Bomb;
    BaseBomb BombClass;

    public GameObject GameMG;
    GameSetManager GameMGClass;


    GameObject SyncPA;
    SyncPlayerAction SyncClass;

    [SyncVar]
    public float MovementSpeed;

    [SyncVar]
    public int Health;

    [SyncVar]
    public float DelayTime;

    [SyncVar]
    public float SpawnDelay;

    [SyncVar]
    public float AttackPoint;

    [SyncVar]
    public int AttackCost =300;

    public int Myindex;

    [SyncVar]
    public int TypeofBomb;

    [SyncVar]
    int Direction;

    public Animator CurrentAnim;

    [SyncVar]
    public bool GameIsSet;

    [SyncVar]
    public bool GodMode;


    [SyncVar]
    public bool Death;

    [SyncVar]
    public float DeathDelay;

    [SyncVar]
    public float CostDelay;

    [SyncVar]
    public float GodModeDelay;
    // Start is called before the first frame update
    public override void OnStartLocalPlayer()
    {

    }

    void Start()
    {
        

        Health = 5;
        CurrentAnim = GetComponent<Animator>();
        //GameIsSet = true;
        GameMG = GameObject.Find("GameSetManager");
        GameMGClass = GameMG.GetComponent<GameSetManager>();
        GameMGClass.AllPlayerisSet++;
        Myindex = GameMGClass.AllPlayerisSet -1;
        //GameMGClass.Player1 = gameObject;
        GameIsSet = false;


        if (Myindex == 0)
        {
            GameMGClass.Player1 = gameObject;
        }

        else if (Myindex == 1)
        {
            GameMGClass.Player2 = gameObject;
        }

        SyncClass = GameObject.Find("SyncOBject").GetComponent<SyncPlayerAction>();

        Array.Resize(ref SyncClass.Player, GameMGClass.AllPlayerisSet);
        Array.Resize(ref SyncClass.PlayerClass, GameMGClass.AllPlayerisSet);
        Array.Resize(ref SyncClass.PlayersAPoints, GameMGClass.AllPlayerisSet);
        SyncClass.Player[GameMGClass.AllPlayerisSet -1] = gameObject;
        SyncClass.PlayerClass[GameMGClass.AllPlayerisSet-1] = gameObject.GetComponent<BasePlayer>();


    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (GameIsSet == false)
        {
            SpawnDelay = SpawnDelay + Time.deltaTime;
            CostDelay = CostDelay + Time.deltaTime;


            CurrentAnim.SetBool("Death", Death);

            if (GodMode == true)
            {
                GodModeDelay = GodModeDelay + Time.deltaTime;

                if (GodModeDelay > 3)
                {
                    GodModeDelay = 0;
                    GodMode = false;
                }
            }


            if (DeathDelay >= 5)
            {
                Death = false;
                DeathDelay = 0;
                GodMode = true;
            }

            if (Death == true)
            {
                DeathDelay = DeathDelay + Time.deltaTime;
                return;
            }

            if (CostDelay > 1)
            {
                CmdAddAttackPoint(30);
                CostDelay = 0;
            }

            if (Input.anyKey == false)
            {
                CurrentAnim.SetBool("Walking", false);
            }

            if (Input.GetKey(Upkey))
            {
                transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);
                CurrentAnim.SetBool("Walking", true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (Input.GetKey(DownKey))
            {
                transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);
                CurrentAnim.SetBool("Walking", true);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (Input.GetKey(RightKey))
            {
                transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);
                CurrentAnim.SetBool("Walking", true);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                Direction = 0;
            }

            if (Input.GetKey(Leftkey))
            {
                transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);
                CurrentAnim.SetBool("Walking", true);
                transform.rotation = Quaternion.Euler(0, -90, 0);
                Direction = 1;
            }

            if (Input.GetKey(AttackKey))
            {
                if (SpawnDelay >= DelayTime)
                {
                    if (AttackCost > AttackPoint)
                    {
                        return;
                    }

                    CmdSpawnBomb();
                    SpawnDelay = 0;
                }
            }

            if (Input.GetKey(AttackChangekey))
            {
                if (SpawnDelay >= DelayTime)
                {
                    CmdChangeAttack();
                }
            }

        }

    }

    [Command]
    void CmdSpawnBomb()
    {
        if (AttackPoint < AttackCost)
        {
            return;
        }
        //Debug.Log("Attack");
        CurrentAnim.SetBool("Walking", false);

        Spawend_Bomb = (GameObject)Instantiate(ref_BombPrefeb, transform);
        
        BombClass = Spawend_Bomb.GetComponent<BaseBomb>();
        BombClass.SetTypeOfBomb(TypeofBomb);
        BombClass.PlaneDirection = Direction;
        NetworkServer.Spawn(BombClass.gameObject);
        AttackPoint = AttackPoint - AttackCost;

    }

    [Command]
    void CmdChangeAttack()
    {
        if (TypeofBomb >= 1)
        {
            TypeofBomb = 0;
            AttackCost = 300;
            SpawnDelay = 0;
            return;
        }
        AttackCost = AttackCost + 200;
        TypeofBomb++;
        SpawnDelay = 0;
    }

    [Command]
    public void CmdAddAttackPoint(float value)
    {
        AttackPoint = AttackPoint + value;
    }

    public void SetHealth(int Amount)
    {
        if (!isServer)
            return;

        if (Health > 5 || GodMode == true)
        {
            return;
        }

        if (Death == true)
        {
            return;
        }

        Health = Health + Amount;
        Death = true;

        if (Health <= 0)
        {
            
            GameMGClass.CheckGameSet(true);
            
        }

    }




    public void AddMovementSpeed(int value)
    {
        if (MovementSpeed > 6)
        {
            return;
        }

        MovementSpeed = MovementSpeed + value;
    }


    public float GetAttackPoint()
    {
        return AttackPoint;
    }
}
