using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AdventureSystem_Manager : MonoBehaviour
{


    [SerializeField] List<Adventure_Game> GameList;

    public Adventure_Game curGame;


    //Play_Random_Game
    [SerializeField] GameObject Exclamation_mark;
    int Select_RandomGame;
    int Played_Count;
    public GameObject Start_Counting;
    [SerializeField] Sprite[] Starting_Image;


    void Start()
    {
       // Play_Random_Game();
    }

    // Update is called once per frame

    public void PerfectGameStart()
    {
        Adventure_Game newG = Instantiate(GameList[0], new Vector3(0, 0, -1.5f), Quaternion.identity);
        curGame = newG;
        StartCoroutine("GameStart_Counting", newG);
        if (curGame!=null)
        {
            curGame.GetComponent<Adventure_Game>().Play_MiniGame();
        }
    }

    //-------------------------------------------
/*    public void Play_Random_Game()
    {
        curGame = null;
        Played_Count++;
        if(Played_Count<=3)
        {
            StartCoroutine("Play_Random_Game_corutine");

        }
        else
        {
            GameResult_F();
        }

    }*/

    IEnumerator Play_Random_Game_corutine()
    {
      
        Select_RandomGame = Random.Range(3, 5);//<-Walking Time
        yield return new WaitForSeconds(Select_RandomGame);
        Exclamation_mark.SetActive(true);

        yield return new WaitForSeconds(0.8f);//Show !
        Exclamation_mark.SetActive(false);
        int num = Random.Range(0, GameList.Count);//<-Select_Game





        Adventure_Game newG = Instantiate(GameList[num],new Vector3(0,0,-1.5f),Quaternion.identity);
        curGame = newG;
        StartCoroutine("GameStart_Counting", newG);

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



}
