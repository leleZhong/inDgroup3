using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    // 전역변수 및 배열
    static int itemtype = 100;                                              // 0 = 먹이, 1 = 가구
    static int[] foodPrice_A = new int[] { 100, 200, 200, 200, 300 };      // 먹이 4개
    static int foodtype = 100;                                              // 먹이 타입 0~3
    static int foodQuantity = 0;                                           // 먹이 아이템 수량 담긴 변수
    static int foodPrice = 0;                                              // 먹이 가격
    /*
    static int[] interiorPrice_A = new int[] { 100, 200, 200, 200 };      // 
    static int interiorType = 100;                                         
    static int interiorQuantity = 0;                                       
    static int interioPrice = 0;
    */
    static private int currentIndex1 = 0;
    static private int currentIndex2 = 1;
    static private int currentIndex3 = 2;

    public Sprite[] food_sprites;
    
    public Image Chang_food_image_L; 
    public Image Chang_food_image_C;
    public Image Chang_food_image_R;
    public Image Purchase_change_image;


    // 오브젝트
    public GameObject _Shop_Panel;                                          
    public GameObject _ShopFood;                                                                                   
    public GameObject _Purchase_Panel;                                      
    
    // 텍스트
    public TMP_Text _Price_count;                                           
    public TMP_Text _Quantity_count;
    public TMP_Text _Purchase_Food_Name_TMP;



    // 메인에서 상점 들어가는 버튼
    public void Main_Shop_Btn()
    {
        _Shop_Panel.SetActive(true);
    }
    // 상점패널 확인 버튼
    public void ShopOk_Btn()
    {
        
        currentIndex1 = 0;
        currentIndex2 = 1;
        currentIndex3 = 2;
        
        
        
        _Shop_Panel.SetActive(false);
        GameManager._instance.uiManager.isPopUp = false;
        Chang_food_image_L.sprite = food_sprites[currentIndex1];
        Chang_food_image_C.sprite = food_sprites[currentIndex2];
        Chang_food_image_R.sprite = food_sprites[currentIndex3];
        GameManager._instance.soundManager.ChangeAndPlaySfx(0); // 추가
    }
    // 먹이 탭 버튼
    public void ShopFood_Btn()
    {
        //_ShopFood.SetActive(true);
    }
    // 인테리어 탭 버튼
    public void Shopinterior_Btn()
    {
        StartCoroutine(GameManager._instance.uiManager.GameInfoMessage("준비중입니다."));

    }
    // -----------------------------------먹이 탭 -----------------------------------------------------------------------
    // 먹이탭에서 먹이 넘긴는 버튼
    public void Next_Btn()
    {
        GameManager._instance.soundManager.ChangeAndPlaySfx(0); // 추가
        currentIndex1++;
        if (currentIndex1 >= food_sprites.Length)
        {
            currentIndex1 = 0;
        }
        Chang_food_image_L.sprite = food_sprites[currentIndex1];

        currentIndex2++;
        if (currentIndex2 >= food_sprites.Length)
        {
            currentIndex2 = 0;
        }
        Chang_food_image_C.sprite = food_sprites[currentIndex2];

        currentIndex3++;
        if (currentIndex3 >= food_sprites.Length)
        {
            currentIndex3 = 0;
        }
        Chang_food_image_R.sprite = food_sprites[currentIndex3];
    }
    //왼쪽 첫번째 먹이 버튼
    public void Shop_food_L()
    {
        GameManager._instance.soundManager.ChangeAndPlaySfx(0); // 추가
        switch(currentIndex1)
        {
            case 0:
                itemtype = 0;
                foodtype = 0;
                foodQuantity = 1;
                foodPrice_A[foodtype] = 100;
                _Purchase_Food_Name_TMP.text = "기본먹이";
                Purchase_change_image.sprite = food_sprites[0];
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                _Purchase_Panel.SetActive(true);
                break;
            case 1:
                itemtype = 0;
                foodtype = 1;
                foodQuantity = 1;
                foodPrice_A[foodtype] = 250;
                _Purchase_Food_Name_TMP.text = "김치볶음밥";
                Purchase_change_image.sprite = food_sprites[1];
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                _Purchase_Panel.SetActive(true);
                break;
            case 2:
                itemtype = 0;
                foodtype = 2;
                foodQuantity = 1;
                foodPrice_A[foodtype] = 400;
                _Purchase_Food_Name_TMP.text = "바나나우유";
                Purchase_change_image.sprite = food_sprites[2];
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                _Purchase_Panel.SetActive(true);
                break;
            case 3:
                itemtype = 0;
                foodtype = 3;
                foodQuantity = 1;
                foodPrice_A[foodtype] = 1500;
                _Purchase_Food_Name_TMP.text = "핫케이크";
                Purchase_change_image.sprite = food_sprites[3];
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                _Purchase_Panel.SetActive(true);
                break;
        }
        
    }
    // 가운데 먹이 버튼
    public void Shop_food_C()
    {
        GameManager._instance.soundManager.ChangeAndPlaySfx(0); // 추가
        switch (currentIndex2)
        {
            case 0:
                itemtype = 0;
                foodtype = 0;
                foodQuantity = 1;
                foodPrice_A[foodtype] = 100;
                _Purchase_Food_Name_TMP.text = "기본먹이";
                Purchase_change_image.sprite = food_sprites[0];
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                _Purchase_Panel.SetActive(true);
                break;
            case 1:
                itemtype = 0;
                foodtype = 1;
                foodQuantity = 1;
                foodPrice_A[foodtype] = 250;
                _Purchase_Food_Name_TMP.text = "김치볶음밥";
                Purchase_change_image.sprite = food_sprites[1];
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                _Purchase_Panel.SetActive(true);
                break;
            case 2:
                itemtype = 0;
                foodtype = 2;
                foodQuantity = 1;
                foodPrice_A[foodtype] = 400;
                _Purchase_Food_Name_TMP.text = "바나나우유";
                Purchase_change_image.sprite = food_sprites[2];
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                _Purchase_Panel.SetActive(true);
                break;
            case 3:
                itemtype = 0;
                foodtype = 3;
                foodQuantity = 1;
                foodPrice_A[foodtype] = 1500;
                _Purchase_Food_Name_TMP.text = "핫케이크";
                Purchase_change_image.sprite = food_sprites[3];
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                _Purchase_Panel.SetActive(true);
                break;
        }

    }
    //오른쪽 끝 먹이 버튼
    public void Shop_food_R()
    {
        GameManager._instance.soundManager.ChangeAndPlaySfx(0); // 추가
        switch (currentIndex3)
        {
            case 0:
                itemtype = 0;
                foodtype = 0;
                foodQuantity = 1;
                foodPrice_A[foodtype] = 100;
                _Purchase_Food_Name_TMP.text = "기본먹이";
                Purchase_change_image.sprite = food_sprites[0];
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                _Purchase_Panel.SetActive(true);
                break;
            case 1:
                itemtype = 0;
                foodtype = 1;
                foodQuantity = 1;
                foodPrice_A[foodtype] = 250;
                _Purchase_Food_Name_TMP.text = "김치볶음밥";
                Purchase_change_image.sprite = food_sprites[1];
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                _Purchase_Panel.SetActive(true);
                break;
            case 2:
                itemtype = 0;
                foodtype = 2;
                foodQuantity = 1;
                foodPrice_A[foodtype] = 400;
                _Purchase_Food_Name_TMP.text = "바나나우유";
                Purchase_change_image.sprite = food_sprites[2];
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                _Purchase_Panel.SetActive(true);
                break;
            case 3:
                itemtype = 0;
                foodtype = 3;
                foodQuantity = 1;
                foodPrice_A[foodtype] = 1500;
                _Purchase_Food_Name_TMP.text = "핫케이크";
                Purchase_change_image.sprite = food_sprites[3];
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                _Purchase_Panel.SetActive(true);
                break;
        }

    }


    


   
    

    /*
    public void Interior1_Btn()
    {
        itemtype = 1;
        interiorType = 0;
        interiorQuantity = 1;
        interiorPrice_A[interiorType] = 200;
        _Quantity_count.text = interiorQuantity.ToString();
        _Price_count.text = interiorPrice_A[interiorType].ToString();
        _Purchase_Panel.SetActive(true);

    }
    public void Interior2_Btn()
    {
        itemtype = 1;
        interiorType = 1;
        interiorQuantity = 1;
        interiorPrice_A[interiorType] = 200;
        _Quantity_count.text = interiorQuantity.ToString();
        _Price_count.text = interiorPrice_A[interiorType].ToString();
        _Purchase_Panel.SetActive(true);

    }
    public void Interior3_Btn()
    {
        itemtype = 1;
        interiorType = 2;
        interiorQuantity = 1;
        interiorPrice_A[interiorType] = 200;
        _Quantity_count.text = interiorQuantity.ToString();
        _Price_count.text = interiorPrice_A[interiorType].ToString();
        _Purchase_Panel.SetActive(true);

    }
    public void Interior4_Btn()
    {
        itemtype = 1;
        interiorType = 3;
        interiorQuantity = 1;
        interiorPrice_A[interiorType] = 200;
        _Quantity_count.text = interiorQuantity.ToString();
        _Price_count.text = interiorPrice_A[interiorType].ToString();
        _Purchase_Panel.SetActive(true);

    }
    */
    
    //-------------------------------------------- 결제창-----------------------------------------------------------------
    // 수량(+) 버튼
    public void Quantity_Up_Btn()
    {
        GameManager._instance.soundManager.ChangeAndPlaySfx(0); // 추가
        // 먹이
        switch (foodtype)
        {
            case 0:
                foodQuantity += 1;
                foodPrice_A[foodtype] += 100;
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                 
                break;
            case 1:
                foodQuantity += 1;
                foodPrice_A[foodtype] += 250;
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                break;
            case 2:
                foodQuantity += 1;
                foodPrice_A[foodtype] += 400;
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                break;
            case 3:
                foodQuantity += 1;
                foodPrice_A[foodtype] += 1500;
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                break;
            case 4:
                foodQuantity += 1;
                foodPrice_A[foodtype] += 300;
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                break;
        }
       
        //인테리어
        /*
        switch (interiorType)
        {
            case 0:
                interiorQuantity += 1;
                interiorPrice_A[interiorType] += 100;
                _Quantity_count.text = interiorQuantity.ToString();
                _Price_count.text = interiorPrice_A[interiorType].ToString();

                break;
            case 1:
                interiorQuantity += 1;
                interiorPrice_A[interiorType] += 200;
                _Quantity_count.text = interiorQuantity.ToString();
                _Price_count.text = interiorPrice_A[interiorType].ToString();
                break;
            case 2:
                interiorQuantity += 1;
                interiorPrice_A[interiorType] += 200;
                _Quantity_count.text = interiorQuantity.ToString();
                _Price_count.text = interiorPrice_A[interiorType].ToString();
                break;
            case 3:
                interiorQuantity += 1;
                interiorPrice_A[interiorType] += 200;
                _Quantity_count.text = interiorQuantity.ToString();
                _Price_count.text = interiorPrice_A[interiorType].ToString();
                break;
        }
        */
    }

    // 수량(-)버튼
    public void Quantity_Down_Btn()
    {
        GameManager._instance.soundManager.ChangeAndPlaySfx(0); // 추가
        // 먹이
        if (foodPrice_A[0] > 100 && foodQuantity > 1 && foodtype == 0)
        {
            foodQuantity -= 1;
            foodPrice_A[foodtype] -= 100;
            _Quantity_count.text = foodQuantity.ToString();
            _Price_count.text = foodPrice_A[foodtype].ToString();
            
        }
        if (foodPrice_A[1] > 250 && foodQuantity > 1 && foodtype == 1)
        {
            foodQuantity -= 1;
            foodPrice_A[foodtype] -= 250;
            _Quantity_count.text = foodQuantity.ToString();
            _Price_count.text = foodPrice_A[foodtype].ToString();
        }
        if (foodPrice_A[2] > 400 && foodQuantity > 1 && foodtype == 2)
        {
            foodQuantity -= 1;
            foodPrice_A[foodtype] -= 400;
            _Quantity_count.text = foodQuantity.ToString();
            _Price_count.text = foodPrice_A[foodtype].ToString();
        }
        if (foodPrice_A[3] > 1500 && foodQuantity > 1 && foodtype == 3)
        {
            foodQuantity -= 1;
            foodPrice_A[foodtype] -= 1500;
            _Quantity_count.text = foodQuantity.ToString();
            _Price_count.text = foodPrice_A[foodtype].ToString();
        }
        if (foodPrice_A[4] > 300 && foodQuantity > 1 && foodtype == 4)
        {
            foodQuantity -= 1;
            foodPrice_A[foodtype] -= 300;
            _Quantity_count.text = foodQuantity.ToString();
            _Price_count.text = foodPrice_A[foodtype].ToString();
        }
        
        //인테리어
        /*
        else if (interiorPrice_A[0] > 100 && interiorQuantity > 1 && interiorType == 0)
        {
            interiorQuantity -= 1;
            interiorPrice_A[interiorType] -= 100;
            _Quantity_count.text = interiorQuantity.ToString();
            _Price_count.text = interiorPrice_A[interiorType].ToString();

        }
        if (interiorPrice_A[1] > 200 && interiorQuantity > 1 && interiorType == 1)
        {
            interiorQuantity -= 1;
            interiorPrice_A[interiorType] -= 200;
            _Quantity_count.text = interiorQuantity.ToString();
            _Price_count.text = interiorPrice_A[interiorType].ToString();
        }
        if (interiorPrice_A[2] > 200 && interiorQuantity > 1 && interiorType == 2)
        {
            interiorQuantity -= 1;
            interiorPrice_A[interiorType] -= 200;
            _Quantity_count.text = interiorQuantity.ToString();
            _Price_count.text = interiorPrice_A[interiorType].ToString();
        }
        if (interiorPrice_A[3] > 200 && interiorQuantity > 1 && interiorType == 3)
        {
            interiorQuantity -= 1;
            interiorPrice_A[interiorType] -= 200;
            _Quantity_count.text = interiorQuantity.ToString();
            _Price_count.text = interiorPrice_A[interiorType].ToString();
        }
        */
        
    }

    

    // 결제창 확인 버튼
    public void Purchase_Ok_Btn()
    {
        if (itemtype == 0)
        {
            foodPrice = foodPrice_A[foodtype];
            if (foodPrice > GameManager._instance.coin)
            {
                // 돈 부족
                StartCoroutine(GameManager._instance.uiManager.GameInfoMessage("코인이 부족합니다."));
            }
            else if (foodPrice <= GameManager._instance.coin)
            {
                GameManager._instance.foodCount[foodtype] += foodQuantity;
                GameManager._instance.coin -= foodPrice;
                _Purchase_Panel.SetActive(false);
                GameManager._instance.uiManager.shopPanel.SetActive(false);
                GameManager._instance.uiManager.isPopUp = false;
                GameManager._instance.uiManager.SetCurGameCoinText();
                GameManager._instance.soundManager.ChangeAndPlaySfx(3); // 추가
            }
        }
        // 인테리어
        /*
        else if (itemtype == 1)
        {
            var interioGet = InterioGet();
            Debug.Log($"������Ÿ��: {itemtype}, ����: {interioGet.Item1}, ����: {interioGet.Item2}, ����: {interioGet.Item3}");
            _Purchase_Panel.SetActive(false);
        }
        */
    }

    // 결제창 취소 버튼
    public void Purchase_Cancel_Btn()
    {
        _Purchase_Panel.SetActive(false);
        GameManager._instance.soundManager.ChangeAndPlaySfx(0); // 추가
    }


    //반환함수 ---------------------------------------------------------------------------------------------------------------------------------------
    /*
    // 먹이 반환
    public(int, int, int) FoodGet()
    {
        
        foodPrice = foodPrice_A[foodtype];
        

        return (foodtype, foodQuantity, foodPrice);
    }

    // 인테리어 반환
    public(int,int,int) InterioGet()
    {
        interioPrice = interiorPrice_A[interiorType];
        return (interiorType, interiorQuantity, interioPrice);
    }
    */



}