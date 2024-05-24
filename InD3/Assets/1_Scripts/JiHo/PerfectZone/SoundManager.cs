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

    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;

    private void Start()
    {
        BGM_List[0].playOnAwake = true; // 시작시 배경음 플레이 추가 완료
        BGM_List[0].loop = true;
        SoundEffect_List[0].playOnAwake = false; 
        SoundEffect_List[0].loop = false;

        instance = this;
        
        BGM_Slider.value = BGM_Slider.maxValue / 2;
        Effect_Slider.value = Effect_Slider.maxValue;
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

    
    // ========================================================

    public void ChangeBGM(int sceneNum) // 0: Room 1: Yard 2: 미니게임선택배경음 3: LR게임배경음
    {
        if (BGM_List[0].clip != bgmClips[sceneNum])
        {
            BGM_List[0].clip = bgmClips[sceneNum];
        }
        
        BGM_List[0].Play();
    }

    public void ChangeAndPlaySfx(int sfxNum) // 0: 버튼클릭음 1: 성장효과음 2: 미니게임선택음 3: 상점구매음 4: 미니게임성공 5: 미니게임실패음
    {
        if (SoundEffect_List[0].clip != sfxClips[sfxNum])
        {
            SoundEffect_List[0].clip = sfxClips[sfxNum];
        }

        SoundEffect_List[0].Play();
    }
}
