﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public UIManager uiManager;
    public AdventureSystem_Manager adventureSystemManager;
    public SoundManager soundManager;
    
    // 먹이 변수
    public int foodType = 100; // 음식 종류, 아무것도 선택하지 않았을 때의 값을 미리 선언
    public int[] foodCount = new int[4]; // 음식의 최대 개수와 각 음식의 개수 //0 1 
    
    // 장난감 변수
    public int toyType = 100; // 장난감 종류, 아무것도 선택하지 않았을 때의 값을 미리 선언
    public GameObject dkbToy; // 화면에 표시되는 장난감

    public int coin;

    //===============================================[ 시간 계산 ]=========================================
    // 시간 계산 변수
    [Header("----[ Time UI ]")]
    public TimerManager timerManager;
    private float _remainingTimeH; // 화면에 표시되는 쿨타임 시간
    private float _remainingTimeM; // 화면에 표시되는 남은 쿨타임 분
    private float _remainingTimeS; // 화면에 표시되는 남은 쿨타임 초
    public float coolH; // 쿨타임 설정 시간
    public float coolM; // 쿨타임 설정 분
    public float coolS; // 쿨타임 설정 초
    public float toyTimeCoolH;
    public float toyTimeCoolM;
    public float toyTimeCoolS;
    public float dkbTimeCoolH;
    public float dkbTimeCoolM;
    public float dkbTimeCoolS;

    //==============================================[ 케어 지수, 정도, UI ]========================================
    // 케어 수치 변수
    [Header("----[ Care Level ]")]
    public int stressLevel;  // 스트레스 지수
    public int playCount; // 놀이 가능 횟수
    public int maxPlayCount; // 최대 놀이 가능 횟수
    public int exp; // 경험치
    public int[] expRange; // 경험치 최소, 최대 범위

    [Header("----[ Care Amount ]")]
    public int feedAmount; // 포만도
    public int loveAmount; // 애정도
    public int minFnLAmount = 20; // 포만도, 애정도 최소 수치 

    // 현재 요구 진행 상태 변수
    //private bool _isRequest; //요구 실행 여부
    //private bool _isPlayTime;
    
    //==============================================[ 성장 ]===================================================
    // 기본 성장 변수
    public int growthLevel;   // 도깨비 성장 레벨
    public bool isGrowStart;  // 성장 시작 확인
    public bool isToyTime;  // 성장 시작 확인
    public bool isLvUp;
    // lv0 ~ lv1 성장
    public GameObject[] dkbObjs;         // lv0 ~ lv1 도깨비 게임 오브젝트
    public Animator[] dkbAnims;          // lv0 ~ lv1 도깨비 애니메이터 
    public SpriteRenderer[] dkbSpriteRs; // lv0 ~ lv1 도깨비 렌더러 
    public RuntimeAnimatorController[] lv0AnimCtrls; // lv0 애니메이터 컨트롤러
    public RuntimeAnimatorController[] lv1AnimCtrls; // lv1 애니메이터 컨트롤러
    public Sprite[] lv0Sprites; // lv0 도깨비들 스프라이트
    public Sprite[] lv1Sprites; // lv1 도깨비들 스프라이트
    // lv2 성장
    public GameObject collectionGroup; // 콜렉션 그룹
    public GameObject[] lv2Prefabs;   // lv2 도깨비들 프리펩
    
    //==============================================[ 미분류 ]===================================================

    
    
    //=========================================================================================================
    
    private void Awake()
    {
        _instance = this;
        
        // 게임 시작 모든 음식 개수 0으로 초기화
        for (int i = 0; i < foodCount.Length; i++)
        {
            foodCount[i] = 0;
        }
        
        // 튜토리얼 진행 보상
        foodCount[0] = 5; 

    }
    
    private void Update()
    {
        uiManager.loveFilledImage.fillAmount = (float)loveAmount / 100;
        uiManager.feedFilledImage.fillAmount = (float)feedAmount / 100;
        
        // 키우기가 시작됐을 때(도깨비 장난감을 놓은 순간) 쿨다운 시작
        if(!isToyTime || isLvUp || uiManager.isHelp)
            return;
        if (isGrowStart)
        {
            if(uiManager.isPopUp)
                return;
        }
        CareCoolDown();
    }

    // ==============================================================================================================

    // 케어 함수. 케어 쿨타임
    public void SetCareCoolTime()
    {
        // 장치에 현재 시, 분, 초에 설정 쿨타임을 더함
        PlayerPrefs.SetFloat("EndHour", timerManager._dataTime.Hour + coolH);
        PlayerPrefs.SetFloat("EndMinute", timerManager._dataTime.Minute + coolM);
        PlayerPrefs.SetFloat("EndSecond", timerManager._dataTime.Second + coolS);
        // 그리고 화면에 표시될 변수에 남은 시간(쿨타임) 저장
        _remainingTimeH = PlayerPrefs.GetFloat("EndHour") - timerManager._dataTime.Hour;
        _remainingTimeM = PlayerPrefs.GetFloat("EndMinute") - timerManager._dataTime.Minute;
        _remainingTimeS = PlayerPrefs.GetFloat("EndSecond") - timerManager._dataTime.Second;
    }

    // 케어 함수. 현재 시간 + n시간 n분 n초가 쿨타임 끝시간, 남은 시간 = 쿨타임 끝 시간 - 현재 시간
    void CareCoolDown()
    {
        // 쿨타임 감소 및 시:분:초 계산
        _remainingTimeS -= Time.deltaTime;
        uiManager.filledTimerImage.fillAmount = (_remainingTimeH * 3600 + _remainingTimeM * 60 + _remainingTimeS) /
                                                (coolH * 3600 + coolM * 60 + coolS);
        if (_remainingTimeS <= 0f && _remainingTimeM != 0f)
        {
            _remainingTimeM -= 1f;
            _remainingTimeS = 60f;
        }
        else if (_remainingTimeM <= 0f && _remainingTimeH != 0f)
        {
            _remainingTimeH -= 1f;
            _remainingTimeM = 60f;
        }
        // 쿨타임 감소 표출
        if (_remainingTimeM > 0)
        {
            uiManager.careCoolTime.text = string.Format("{0:D}",(int)_remainingTimeM);
        }
        else if(_remainingTimeM <= 0)
        {
            uiManager.careCoolTime.text = string.Format("{0:D}",(int)_remainingTimeS);
        }
        

        // 쿨타임이 끝났을 때 갖가지 초기화
        if (_remainingTimeS <= 0 && _remainingTimeM <= 0 && _remainingTimeH <= 0)
        {
            // 도깨비 장난감이 활성화 돼있을 때
            if (dkbToy.activeSelf)
            {
                WhatDokkaebi();    
                SetCareCoolTime();
                playCount = maxPlayCount;
                return;
            }
            ChangeStateByFoodAmount();
            SetCareCoolTime();
            playCount = maxPlayCount;
        }
    }

    // 무슨 도깨비인지 장난감 타입(toyType)에 따라 결정
    void WhatDokkaebi()
    {
        //uiManager.isSetToy = false;
        uiManager.isPopUp = false;
        isGrowStart = true;
        dkbObjs[0].SetActive(true);
        dkbToy.SetActive(false);
        uiManager.SetExpStateActive();
        uiManager.ChangeExpText();
        dkbSpriteRs[0].sprite = lv0Sprites[toyType];
        dkbAnims[0].runtimeAnimatorController = lv0AnimCtrls[toyType];
        coolH = dkbTimeCoolH;
        coolM = dkbTimeCoolM;
        coolS = dkbTimeCoolS;
    }
    
    // 포만도에 의한 스탯 변화 
    void ChangeStateByFoodAmount()
    {
        if (feedAmount <= minFnLAmount) // 스트레스 up
        {
            if(stressLevel >= 3)
            {
                print("도깨비 방생 애니메이션 등장");
                uiManager.releaseDkbPanel.SetActive(true);
                // 도깨비 있던 곳 비워지고 모든 값 초기화
                // 새로운 도깨비 데려오는 버튼 도깨비 불 있는 곳에 활성화
                CareRoomReset();
            }
            else
            {
                stressLevel++;
                //print("스트레스 : "+stressLevel);
            }
        }
        else if (feedAmount > minFnLAmount) // 경험치 up
        {
            DokkaebiGrowth();
            //print("경험치 : "+exp);
        }
            
        if(feedAmount > 0)
        {
            feedAmount -= 10;
        }
    }

    // 경험치에 따른 도깨비 성장
    void DokkaebiGrowth()
    {
        switch (growthLevel)
        {
            case 0:
                if (exp >= expRange[0] - 1)
                {
                    isLvUp = true;
                    exp++;
                    uiManager.ExpStateUpdate();
                    uiManager.lvUpBtn.SetActive(true);
                }
                else
                {
                    exp++;
                    uiManager.ExpStateUpdate();
                }
                break;
            case 1:
                if (exp >= expRange[1] - 1)
                {
                    isLvUp = true;
                    exp++;
                    uiManager.ExpStateUpdate();
                    uiManager.lvUpBtn.SetActive(true);
                }
                else
                {
                    exp++;
                    uiManager.ExpStateUpdate();
                    DokeV_JIHO.instance.Random_Event();
                }
                break;
        }
    }

    public void LvUpButton()
    {
        soundManager.ChangeAndPlaySfx(1); // 추가
        switch (growthLevel)
        {
            case 0:
                growthLevel = 1;
                uiManager.ExpStateReset();
                exp = 0;
                uiManager.ChangeExpText();
                dkbObjs[0].SetActive(false);
                dkbObjs[1].SetActive(true);
                dkbSpriteRs[1].sprite = lv1Sprites[toyType];
                dkbAnims[1].runtimeAnimatorController = lv1AnimCtrls[toyType];
                isLvUp = false;
                uiManager.lvUpBtn.SetActive(false);
                DokeV_JIHO.instance.Random_Event();
                break;
            case 1:
                print("성체됨");
                if(!uiManager.goYardBtn.activeSelf)
                    uiManager.goYardBtn.SetActive(true);
                uiManager.GoYard(); // 성체 마당으로 보냄
                GameObject newDkbObj = Instantiate(lv2Prefabs[toyType], collectionGroup.transform);
                newDkbObj.transform.position = collectionGroup.transform.position;
                newDkbObj.transform.rotation = collectionGroup.transform.rotation;
                isLvUp = false;
                uiManager.lvUpBtn.SetActive(false);
                CareRoomReset();
                break;
        }
    }

    // 성장이 끝나거나 방생됐을 때 변수 리셋
    void CareRoomReset()
    {
        isGrowStart = false;
        uiManager.isPopUp = true;
        isToyTime = false;
        toyType = 100;
        exp = 0;
        stressLevel = 0;
        feedAmount = 0;
        loveAmount = 0;
        growthLevel = 0;
        dkbObjs[0].SetActive(false);
        dkbObjs[1].SetActive(false);
        uiManager.dkbSettingBtn.SetActive(true);
        uiManager.expStateTMP.text = "";
        uiManager.careCoolTime.text = "";
        uiManager.ResetExpGroup();
        SetCareCoolTime();
    }

    public void LoveAmountUp()
    {
        loveAmount += 10;
    }

    public void FeedAmountUp()
    {
        feedAmount += 10;
    }
}