using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject gameStartScene;
    public GameObject gameIntroScene;
    public GameObject roomScene;
    public GameObject miniGameScene;
    public GameObject perfectGameScene;
    public GameObject yardScene;

    //==============================================[ 안내 메시지 ]====================================================
    // 안내 메시지
    [Header("----[ Info Message ]")]
    public GameObject infoMessage;
    public TMP_Text infoMessageText;
    private bool _isInfoTime;

    //==============================================[ 음식 UI ]======================================================
    // 먹이 상자 UI 변수
    [Header("----[ Food UI ]")]
    //public bool isFullTime;
    public GameObject foodPanel; // 먹이 상자 패널 UI
    public TMP_Text foodInfoText; // 음식 정보 텍스트
    public TMP_Text foodNameText; // 음식 이름 텍스트
    public TMP_Text[] foodCountText; // 음식 개수 표출 텍스트
    public Image[] foodItem; // 음식 이미지
    
    //==============================================[ 놀이 UI ]======================================================
    //  놀이 상자 변수
    [Header("----[ Play UI ]")]
    public GameObject playBoxPanel;  // 놀이 상자 패널
    public Sprite[] playTypeSprites; // 구현된 놀이 종류 스프라이트
    public GameObject curGameStateGroup; // 현재 게임 순서에 따라 바뀌는 하단 점 상태 표시 그룹
    public GameObject curGameStatePrefab; // 하단 점 프리펩
    public Image curGameImage; // 현재 놀이 상자에 표시되는 게임 이미지

    public GameObject perfectGameStartBtn;
    public TMP_Text curPlayCountText;
    public bool isMiniGameStart;
    
    //===============================================[ 시간 계산 ]====================================================
    
    public TMP_Text careCoolTime; // 게임 쿨타임 텍스트

    //==============================================[ 케어 지수, 정도, UI ]============================================
    [Header("----[ Care UI ]")]
    public Image feedAmountImage;   // 배부른 정도 이미지 UI
    public Image loveAmountImage;   // 쓰다듬은 정도 이미지 UI

    public TMP_Text feedAmountText;    // 배부른 정도 텍스트 UI
    public TMP_Text loveAmountText;    // 쓰다듬은 정도 텍스트 UI

    // 케어 버튼 변수
    public Button shopBtn; // 상점 버튼
    public Button feedBtn;  // 먹이 버튼
    public Button playBtn;  // 놀기 버튼

    public GameObject shopPanel; // 상점 패널
    public GameObject toyPanel;
    public TMP_Text toyInfoText; // 장난감 정보 텍스트
    public TMP_Text toyNameText; // 장난감 이름 텍스트
    public Image[] toyItemBtn; // 보이는 장난감 이미지
    public Sprite[] toyItemO; // 장난감 있을 때 이미지
    public Sprite[] toyItemX; // 장난감 없을 때 이미지
    public Sprite[] toyItemSelected; // 장난감 선택시 이미지


    public Image[] expStates; // 경험치 상태 - 게임오브젝트로 수정해
    public GameObject expStateGroup; // 경험치 상태 오브젝트 그룹
    public GameObject expStatePrefab; // 경험치 상태 오브젝트 프리펩
    public TMP_Text expStateTMP; // 경험치 상태 텍스트 99/99

    public GameObject dkbSettingBtn; // 도깨비 세팅 버튼

    public GameObject countDownPanel;
    public TMP_Text countDownText;
    public bool isCountDown;

    public TMP_Text playerName;
    public GameObject playerNameInputGroup;
    public TMP_Text curCoinText;
    public int perfectResultCoin;

    //==============================================[ 기타 ]==========================================================

    public bool isPopUp;
    public bool isSetToy;
    
    // ==============================================================================================================

    private void Awake()
    {
        // 필요 UI를 제외한 나머지 전부 비활성화하고 시작
        infoMessage.SetActive(false);
        foodPanel.SetActive(false);
        playBoxPanel.SetActive(false);
        
        gameStartScene.SetActive(true);
        gameIntroScene.SetActive(false);
        roomScene.SetActive(false);
        miniGameScene.SetActive(false);
        perfectGameScene.SetActive(false);
        yardScene.SetActive(false);

        AddCurExpState();
        ChangeExpText();
    }

    private void Start()
    {
        curCoinText.text = GameManager._instance.coin.ToString();
        curPlayCountText.text = String.Format("{0:D1}/{1:D1}", gameManager.playCount, gameManager.maxPlayCount);
    }
    // ==============================================================================================================

    // 먹이 함수 1. 배부를 때는 밥X, 아닐 때 먹이 상자 Enable
    public void FoodPanelButton()
    {
        if(isPopUp || isSetToy)
            return;
        
        for(int i = 0; i < foodCountText.Length; i++)
        {
            foodCountText[i].text = gameManager.foodCount[i].ToString();
        }

        foodPanel.SetActive(true);
        isPopUp = true;
    }

    // 먹이 함수 2. 밥 주기 취소 (패널 Disable, 먹이 상자 초기화)
    public void FoodBoxCancel()
    {
        foodPanel.SetActive(false);
        isPopUp = false;
        for (int i = 0; i < foodItem.Length; i++)
        {
            foodItem[i].color = new Color(1, 1, 1, 1);
        }
        foodNameText.text = "음식 이름";
        foodInfoText.text = "음식 선택 시 정보를 표시합니다.";
        gameManager.foodType = 100;
    }

    // 먹이 함수 3. 먹이 눌렀을 때 정보 표출, 하나의 먹이만 선택 가능
    public void FoodBoxItem(int foodIndex)
    {
        for (int i = 0; i < foodItem.Length; i++)
        {
            if (i == foodIndex)
            {
                foodItem[foodIndex].color = new Color(0.5f, 0.5f, 0.5f, 1);
                switch (foodIndex)
                {
                    case 0:
                        foodNameText.text = "0번 음식";
                        foodInfoText.text = "0번이에유";
                        gameManager.foodType = 0;
                        break;
                    case 1:
                        foodNameText.text = "1번 음식";
                        foodInfoText.text = "1번이에유";
                        gameManager.foodType = 1;
                        break;
                    case 2:
                        foodNameText.text = "2번 음식";
                        foodInfoText.text = "2번이에유";
                        gameManager.foodType = 2;
                        break;
                    case 3:
                        foodNameText.text = "3번 음식";
                        foodInfoText.text = "3번이에유";
                        gameManager.foodType = 3;
                        break;
                }
            }
            else
            {
                foodItem[i].color = new Color(1, 1, 1, 1);
            }
        }
    }

    // 먹이 함수 4. 먹이를 주고 먹이 종류에 따라 포만도 변화 (패널 Disable, 배부름 bool값 true, 최대체력, 포만도 수치 증가)
    public void FoodBoxAccept()
    {
        if (gameManager.feedAmount >= 100)
        {
            StartCoroutine(GameInfoMessage("배부른 것 같습니다."));
            return;
        }

        if (gameManager.foodType == 100)
        {
            StartCoroutine(GameInfoMessage("줄 음식을 선택해주세요."));
            return;
        }

        if (gameManager.foodCount[gameManager.foodType] > 0)
        {
            print("밥 먹었어유");
            gameManager.foodCount[gameManager.foodType]--;
            gameManager.feedAmount += 10;
            FoodBoxCancel();
        }
        else
        {
            StartCoroutine(GameInfoMessage("음식을 보유하고 있지 않습니다."));
        }
    }
    
    // ==============================================================================================================
    
    // 케어 함수 Play. 
    public void PlayBoxButton()
    {
        if(isPopUp || isSetToy)
            return;
        
        if (gameManager.playCount <= 0)
        {
            StartCoroutine(GameInfoMessage("피곤해 보입니다."));
        }
        else
        {
            playBoxPanel.SetActive(true);
            for (int i = 0; i < playTypeSprites.Length; i++)
            {
                AddCurGameState();
                if (curGameImage.sprite == playTypeSprites[i])
                {
                    int curGameIndex = i;
                    ChangeCurGameState(curGameIndex);
                }
            }
            isPopUp = true;
        }
    }
    
    public void PlayBoxAccept()
    {
        gameManager.playCount--;
        curPlayCountText.text = String.Format("{0:D1}/{1:D1}", gameManager.playCount, gameManager.maxPlayCount);
        playBoxPanel.SetActive(false);
        isPopUp = false;
        for (int i = 0; i < playTypeSprites.Length; i++)
        {
            if (curGameImage.sprite == playTypeSprites[i])
            {
                switch (i)
                {
                    case 0:
                        miniGameScene.SetActive(true);
                        isMiniGameStart = false;
                        CircleManager.Instance.GameStart();
                        break;
                    case 1:
                        perfectGameScene.SetActive(true);
                        break;
                }
            }
        }
    }
    
    public void PlayBoxCancel()
    {
        playBoxPanel.SetActive(false);
        isPopUp = false;
    }

    public void NextGameBtn()
    {
        for (int i = 0; i < playTypeSprites.Length; i++)
        {
            if (curGameImage.sprite == playTypeSprites[i])
            {
                int curGameIndex = i + 1 >= playTypeSprites.Length
                    ? 0
                    : i + 1;
                curGameImage.sprite = playTypeSprites[curGameIndex];
                ChangeCurGameState(curGameIndex);
                return;
            }
        }
    }
    
    public void PreGameBtn()
    {
        for (int i = 0; i < playTypeSprites.Length; i++)
        {
            if (curGameImage.sprite == playTypeSprites[i])
            {
                int curGameIndex = i - 1 < 0
                    ? playTypeSprites.Length - 1
                    : i - 1;
                curGameImage.sprite = playTypeSprites[curGameIndex];
                ChangeCurGameState(curGameIndex);
                return;
            }
        }
    }

    void AddCurGameState()
    {
        if(curGameStateGroup.GetComponentsInChildren<Image>().Length == playTypeSprites.Length)
            return;
        GameObject newGameState = Instantiate(curGameStatePrefab, curGameStateGroup.transform);
        newGameState.transform.position = curGameStateGroup.transform.position;
        newGameState.transform.rotation = curGameStateGroup.transform.rotation;
        
    }

    void ChangeCurGameState(int index)
    {
        Image[] curGame = curGameStateGroup.GetComponentsInChildren<Image>();
        for (int i = 0; i < curGame.Length; i++)
        {
            if (i == index)
            {
                curGame[index].color = Color.black;
            }
            else
            {
                curGame[i].color = Color.white;
            }
        }

    }
    
    // ==============================================================================================================

    public void ExpStateUpdate()
    {
        for (int i = 0; i < gameManager.exp; i++)
        {
            expStates[i].color = Color.yellow;;
            ChangeExpText();
        }
    }

    public void ExpStateReset()
    {
        for (int i = 0; i < gameManager.exp; i++)
        {
            expStates[i].color = Color.black;;
        }
        AddCurExpState();
    }
    
    void AddCurExpState()
    {
        for (int i = 0; i < gameManager.expRange[gameManager.growthLevel]; i++)
        {
            GameObject newExpState = Instantiate(expStatePrefab, expStateGroup.transform);
            newExpState.transform.position = expStateGroup.transform.position;
            newExpState.transform.rotation = expStateGroup.transform.rotation;
        }
        expStates = expStateGroup.GetComponentsInChildren<Image>();
    }
    
    public void ChangeExpText()
    {
        expStateTMP.text = string.Format("{0:D2}:{1:D2}", gameManager.exp, gameManager.expRange[gameManager.growthLevel]);
    }
    
    // ==============================================================================================================
    
    // 게임 내 안내메시지
    public IEnumerator GameInfoMessage(string infoMessageStr)
    {
        if (_isInfoTime)
            yield break;
        _isInfoTime = true;
        infoMessage.SetActive(true);
        infoMessageText.text = infoMessageStr;
        yield return new WaitForSeconds(1);
        infoMessage.SetActive(false);
        _isInfoTime = false;
    }

    // UI
    // 좌측 상단 포만도, 애정도, 청결도 UI Button
    public void SetCareAmountText(int careAmountType)
    {
        StartCoroutine(CareAmountTextNumber(careAmountType));
    }

    private IEnumerator CareAmountTextNumber(int careAmountType)
    {
        // 각 케어 지수 아이콘 이미지 알파값 낮추고 현재 지수 텍스트 활성화
        switch(careAmountType)
        {
            case 0:
                feedAmountImage.color = new Color(1, 1, 1, 0.5f);
                feedAmountText.enabled = true;
                feedAmountText.text = string.Format("{0}\n/100", gameManager.feedAmount);
            break;
            case 1:
                loveAmountImage.color = new Color(1, 1, 1, 0.5f);
                loveAmountText.enabled = true;
                loveAmountText.text = string.Format("{0}\n/100", gameManager.loveAmount);
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
                loveAmountImage.color = new Color(1, 1, 1, 1);
                loveAmountText.enabled = false;
                break;
        }
    }
    

    // ==============================================================================================================
    // 도깨비 장난감 놓는 +버튼 상호작용시
    public void SetDokkaebiButton()
    {
        if(isPopUp)
            return;

        toyPanel.SetActive(true);
        isPopUp = true;
    }
    
    // 장난감 놓는 패널 닫기
    public void ToyBoxCancel()
    {
        toyPanel.SetActive(false);
        isPopUp = false;
        toyNameText.text = "장난감 이름";
        toyInfoText.text = "장난감 선택 시 정보를 표시합니다.";
        if(gameManager.toyType != 100)
            toyItemBtn[gameManager.toyType].sprite = toyItemO[gameManager.toyType];
        gameManager.toyType = 100;
    }

    // 장난감 패널 열 때 아이템 목록 설정
    public void ToyBoxItem(int toyIndex)
    {
        for (int i = 0; i < toyItemBtn.Length; i++)
        {
            if (i == toyIndex)
            {
                if (toyItemBtn[i].sprite == toyItemX[i])
                {
                    StartCoroutine(GameInfoMessage("장난감을 먼저 구매해주세요."));
                    return;
                }
                
                if (gameManager.toyType != 100)
                {
                    toyItemBtn[gameManager.toyType].sprite = toyItemO[gameManager.toyType];
                }
                toyItemBtn[toyIndex].sprite = toyItemSelected[toyIndex];
                gameManager.toyType = toyIndex;
                switch (gameManager.toyType)
                {
                    case 0:
                        toyNameText.text = "0번 장난감";
                        toyInfoText.text = "0번이에유";
                        break;
                    case 1:
                        toyNameText.text = "1번 장난감";
                        toyInfoText.text = "1번이에유";
                        break;
                    case 2:
                        toyNameText.text = "2번 장난감";
                        toyInfoText.text = "2번이에유";
                        break;
                    case 3:
                        toyNameText.text = "3번 장난감";
                        toyInfoText.text = "3번이에유";
                        break;
                }
            }
        }
    }

    // 장난감 선택 
    public void ToyBoxAccept()
    {
        if (gameManager.toyType == 100)
        {
            StartCoroutine(GameInfoMessage("놓을 장난감을 선택해주세요."));
            return;
        }
        
        toyItemBtn[gameManager.toyType].sprite = toyItemX[gameManager.toyType];
        
        // 장난감 패널 닫기
        toyPanel.SetActive(false);
        toyItemBtn[gameManager.toyType].sprite = toyItemO[gameManager.toyType];
        
        // 도깨비 장난감 설치
        dkbSettingBtn.SetActive(false);
        gameManager.dkbToy.SetActive(true);
        SpriteRenderer dokkaebiToySpriteRenderer = gameManager.dkbToy.GetComponent<SpriteRenderer>();
        dokkaebiToySpriteRenderer.sprite = toyItemO[gameManager.toyType];

        gameManager.coolH = gameManager.toyTimeCoolH;
        gameManager.coolM = gameManager.toyTimeCoolM;
        gameManager.coolS = gameManager.toyTimeCoolS;
        gameManager.SetCareCoolTime();
        gameManager.isGrowStart = true;
    }
    
    // ==============================================================================================================

    public void ShopBoxPanelBtn()
    {
        if(isPopUp || isSetToy)
            return;
        isPopUp = true;
        shopPanel.SetActive(true);
    }
    
    // ==============================================================================================================

    public void GoYard()
    {
        roomScene.SetActive(false);
        yardScene.SetActive(true);
        gameManager.collectionGroup.SetActive(true);
    }

    public void GoRoom()
    {
        roomScene.SetActive(true);
        yardScene.SetActive(false);
        gameManager.collectionGroup.SetActive(false);
        playerName.text = PlayerPrefs.GetString("PlayerName");
        isSetToy = true;
    }

    public void GoIntro()
    {
        gameStartScene.SetActive(false);
        gameIntroScene.SetActive(true);
        playerNameInputGroup.SetActive(true);
    }

    // ==============================================================================================================
    
    public IEnumerator GameCountDown()
    {
        countDownPanel.SetActive(true);
        isCountDown = true;
        countDownText.text = "3";
        yield return new WaitForSeconds(1);
        countDownText.text = "2";
        yield return new WaitForSeconds(1);
        countDownText.text = "1";
        yield return new WaitForSeconds(1);
        countDownText.text = "Go!";
        yield return new WaitForSeconds(1);
        countDownPanel.SetActive(false);
        isCountDown = false;
    }

    public void SetCurGameCoinText()
    {
        curCoinText.text = GameManager._instance.coin.ToString();
    }
    
    public void PerfectGameResultBtn()
    {
        //Ex)    GetGold( Coin * MaxCombo_Count );
        // Go Main Game Screen
        GameManager._instance.coin += perfectResultCoin;
        perfectGameScene.SetActive(false);
        perfectGameStartBtn.SetActive(true);
        SetCurGameCoinText();
    }
}
