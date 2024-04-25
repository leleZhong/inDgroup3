using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public UIManager uiManager;
    
    // 먹이 변수
    public int foodType = 100; // 음식 종류, 아무것도 선택하지 않았을 때의 값을 미리 선언
    public int[] foodCount = new int[4]; // 음식의 최대 개수와 각 음식의 개수

    //===============================================[ 시간 계산 ]=========================================
    // 시간 계산 변수
    [Header("----[ Time UI ]")]
    public TimerManager timerManager;
    private float _remainingTimeH;
    private float _remainingTimeM;
    private float _remainingTimeS;


    //==============================================[ 케어 지수, 정도, UI ]========================================
    // 케어 수치 변수
    [Header("----[ Care Level ]")]
    public int stressLevel;  // 스트레스 지수
    public int maxPlayCount; // 최대 놀이 가능 횟수
    public int exp;

    [Header("----[ Care Amount ]")]
    public int feedAmount;   // 배부른 정도
    public int loveAmount;   // 쓰다듬은 정도


    // 현재 요구 진행 상태 변수
    private bool _isRequest; //요구 실행 여부
    //private bool _isPlayTime;

    private void Awake()
    {
        _instance = this;
        for (int i = 0; i < foodCount.Length; i++)
        {
            foodCount[i] = 0;
        }

        foodCount[0] = 1; 
        foodCount[1] = 1; 
        foodCount[2] = 1;
        foodCount[3] = 1;
        
    }

    private void Start()
    {
        SetCareCoolTime();
    }

    private void Update()
    {
        CareCoolDown();
    }

    // ==============================================================================================================

    // 케어 함수. 케어 쿨타임
    void SetCareCoolTime()
    {
        PlayerPrefs.SetFloat("EndHour", timerManager._dataTime.Hour + 0f);
        PlayerPrefs.SetFloat("EndMinute", timerManager._dataTime.Minute + 0f);
        PlayerPrefs.SetFloat("EndSecond", timerManager._dataTime.Second + 30f);
        _remainingTimeH = PlayerPrefs.GetFloat("EndHour") - timerManager._dataTime.Hour;
        _remainingTimeM = PlayerPrefs.GetFloat("EndMinute") - timerManager._dataTime.Minute;
        _remainingTimeS = PlayerPrefs.GetFloat("EndSecond") - timerManager._dataTime.Second;
    }

    // 케어 함수. 현재 시간 + n시간 n분 n초가 쿨타임 끝시간, 남은 시간 = 쿨타임 끝 시간 - 현재 시간
    void CareCoolDown()
    {
        // 쿨타임 감소 표출
        _remainingTimeS -= Time.deltaTime;
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
        uiManager.careCoolTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)_remainingTimeH, (int)_remainingTimeM, (int)_remainingTimeS);

        // 쿨타임이 끝났을 때 쿨타임, 배부름, 요구bool, 요구Text들 초기화
        if (_remainingTimeS <= 0 && _remainingTimeM <= 0 && _remainingTimeH <= 0)
        {
            if (!_isRequest)
            {
                if(stressLevel >= 10)
                {
                    print("도깨비 방생 애니메이션 등장");
                    // 도깨비 있던 곳 비워지고 모든 값 초기화
                    // 새로운 도깨비 데려오는 버튼 도깨비 불 있는 곳에 활성화
                }
                else
                {
                    stressLevel++;
                }
            }
            _isRequest = false;
            maxPlayCount = 3;
            SetCareCoolTime();
        }
    }

    // 요구 함수. 요구를 들어준 상태면 실행X
    void Request()
    {
        if (_isRequest)
            return;
        exp++;
        uiManager.EXPStateUpdate();
        _isRequest = true;
    }
    
}