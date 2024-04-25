using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    
    //==============================================[ 안내 메시지 ]====================================================
    // 안내 메시지
    [Header("----[ Info Message ]")]
    public GameObject infoMessage;
    public TMP_Text infoMessageText;

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
    public Sprite[] playTypeSprites;
    public GameObject curGameStateGroup;
    public GameObject curGameStateObj;
    public Image curGameImage;
    
    //===============================================[ 시간 계산 ]====================================================
    
    public TMP_Text careCoolTime;

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

    // public GameObject shopPanel; // 상점 패널
    public GameObject foodBoxPanel;  // 먹이 상자 패널
    public GameObject playBoxPanel;  // 놀이 상자 패널

    public Image[] expStates;
    
    //==============================================[ 기타 ]==========================================================

    private bool _isPopUp;
    
    // ==============================================================================================================

    private void Awake()
    {
        // 필요 UI를 제외한 나머지 전부 비활성화하고 시작
        infoMessage.SetActive(false);
        foodPanel.SetActive(false);
        foodBoxPanel.SetActive(false);
        playBoxPanel.SetActive(false);
    }

    // ==============================================================================================================

    // 먹이 함수 1. 배부를 때는 밥X, 아닐 때 먹이 상자 Enable
    public void FoodPanelButton()
    {
        if(_isPopUp)
            return;
        
        for(int i = 0; i < foodCountText.Length; i++)
        {
            foodCountText[i].text = gameManager.foodCount[i].ToString();
        }

        foodPanel.SetActive(true);
        _isPopUp = true;
    }

    // 먹이 함수 2. 밥 주기 취소 (패널 Disable, 먹이 상자 초기화)
    public void FoodBoxCancel()
    {
        foodPanel.SetActive(false);
        _isPopUp = false;
        for (int i = 0; i < foodItem.Length; i++)
        {
            foodItem[i].color = new Color(1, 1, 1, 1);
        }
        foodNameText.text = "음식 이름";
        foodInfoText.text = "음식 선택 시 정보를 표시합니다.";
        gameManager.foodType = 100;
    }

    // 먹이 함수 3. 먹이 눌렀을 때 정보 표출, 하나의 먹이만 선택 가능
    public void FoodBoxItem(int foodindex)
    {
        for (int i = 0; i < foodItem.Length; i++)
        {
            if (i == foodindex)
            {
                foodItem[foodindex].color = new Color(0.5f, 0.5f, 0.5f, 1);
                switch (foodindex)
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
            StartCoroutine(GameinfoMessage("배부른 것 같습니다."));
            return;
        }

        if (gameManager.foodType == 100)
        {
            StartCoroutine(GameinfoMessage("줄 음식을 선택해주세요."));
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
            StartCoroutine(GameinfoMessage("음식을 보유하고 있지 않습니다."));
        }
    }
    
    // ==============================================================================================================
    
    // 케어 함수 Play. 
    public void PlayBoxButton()
    {
        if(_isPopUp)
            return;
        
        if (gameManager.maxPlayCount <= 0)
        {
            StartCoroutine(GameinfoMessage("피곤해 보입니다."));
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
            _isPopUp = true;
        }
    }
    
    public void PlayBoxAccept()
    {
        gameManager.maxPlayCount--;
        playBoxPanel.SetActive(false);
        _isPopUp = false;
    }
    
    public void PlayBoxCancel()
    {
        playBoxPanel.SetActive(false);
        _isPopUp = false;
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
        GameObject newGameState = Instantiate(curGameStateObj, curGameStateGroup.transform);
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

    public void EXPStateUpdate()
    {
        for (int i = 1; i < gameManager.exp; i++)
        {
            expStates[i].color = Color.yellow;;
        }
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
    
   
}
