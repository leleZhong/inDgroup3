using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DokeV_JIHO : MonoBehaviour
{
    public GameObject DokeV;
    public float Speed;


    public GameObject Glass;
    public Transform Glass_Pos;

    //Ball


    //Glass
    GameObject curGlass;
    [SerializeField] Sprite In_Glass_Ani;
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

    public bool DoKev_Staute()
    {
        if (CurPlaying != Playing.tired)
        {
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
                break;

            case Playing.glass:
                CurPlaying = Playing.glass;
                push_CurTime = push_Time;
                ani.SetInteger("Glass", 1);
               
                
                break;

            case Playing.food:
                CurPlaying = Playing.food;
                ani.SetInteger("Food", 1);
                Invoke("Eated_Food", 5);
                //Inven _ Get _ Food
                break;

            case Playing.tired:
                CurPlaying = Playing.tired;
                break;
        }

        BeforePlaying = CurPlaying;

    }

    //----------------------------Glass-------------------------------------------
    public void Push_Glass()
    {
      
        Mouse_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(Mouse_Pos, Vector2.zero, 0);

        if (Input.GetMouseButtonDown(0))
        {
            if (hit!=false)
            {
                MouseClick_Pos = Mouse_Pos;
                print("���콺 ��ġ ������");
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
                print("������");
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
    void Eated_Food()
    {
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
            hit = Physics2D.Raycast(Mouse_Pos, Vector2.zero, 0);

            if (hit != false)
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

    void OffDummy()
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
        }
        else if (num == 1 && ani.GetInteger("BallPlaying") == 0)
        {
            //bad
            ani.SetInteger("BallPlaying", -1);
            Invoke("Playing_End", 3);

        }
    }

    void Playing_End()
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