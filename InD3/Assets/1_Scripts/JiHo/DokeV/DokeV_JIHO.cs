using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DokeV_JIHO : MonoBehaviour
{
    public static DokeV_JIHO instance;

    public GameObject DokeV;
    // Dialog
    public TextMeshProUGUI Dialog_TMP;


    public GameObject Glass;
    public Transform Glass_Pos;


    //Ball


    //Glass
    GameObject curGlass;
    [SerializeField] float push_Time;
    float push_CurTime;
    RaycastHit2D hit;
    Ray ray;
    Vector2 Mouse_Pos;
    Vector2 MouseClick_Pos=Vector2.zero;

    //animator
    public Animator ani;
    [SerializeField] AnimationClip Idle_Ani;


    //wash
    [SerializeField] GameObject dirty_Dummy;
    [SerializeField] GameObject Clean_Dummy;


    //ball
    [SerializeField] GameObject ball_Obj;
     GameObject curBall_Obj;

    //audio
    public AudioSource dokeVSound;
    public AudioClip [] dokeVEffect;
    public enum Playing
    {
        none,
        ball,
        wash,
        glass,
        food,
        tired
    }

   public Playing CurPlaying;
   public Playing BeforePlaying;

    private void Awake()
    {
        instance = this;
        SoundManager.instance.Add_Sound(dokeVSound, "Effect");
    }

    private void Start()
    {
        CurPlaying = Playing.none;
    }
   
    void Update()
    {
      
        switch (CurPlaying)
        {
            case Playing.ball:
                Ball_Playing();
                break;

            case Playing.wash:
                Wash_DokeV();
                break;

            case Playing.glass:
                Push_Glass();
                break;

            case Playing.none:

                ani.SetInteger("BallPlaying", 0);
                ani.SetInteger("Glass", 0);

                ani.Play(Idle_Ani.name);
                //DoKeV_Event();
                
                break;
        }

       
       if(CurPlaying!= BeforePlaying)
        {
          
            DoKeV_Event();
        }
        
    }

    //Select Random_Event
    public void Random_Event()
    {
         if(CurPlaying==Playing.none)
        {
            int a = Random.Range(0, 5);
            switch (a)
            {
                case 0:
                    CurPlaying = Playing.ball;
                    break;
                case 1:
                    CurPlaying = Playing.food;
                    break;
                case 2:
                    CurPlaying = Playing.glass;
                    break;
                case 3:
                    CurPlaying = Playing.wash;
                    break;
                case 4:
                    CurPlaying = Playing.none;
                    break;
            }
        }
       
    }


    public void DoKeV_Event()
    {
        switch (CurPlaying)
        {
            case Playing.ball:
                CurPlaying = Playing.ball;
                
                break;

            case Playing.wash:
                CurPlaying = Playing.wash;

                Color c = new Color(1, 1, 1, 1);
                dirty_Dummy.SetActive(true);
                dirty_Dummy.gameObject.GetComponent<SpriteRenderer>().color = c;
               
                StartCoroutine(GameManager._instance.uiManager.GameInfoMessage("도깨비가 더러워보인다 터치해서 깨끗하게 닦아주자"));
                break;

            case Playing.glass:
                CurPlaying = Playing.glass;
                push_CurTime = push_Time;
                ani.SetInteger("Glass", 1);
                
                StartCoroutine(GameManager._instance.uiManager.GameInfoMessage("도깨비가 답답해 보인다 유리병을 때겨 빼주자"));


                break;

            case Playing.food:
                CurPlaying = Playing.food;
                ani.SetInteger("Food", 1);
                Invoke("Eated_Food", 5);
                
                StartCoroutine(GameManager._instance.uiManager.GameInfoMessage("도깨비가 뭘 주워먹은 듯하다 배가불러보인다"));

                //Inven _ Get _ Food
                break;

           
        }

        BeforePlaying = CurPlaying;

    }

    //----------------------------Glass-------------------------------------------
    public void Push_Glass()
    {
      
        Mouse_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       // hit = Physics2D.Raycast(Mouse_Pos, Vector2.zero, 0);

        if (Input.GetMouseButtonDown(0))
        {
            //if (hit!=false)
            {
                MouseClick_Pos = Mouse_Pos;
                print("마우스 위치 저장함");
            }

        }
        else if (Input.GetMouseButton(0))
        {
            if (MouseClick_Pos != Vector2.zero)
            {
                if (Mathf.Abs(Vector2.Distance(MouseClick_Pos, Mouse_Pos)) > 2)
                {
                    ani.SetInteger("Glass", 2);
                    push_CurTime -= Time.deltaTime;
                    if(push_CurTime<=0)
                    {
                        ani.SetInteger("Glass", 3);
                        if(curGlass==null)
                        {
                            StartCoroutine("Move_Glass");
                        }
                    }
                }
                print("빼는중");
            }


        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (push_CurTime>0)
            {
                ani.SetInteger("Glass", 1);
            }
            MouseClick_Pos = Vector2.zero;
        }
    }

    IEnumerator Move_Glass()
    {
        if (curGlass == null)
        {
            curGlass = Instantiate(Glass, Glass_Pos.position, Quaternion.identity);
        }

        if (curGlass != null)
        {


            while (curGlass != null)
            {
                yield return null;
                curGlass.transform.position = Vector2.Lerp(curGlass.transform.position, Glass_Pos.position + new Vector3(3, 0, 0), 0.1f);
                if (curGlass.transform.position.x >= Glass_Pos.position.x + 2.5f)
                {
                    Destroy(curGlass);
                    CurPlaying = Playing.none;
                    dokeVSound.clip=dokeVEffect[1];
                    dokeVSound.Play();
                    GameManager._instance.LoveAmountUp();
                    break;
                }

            }
        }
    }

    //-------------------------------------------------Glass-------------------------------------------
    //-------------------------------------------------Food-------------------------------------------
    void Eated_Food()//코루틴으로 실행
    {
        if (CurPlaying != Playing.none)
        {
            GameManager._instance.FeedAmountUp();

            ani.SetInteger("Food", 0);
            CurPlaying = Playing.none;
        }
           
    }

    //-------------------------------------------------Food-------------------------------------------
    //-------------------------------------------------wash---------------------------------------------------
    void Wash_DokeV()
    {

       

        if (Input.GetMouseButtonDown(0))
        {
            Mouse_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //hit = Physics2D.Raycast(Mouse_Pos, Vector2.zero, 0);

            //if (hit != false)
            {

                Color c = dirty_Dummy.GetComponent<SpriteRenderer>().color;
                c.a -= 0.2f;
                dirty_Dummy.GetComponent<SpriteRenderer>().color = c;


                if (c.a<=0)
                {
                    dirty_Dummy.SetActive(false);
                    Clean_Dummy.SetActive(true);

                    dokeVSound.clip = dokeVEffect[0];
                    dokeVSound.pitch = 1;
                    dokeVSound.Play();
                    Invoke("OffDummy", 2);
                }

            }
        }

    }

    void OffDummy()//코루틴 실행
    {
        if(CurPlaying!= Playing.none)
        {
            Clean_Dummy.SetActive(false);
            GameManager._instance.LoveAmountUp();
            CurPlaying = Playing.none;
        }

    }
    //-------------------------------------------wash--------------------------------------------
    //-------------------------------------------Ball--------------------------------------------
   
    public void Ball_Playing()
    {
        //공 생성
        if (curBall_Obj == null && ani.GetInteger("BallPlaying") == 0)
        {
            curBall_Obj = Instantiate(ball_Obj, transform.position + new Vector3(1, -1, 0), Quaternion.identity);
            StartCoroutine(GameManager._instance.uiManager.GameInfoMessage("도깨비와 공놀이를 할 수 있다"));
        }

        if (Input.GetMouseButtonDown(0))
        {
            int num = Random.Range(0, 2);

            if (num == 0 && ani.GetInteger("BallPlaying") == 0)
            {
                if (curBall_Obj != null)
                {
                    Destroy(curBall_Obj);
                    //Good
                    ani.SetInteger("BallPlaying", 1);
                    Invoke("Playing_End", 3);

                    StartCoroutine(GameManager._instance.uiManager.GameInfoMessage("재미있게 놀고있는것 같다"));
                    GameManager._instance.LoveAmountUp();
                }


            }
            else if (num == 1 && ani.GetInteger("BallPlaying") == 0)
            {
                if (curBall_Obj != null)
                {
                    Destroy(curBall_Obj);
                    //bad
                    ani.SetInteger("BallPlaying", -1);
                    Invoke("Playing_End", 3);
                    StartCoroutine(GameManager._instance.uiManager.GameInfoMessage("하기 싫어하는것 같다"));
                }
            }
        }
       
    }

    void Playing_End()//Invoke 실행
    {
        
        ani.SetInteger("BallPlaying", 0);
        CurPlaying = Playing.none; 
        
    }

    //-------------------------------------------Ball--------------------------------------------



    //-------------------------------------------Tired--------------------------------------------

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.name.Contains("Ball"))
        {
            Destroy(collision.gameObject);
           
            CurPlaying = Playing.none;
        }
    }


}
