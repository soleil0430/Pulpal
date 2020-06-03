using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicUISystem : MonoBehaviour
{
    public Text FirstPlayerlifeText;
    string LifeText;
    string Playerone;
    int FPLife;

    public Text SecondPlayerlifeText;
    string Playertwo;
    int SCLife;

    public Text FirstPlayerACPText;
    string ACPBase;
    int ACPoint;

    public Text SCPlayerACPText;
    int SCACPoint;

    string AttackBase;

    public GameObject FirstPlayerObject;
    BasePlayer FPBasePlayerClass;

    public GameObject SecondPlayerObject;
    BasePlayer SCBasePlayerClass;

    public GameObject GameMG;
    GameSetManager GameMGClass;

    public Text GameSetText;

    public Text AttackTextOne;
    int FirstPlayerAttackType;

    public Text AttackTextTwo;
    int SecondPlayerAttackType;

    public string[] Attack = new string[3]; 

    float DelayTimer;

    // Start is called before the first frame update
    void Start()
    {
        FPBasePlayerClass = FirstPlayerObject.GetComponent<BasePlayer>();
        SCBasePlayerClass = SecondPlayerObject.GetComponent<BasePlayer>();
        GameMGClass = GameMG.GetComponent<GameSetManager>();

        GameSetText.gameObject.SetActive(false);

        ACPBase = "ACP: ";
        Playerone = "1P Life: ";
        Playertwo = "2P Life: ";
        LifeText = "♥";
        AttackBase = "Attack: ";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMGClass.GameSet == true)
        {
            DelayTimer = DelayTimer + Time.deltaTime;
            GameSetText.gameObject.SetActive(true);
            if (DelayTimer > 5)
            {
                GameSetText.gameObject.SetActive(false);
                DelayTimer = 0;
            }

        }

        else
        {
            GameSetText.gameObject.SetActive(false);
        }
        /*

        FPLife = FPBasePlayerClass.Health;
        ACPoint = FPBasePlayerClass.AttackPoint;
        SCLife = SCBasePlayerClass.Health;
        SCACPoint = SCBasePlayerClass.AttackPoint;

        FirstPlayerAttackType = FPBasePlayerClass.TypeofBomb;
        SecondPlayerAttackType = SCBasePlayerClass.TypeofBomb;

        SetLife(FPLife, FirstPlayerlifeText, Playerone);
        SetLife(SCLife, SecondPlayerlifeText, Playertwo);


        string FPFinalACPText = ACPBase + ACPoint.ToString();
        FirstPlayerACPText.text = FPFinalACPText;

        string SCFinalACPText = ACPBase + SCACPoint.ToString();
        SCPlayerACPText.text = SCFinalACPText;


        string FPFinalAttackTypeText = AttackBase + Attack[FirstPlayerAttackType];
        AttackTextOne.text = FPFinalAttackTypeText;

        string SCFinalAttackTypeText = AttackBase + Attack[SecondPlayerAttackType];
        AttackTextTwo.text = SCFinalAttackTypeText;*/
    }

    void SetLife(int life, Text TargetText, string TargetPlayer)
    {
        string FinalLife = "";

        for (int i = 0; i < life; i++)
        {
            FinalLife = FinalLife + LifeText;
        }


        string FPFinalLifeText = TargetPlayer + FinalLife;

        TargetText.text = FPFinalLifeText;
    }
}
