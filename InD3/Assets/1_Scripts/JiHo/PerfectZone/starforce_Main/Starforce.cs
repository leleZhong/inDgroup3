﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Starforce : Adventure_Game
{
    public AudioSource Game_BGM;
    public AudioSource Game_Effect;
    public List<AudioClip> Effect_Sound;

    [SerializeField] GameObject UPPoint;
    int UPPoint_H = 1;
    [SerializeField] float F_UPPoint_Speed;
    float UPPoint_Speed;
    [SerializeField] List<GameObject> StarCheck;

    public GameObject Is_SuccessCheck;
    [SerializeField] float Change_Is_SuccessCheck_sizeX;
    [SerializeField] float Change_Is_SuccessCheck_MinsizeX;


    [SerializeField] GameObject Success;
    [SerializeField] GameObject Fail;
    [SerializeField] Vector2 Succes_Offset;

    [SerializeField] int chance_Num;
    [SerializeField] int Succes_num;

    //Timer_Slider
    [SerializeField] int MaxGame_Time;
    [SerializeField] int Plus_SuccesTime;
    [SerializeField] Slider Timer_Slider;

    // Coin_Point
    int Coin;
    [SerializeField] TextMeshProUGUI Coin_Point;
    int Combo_Count;
    [SerializeField] TextMeshProUGUI Combo_Text;
    int MaxCombo_Count;
    [SerializeField] TextMeshProUGUI MaxCombo_Count_Text;

    //Life
    [SerializeField] GameObject[] Life;

    //Result
    public GameObject Result_Panel;
    public TextMeshProUGUI Result;
    public TextMeshProUGUI Final_Result;

    public int gameResultCoin;
    private void Start()
    {
        SoundManager.instance.Add_Sound(Game_BGM, "BGM");
        SoundManager.instance.Add_Sound(Game_Effect, "Effect");


        //Timer_Slider
        Timer_Slider.maxValue = MaxGame_Time;
        Timer_Slider.value = MaxGame_Time;
    }

    private void Update()
    {
        GameTimer();

    }


    public void Init_Starforce()
    {
        AdventureSystem_Manager.intance.curGame = null;
        Timer_Slider.value = Timer_Slider.maxValue;
        UPPoint.transform.localPosition = new Vector3(-5.84f, 4.26f, 1.65799f);
        Succes_num = 0;
        Combo_Count = 0;
        MaxCombo_Count = 0;
        chance_Num = 3;
        Coin = 0;

        Coin_Point.text = "0";
        Combo_Text.text = "0";
        MaxCombo_Count_Text.text = "0";

        StarCheck[0].transform.localScale = new Vector3(1, 1, 1);
        for (int i=0; i<3; i++)
        {
            Life[i].SetActive(true);
        }

        Result_Panel.SetActive(false);
        Combo_Text.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public override void StartSetting()
    {
        UPPoint_Speed = F_UPPoint_Speed;
        UPPoint.GetComponent<Rigidbody2D>().velocity = Vector2.right * UPPoint_Speed;
    }

    public override void Play_MiniGame()
    {
        if (Input.GetMouseButtonDown(0) && adventureSystem_Manager.Start_Counting.GetComponent<SpriteRenderer>().sprite == null && Result_Panel.activeSelf==false)
        {
            UPPoint.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            if (Is_SuccessCheck != null)//succes
            {
                //Succes Num
                Succes_num++;
                Combo_Count++;
                Coin += Succes_num;
                Coin_Point.text = Coin.ToString();
                Game_Effect.clip = Effect_Sound[0];
                Game_Effect.pitch += 0.02f* Combo_Count;
                Game_Effect.Play();
                //Combo
                Combo_Text.gameObject.SetActive(true);
                Combo_Text.transform.position = UPPoint.transform.position;
                Timer_Slider.value += Plus_SuccesTime;
                Combo_Text.GetComponent<Animator>().Play("Combo");

                //Max_Combo
                if(MaxCombo_Count<= Combo_Count)
                {
                    MaxCombo_Count = Combo_Count;
                    
                    MaxCombo_Count_Text.text = MaxCombo_Count.ToString();
                }

                if (Timer_Slider.value >= Timer_Slider.maxValue)
                {
                    Timer_Slider.value = Timer_Slider.maxValue;
                }

                if (Is_SuccessCheck.transform.localScale.x> Change_Is_SuccessCheck_MinsizeX)//change_SuccessCheck__Size
                {
                    Is_SuccessCheck.transform.localScale = new Vector2(Is_SuccessCheck.transform.localScale.x - Change_Is_SuccessCheck_sizeX, Is_SuccessCheck.transform.localScale.y);
                    if(Is_SuccessCheck.transform.localScale.x < Change_Is_SuccessCheck_MinsizeX)
                    {
                        Is_SuccessCheck.transform.localScale = new Vector2(Change_Is_SuccessCheck_MinsizeX, Is_SuccessCheck.transform.localScale.y);

                    }
                }
                else//change_SuccessCheck_Position
                {
                    //-5,7
                    int numm = Random.Range(-4, 6);
                    Is_SuccessCheck.transform.localPosition = new Vector3(numm, Is_SuccessCheck.transform.localPosition.y, Is_SuccessCheck.transform.localPosition.z);
                }

                Invoke("Set_Speed", 0.5f);
            }
            else//fail
            {
                Game_Effect.pitch = 0.4f;
                Combo_Count = 0;
                chance_Num--;
                Game_Effect.clip = Effect_Sound[1];
                Game_Effect.Play();
                for (int i=0; i<Life.Length; i++)
                {
                    if(Life[i].activeSelf==true)
                    {
                        Life[i].SetActive(false);
                        break;
                    }

                  
                }

                GameObject res = Instantiate(Fail, (Vector2)UPPoint.transform.position + Succes_Offset, Quaternion.identity);
                res.transform.parent = transform;
                Invoke("Set_Speed", 0.5f);
                Destroy(res, 1);
            }

            Combo_Text.text = Combo_Count.ToString();

            if (chance_Num == 0)
            {
                if (Result_Panel.activeSelf == false)
                {
                    Game_Result();
                }
                Game_Effect.clip = Effect_Sound[2];
                Game_Effect.Play();
                //Destroy(gameObject);
            }



        }
    }

    void Set_Speed()
    {

        UPPoint.GetComponent<Rigidbody2D>().velocity = Vector2.right * UPPoint_Speed* UPPoint_H;


    }




    //-------------Timer_Slider
    void GameTimer()
    {
        if(adventureSystem_Manager.Start_Counting.GetComponent<SpriteRenderer>().sprite==null)
        {
            Timer_Slider.value -= Time.deltaTime;

        }

        if(Timer_Slider.value<=0)

        {   if(Result_Panel.activeSelf==false)
            {
                Game_Result();
            }
           
            // Destroy(gameObject);
        }
    }

    public void Game_Result()
    {
        //Coin = MaxCombo_Count * score
        Result_Panel.SetActive(true);
        Result.text = Coin.ToString()+"\n"+  MaxCombo_Count.ToString();
        gameResultCoin = Coin + MaxCombo_Count;
        Final_Result.text = gameResultCoin.ToString();
        GameManager._instance.uiManager.perfectResultCoin = gameResultCoin;
    }

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == UPPoint.gameObject)
        {
            UPPoint_H *= -1;
            UPPoint.GetComponent<Rigidbody2D>().velocity = UPPoint.GetComponent<Rigidbody2D>().velocity * -1;
        }
    }
}
