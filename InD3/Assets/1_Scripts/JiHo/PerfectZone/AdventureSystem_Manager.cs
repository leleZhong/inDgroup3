using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class AdventureSystem_Manager : MonoBehaviour
{
    public static AdventureSystem_Manager intance;
    [SerializeField] public List<Adventure_Game> GameList;

    public Adventure_Game curGame;


    //Play_Random_Game
    [SerializeField] GameObject Exclamation_mark;
    int Select_RandomGame;
    int Played_Count;
    public GameObject Start_Counting;
    [SerializeField] Sprite[] Starting_Image;

    //public Transform perfectGameGroup;

    void Start()
    {
        intance = this;
       // Play_Random_Game();
    }

    // Update is called once per frame

    private void Update()
    {
        if (curGame!=null)
        {
            curGame.GetComponent<Adventure_Game>().Play_MiniGame();
        }
    }

    public void PerfectGameStart()
    {
        GameManager._instance.uiManager.perfectGameStartBtn.SetActive(false);
        StartCoroutine(GameManager._instance.uiManager.GameCountDown());
        //Adventure_Game newG = Instantiate(GameList[0], perfectGameGroup);
        Invoke("PerfectZone", 4f);
    }

    void PerfectZone()
    {
         GameList[0].gameObject.SetActive(true);
         curGame = GameList[0];
      



        StartCoroutine("GameStart_Counting", GameList[0]);
    }


  

    //-----------------------------------------------------


    IEnumerator GameStart_Counting(Adventure_Game Game)
    {
        for(int i=0; i<3; i++)
        {
            Start_Counting.GetComponent<SpriteRenderer>().sprite = Starting_Image[i];
            yield return new WaitForSeconds(0.5f);
            
        }

        Start_Counting.GetComponent<SpriteRenderer>().sprite = null;
        Game.StartSetting();
    }
    //------------------------------------------------------------------


    public void Enable_Obj(GameObject g)
    {
        g.SetActive(false);
    }
}
