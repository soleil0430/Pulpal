using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetManager : MonoBehaviour
{

    public float CostTimer;

    public int CostAmount;

    public int HeliSpawnIndex;

    //public Transform HeliTransfrom;

    public GameObject SpawnHeil;
    GameObject SapwedHeil;
    BaseHeli HeilClass;

    public GameObject Player1;
    BasePlayer Player1Class;

    public GameObject Player2;
    BasePlayer Player2Class;

    public bool GameSet;

    public bool OutOfBox;

    public int AllPlayerisSet;


    public GameObject SpawnLoc1;
    public GameObject SpawnLoc2;

    float GameSetTimer;

    // Start is called before the first frame update
    void Start()
    {

        OutOfBox = false;
        GameSet = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (Player1 != null && Player2 != null)
        {
            Player1Class = Player1.GetComponent<BasePlayer>();
            Player2Class = Player2.GetComponent<BasePlayer>();
        }

        if (GameSet == true && AllPlayerisSet >= 124)
        {
            GameSetTimer = GameSetTimer + Time.deltaTime;
            SpawnHeli();

            if (GameSetTimer > 5)
            {
                Player1.transform.position = SpawnLoc1.transform.position;
                Player2.transform.position = SpawnLoc2.transform.position;
                Player1Class.Health = 5;
                Player2Class.Health = 5;
                Player1Class.AttackPoint = 300;
                Player2Class.AttackPoint = 300;
                Player1Class.Death = false;
                Player2Class.Death = false;
                Player1Class.MovementSpeed = 3;
                Player2Class.MovementSpeed = 3;
                Player1Class.CostDelay = 0;
                Player2Class.CostDelay = 0;
                Player1Class.GodMode = true;
                Player2Class.GodMode = true;
                Player1Class.GodModeDelay = 0;
                Player2Class.GodModeDelay = 0;

                GameSetTimer = 0;
                GameSet = false;
                Player1Class.GameIsSet = false;
                Player2Class.GameIsSet = false;
            }
        }


        if (GameSet == false)
        {
            if (Player1 != null || Player2 != null)
            {
                CostTimer = CostTimer + Time.deltaTime;
                GameObject[] Box;
                Box = GameObject.FindGameObjectsWithTag("ItemBox");

                if (Box.Length == 0)
                {
                    Debug.Log("Out of Box");
                    OutOfBox = true;

                    if (OutOfBox == true)
                    {

                        SpawnHeli();

                    }
                }

            }
        }
    }


    public void CheckGameSet(bool value)
    {
        GameSet = value;

        if (GameSet == true)
        {
            Player1Class.GameIsSet = true;
            Player2Class.GameIsSet = true;
        }
        
    }

    void SpawnHeli()
    {
        if (HeliSpawnIndex <= 0)
        {
            SapwedHeil = Instantiate(SpawnHeil, transform);
            HeilClass = SapwedHeil.GetComponent<BaseHeli>();
            HeilClass.GameMG = gameObject;
            HeilClass.transform.parent = null;
            HeilClass.transform.position = new Vector3(-1.02f, 120.43f, -2.681654f);
            HeilClass.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            HeilClass.transform.rotation = Quaternion.Euler(0, -90, 0);
            HeliSpawnIndex++;
        }
    }
}
