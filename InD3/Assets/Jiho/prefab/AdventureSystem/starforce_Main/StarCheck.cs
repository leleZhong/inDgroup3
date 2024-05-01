using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCheck : MonoBehaviour
{
    public Starforce Starforce_Main;
    public  GameObject UPPoint;

    private void Awake()
    {
        UPPoint = GameObject.Find("UPPoint");

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject== UPPoint.gameObject)
        {
            Starforce_Main.Is_SuccessCheck = gameObject;
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == UPPoint.gameObject)
        {
            Starforce_Main.Is_SuccessCheck = null;
        }
    }
}
