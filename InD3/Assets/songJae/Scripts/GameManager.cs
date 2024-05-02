using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    //==============================================[ 안내 메시지 ]==============================================
    // 안내 메시지
    [Header("----[ Info Message ]")]
    public GameObject infoMessage;
    public TMP_Text infoMessageText;

    //==============================================[ 음식 UI ]==============================================
    // 먹이 상자 UI 변수
    [Header("----[ Food UI ]")]
    //public bool isFullTime;
    public GameObject _foodPanel; // 먹이 상자 패널 UI
    public TMP_Text _foodInfoText; // 음식 정보 텍스트
    public TMP_Text _foodNameText; // 음식 이름 텍스트
    public TMP_Text[] _foodCountText; // 음식 개수 표출 텍스트
    public Image[] _foodItem; // 음식 이미지

    // 먹이 변수
    int foodType = 100; // 음식 종류, 아무것도 선택하지 않았을 때의 값을 미리 선언
    int[] foodCount = new int[4]; // 음식의 최대 개수와 각 음식의 개수

    //===============================================[ 시간 계산 ]=========================================
    // 시간 계산 변수
    [Header("----[ Time UI ]")]
    public TimerManager _timerManager;
    float remainingTimeH;
    float remainingTimeM;
    float remainingTimeS;
    public TMP_Text _careCoolTime;

    //==============================================[ 케어 지수, 정도, UI ]========================================
    // 케어 수치 변수
    [Header("----[ Care Level ]")]
    public int stressLevel;  // 스트레스 지수
    public int maxPlayCount; // 최대 놀이 가능 횟수
    public int exp;

    [Header("----[ Care Amount ]")]
    public int feedAmount;   // 배부른 정도
    public int cleanAmount;  // 깨끗한 정도
    public int loveAmount;   // 쓰다듬은 정도

    [Header("----[ Care UI ]")]
    public Image feedAmountImage;   // 배부른 정도 이미지 UI
    public Image cleanAmountImage;  // 깨끗한 정도 이미지 UI
    public Image loveAmountImage;   // 쓰다듬은 정도 이미지 UI

    public TMP_Text feedAmountText;    // 배부른 정도 텍스트 UI
    public TMP_Text cleanAmountText;   // 깨끗한 정도 텍스트 UI
    public TMP_Text loveAmountText;    // 쓰다듬은 정도 텍스트 UI

    // 케어 버튼 변수
    public Button _feedBtn;  // 먹이 버튼
    public Button _playBtn;  // 놀기 버튼
    public Button _cleanBtn; // 씻기 버튼
    public Button _loveBtn;  // 쓰다듬기 버튼

    // 현재 요구 진행 상태 변수
    bool isRequest; //요구 실행 여부
    //bool isPlayTime;
    bool isCleanTime;
    bool isLoveTime;

    public GameObject playBoxPanel;  // 놀이 상자 패널
    public GameObject cleanBoxPanel; // 씻기 상자 패널
    public GameObject loveBoxPanel;  // 쓰다듬기 상자 패널

    
  


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

    // 먹이 함수 1. 배부를 때는 밥X, 아닐 때 먹이 상자 Enable
    public void FoodPanelButton()
    {
        for(int i = 0; i < _foodCountText.Length; i++)
        {
            _foodCountText[i].text = foodCount[i].ToString();
        }

        _foodPanel.SetActive(true);
    }

    // 먹이 함수 2. 밥 주기 취소 (패널 Disable, 먹이 상자 초기화)
    public void FoodBoxCancel()
    {
        _foodPanel.SetActive(false);
        for (int i = 0; i < _foodItem.Length; i++)
        {
            _foodItem[i].color = new Color(1, 1, 1, 1);
        }
        _foodNameText.text = "음식 이름";
        _foodInfoText.text = "음식 선택 시 정보를 표시합니다.";
        foodType = 100;
    }

    // 먹이 함수 3. 먹이 눌렀을 때 정보 표출, 하나의 먹이만 선택 가능
    public void FoodBoxItem(int foodindex)
    {
        for (int i = 0; i < _foodItem.Length; i++)
        {
            if (i == foodindex)
            {
                _foodItem[foodindex].color = new Color(0.5f, 0.5f, 0.5f, 1);
                switch (foodindex)
                {
                    case 0:
                        _foodNameText.text = "0번 음식";
                        _foodInfoText.text = "0번이에유";
                        foodType = 0;
                        break;
                    case 1:
                        _foodNameText.text = "1번 음식";
                        _foodInfoText.text = "1번이에유";
                        foodType = 1;
                        break;
                    case 2:
                        _foodNameText.text = "2번 음식";
                        _foodInfoText.text = "2번이에유";
                        foodType = 2;
                        break;
                    case 3:
                        _foodNameText.text = "3번 음식";
                        _foodInfoText.text = "3번이에유";
                        foodType = 3;
                        break;
                }
            }
            else
            {
                _foodItem[i].color = new Color(1, 1, 1, 1);
            }
        }
    }

    // 먹이 함수 4. 먹이를 주고 먹이 종류에 따라 포만도 변화 (패널 Disable, 배부름 bool값 true, 최대체력, 포만도 수치 증가)
    public void FoodBoxAccept()
    {
        if (feedAmount >= 100)
        {
            StartCoroutine(GameinfoMessage("배부른 것 같습니다."));
            return;
        }

        if (foodType == 100)
        {
            StartCoroutine(GameinfoMessage("줄 음식을 선택해주세요."));
            return;
        }

        if (foodCount[foodType] > 0)
        {
            print("밥 먹었어유");
            foodCount[foodType]--;
            feedAmount += 10;
            FoodBoxCancel();
        }
        else
        {
            StartCoroutine(GameinfoMessage("음식을 보유하고 있지 않습니다."));
        }
    }


    // ==============================================================================================================

    // 케어 함수. 케어 쿨타임
    void SetCareCoolTime()
    {
        PlayerPrefs.SetFloat("EndHour", _timerManager._dataTime.Hour + 0f);
        PlayerPrefs.SetFloat("EndMinute", _timerManager._dataTime.Minute + 0f);
        PlayerPrefs.SetFloat("EndSecond", _timerManager._dataTime.Second + 30f);
        remainingTimeH = PlayerPrefs.GetFloat("EndHour") - _timerManager._dataTime.Hour;
        remainingTimeM = PlayerPrefs.GetFloat("EndMinute") - _timerManager._dataTime.Minute;
        remainingTimeS = PlayerPrefs.GetFloat("EndSecond") - _timerManager._dataTime.Second;
    }

    // 케어 함수. 현재 시간 + n시간 n분 n초가 쿨타임 끝시간, 남은 시간 = 쿨타임 끝 시간 - 현재 시간
    void CareCoolDown()
    {
        // 쿨타임 감소 표출
        remainingTimeS -= Time.deltaTime;
        if (remainingTimeS <= 0f && remainingTimeM != 0f)
        {
            remainingTimeM -= 1f;
            remainingTimeS = 60f;
        }
        else if (remainingTimeM <= 0f && remainingTimeH != 0f)
        {
            remainingTimeH -= 1f;
            remainingTimeM = 60f;
        }
        _careCoolTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)remainingTimeH, (int)remainingTimeM, (int)remainingTimeS);

        // 쿨타임이 끝났을 때 쿨타임, 배부름, 요구bool, 요구Text들 초기화
        if (remainingTimeS <= 0 && remainingTimeM <= 0 && remainingTimeH <= 0)
        {
            if (!isRequest)
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
            isRequest = false;
            isCleanTime = false;
            isLoveTime = false;
            maxPlayCount = 3;
            SetCareCoolTime();
        }
    }

    // 요구 함수. 요구를 들어준 상태면 실행X
    void Request()
    {
        if (isRequest)
            return;
        exp++;
        isRequest = true;
    }

    // 케어 함수 Play. 
    public void PlayButton()
    {
        if (maxPlayCount <= 0)
        {
            StartCoroutine(GameinfoMessage("피곤해 보입니다."));
        }
        else
        {
            playBoxPanel.SetActive(true);
        }
    }

    public void PlayBoxAccept()
    {
        maxPlayCount--;
        playBoxPanel.SetActive(false);
    }
    public void PlayBoxCancel()
    {
        playBoxPanel.SetActive(false);
    }


    // 케어 함수 Clean.
    public void CleanButton()
    {
        if (cleanAmount >= 100)
        {
            StartCoroutine(GameinfoMessage("이미 깨끗합니다."));
            return;
        }
        else if (isCleanTime)
        {
            StartCoroutine(GameinfoMessage("지금은 씻기 싫은 것 같습니다."));
            return;
        }
        else
        {
            cleanBoxPanel.SetActive(true);
        }
    }

    public void CleanBoxSuccessed() //임시로 public 후 버튼 상호작용
    {
        Request();
        isCleanTime = true;
        cleanAmount += 10;
        cleanBoxPanel.SetActive(false);
    }
    
    public void CleanBoxCancel()
    {
        cleanBoxPanel.SetActive(false);
    }

    // 케어 함수 Touch.
    public void LoveButton()
    {
        if (loveAmount >= 100)
        {
            StartCoroutine(GameinfoMessage("충분히 사랑을 느끼고 있습니다."));
            return;
        }
        else if (isLoveTime)
        {
            StartCoroutine(GameinfoMessage("지금은 귀찮아 보입니다."));
            return;
        }
        else
        {
            loveBoxPanel.SetActive(true);
        }
    }

    public void LoveBoxSuccessed() //임시로 public 후 버튼 상호작용
    {
        Request();
        isLoveTime = true;
        loveAmount += 10;
        loveBoxPanel.SetActive(false);
    }

    public void LoveBoxCancel()
    {
        loveBoxPanel.SetActive(false);
    }

    // ==============================================================================================================

    // 게임 내 안내메시지
    IEnumerator GameinfoMessage(string infoMessageStr)
    {
        infoMessage.SetActive(true);
        infoMessageText.text = infoMessageStr;
        yield return new WaitForSeconds(2);
        infoMessage.SetActive(false);
    }

    // UI
    // 좌측 상단 포만도, 애정도, 청결도 UI Button
    public void SetCareAmountText(int careAmountType)
    {
        StartCoroutine(CareAmountTextNumber(careAmountType));
    }

    IEnumerator CareAmountTextNumber(int careAmountType)
    {
        // 각 케어 지수 아이콘 이미지 알파값 낮추고 현재 지수 텍스트 활성화
        switch(careAmountType)
        {
            case 0:
                feedAmountImage.color = new Color(1, 1, 1, 0.5f);
                feedAmountText.enabled = true;
                feedAmountText.text = string.Format("{0}\n/100", feedAmount);
            break;
            case 1:
                cleanAmountImage.color = new Color(1, 1, 1, 0.5f);
                cleanAmountText.enabled = true;
                cleanAmountText.text = string.Format("{0}\n/100", cleanAmount);
            break;
            case 2:
                loveAmountImage.color = new Color(1, 1, 1, 0.5f);
                loveAmountText.enabled = true;
                loveAmountText.text = string.Format("{0}\n/100", loveAmount);
            break;
        }

        yield return new WaitForSeconds(3f);

        // 각 케어 지수 아이콘 이미지 알파값 높이고 현재 지수 텍스트 비활성화
        switch (careAmountType)
        {
            case 0:
                feedAmountImage.color = new Color(1, 1, 1, 1);
                feedAmountText.enabled = false;
                break;
            case 1:
                cleanAmountImage.color = new Color(1, 1, 1, 1);
                cleanAmountText.enabled = false;
                break;
            case 2:
                loveAmountImage.color = new Color(1, 1, 1, 1);
                loveAmountText.enabled = false;
                break;
        }
    }
}