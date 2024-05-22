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


    //tired

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
     
        int a = Random.Range(0, 5);
        switch(a)
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

    //----------------------Dialog---------------------------------------------
    public void Dialog_Text_F(string t)
    {
        if(Dialog_TMP!=null)
        {
            Dialog_TMP.text = t;
            StartCoroutine("Distoty_Dialog");
        }
       
    }

    IEnumerator Distoty_Dialog()
    {
        Dialog_TMP.transform.parent.gameObject.SetActive(true);
        Color c = Dialog_TMP.GetComponent<TextMeshProUGUI>().color;
        c.a = 1;
        Dialog_TMP.GetComponent<TextMeshProUGUI>().color = c;

        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            c.a -= 0.1f;
            Dialog_TMP.GetComponent<TextMeshProUGUI>().color = c;
            if(c.a<=0)
            {
                break;
            }
        }
        Dialog_TMP.transform.parent.gameObject.SetActive(false);
    }

    public bool DoKev_Staute()//놀기를 눌렀을 때 피곤한지 확인
    {
        if (CurPlaying != Playing.tired)
        {
            //피곤하지않다면 놀수있다
            return true;
        }
        else
        {
            
            return false;
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
                Dialog_Text_F("도깨비가 더러워보인다");
                break;

            case Playing.glass:
                CurPlaying = Playing.glass;
                push_CurTime = push_Time;
                ani.SetInteger("Glass", 1);
                Dialog_Text_F("도깨비가 답답해 보인다"); 



                break;

            case Playing.food:
                CurPlaying = Playing.food;
                ani.SetInteger("Food", 1);
                Invoke("Eated_Food", 5);
                Dialog_Text_F("도깨비가 배불러보인다");

                //Inven _ Get _ Food
                break;

            case Playing.tired:
                CurPlaying = Playing.tired;
                ani.SetInteger("Tired", 1);
                Dialog_Text_F("도깨비가 지쳐서 놀기 싫어하는거같다");
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
                    break;
                }

            }
        }
    }

    //-------------------------------------------------Glass-------------------------------------------
    //-------------------------------------------------Food-------------------------------------------
    void Eated_Food()//코루틴으로 실행
    {

        //음식 하나 줄이기

        ani.SetInteger("Food",0);
        CurPlaying = Playing.none;
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

                    Invoke("OffDummy", 2);
                }

            }
        }

    }

    void OffDummy()//코루틴 실행
    {
        Clean_Dummy.SetActive(false);
        CurPlaying = Playing.none;
    }
    //-------------------------------------------wash--------------------------------------------
    //-------------------------------------------Ball--------------------------------------------
   
    public void Ball_Playing()
    {
        int num = Random.Range(0, 2);

        if(num==0 && ani.GetInteger("BallPlaying")==0)
        {
            //Good
            ani.SetInteger("BallPlaying", 1);
            Invoke("Playing_End", 3);
            Dialog_Text_F("도깨비는 공놀이가 재미있다");
        }
        else if (num == 1 && ani.GetInteger("BallPlaying") == 0)
        {
            //bad
            ani.SetInteger("BallPlaying", -1);
            Invoke("Playing_End", 3);
            Dialog_Text_F("도깨비는 공놀이를 싫어하는것같다");

        }
    }

    void Playing_End()//Invoke 실행
    {
        ani.SetInteger("BallPlaying", 0);
        CurPlaying = Playing.none;
    }

    //-------------------------------------------Ball--------------------------------------------
    //-------------------------------------------Tired--------------------------------------------

    IEnumerator DoKeV_Tired()
    {
        
        yield return new WaitForSeconds(10f);
        CurPlaying = Playing.none;
    }
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
