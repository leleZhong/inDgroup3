using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public class TimerManager : MonoBehaviour
{
    public DateTime _dataTime;
    public string url = "";

    [Obsolete]
    void Start()
    {
        StartCoroutine(WebChk());
    }

    [Obsolete]
    IEnumerator WebChk()
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string date = request.GetResponseHeader("date"); //이곳에서 반송된 데이터에 시간 데이터가 존재
                Debug.Log("받아온 시간" + date);// GMT로 받아온다.
                DateTime dateTime = DateTime.Parse(date).ToLocalTime(); // ToLocalTime() 메소드로 한국시간으로 변환시켜 준다.
                _dataTime = dateTime;
                Debug.Log("한국시간으로변환" + dateTime);
            }
        }
    }
}