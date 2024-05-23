using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public List<AudioSource> BGM_List;
    public List<AudioSource> SoundEffect_List;

    public GameObject Setting_Panel;
    public Slider BGM_Slider;
    public Slider Effect_Slider;

    private void Start()
    {
        instance = this;
        BGM_Slider.value = BGM_Slider.maxValue / 2;
        Effect_Slider.value = Effect_Slider.maxValue / 2;
    }


    public void Add_Sound(AudioSource BGM,string type="")
    {           //        추가할 사운드   //사운드 타입 
        switch (type)
        {
            case "":
                break;

            case "BGM":
                BGM_List.Add(BGM);
                break;

            case "Effect":
                SoundEffect_List.Add(BGM);
                break;

        }
       
    }


    public void Change_BGMSound()
    {
        foreach(AudioSource a_s in BGM_List)
        {
            a_s.volume = BGM_Slider.value;
        }
    }

    public void Change_EffectSound()
    {
        foreach (AudioSource a_s in SoundEffect_List)
        {
            a_s.volume = Effect_Slider.value;
            
        }
    }

    public void OpenPanel(GameObject g)//판넬 열어줌
    {
        g.SetActive(true);
    }

    public void ClosePanel(GameObject g)//판넬 닫아줌
    {
        g.SetActive(false);
       
    }


   
}
