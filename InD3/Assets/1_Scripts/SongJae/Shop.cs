using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    //변수 선언
    static int itemtype = 100;                                              // 아이템 종류(인테리어 OR 먹이 구분)
    static int[] foodPrice_A = new int[] { 100, 200, 200, 200, 300 };      // 먹이 아이템 가격 (인덱스 순서대로 기본,김볶,바나나우유,팬케잌)
    static int foodtype = 100;                                              // 먹이 종류
    static int foodQuantity = 0;                                           // 먹이 수량
    static int foodPrice = 0;                                              // 먹이 총 가격
    static int[] interiorPrice_A = new int[] { 100, 200, 200, 200 };       // 인테리어 가격
    static int interiorType = 100;                                          // 인테리어 종류
    static int interiorQuantity = 0;                                        // 인테리어 수량
    static int interioPrice = 0;                                            // 인테리어 총 가격





    // 사용할 오브젝트
    public GameObject _Shop_Panel;                                          // shop 패널
    public GameObject _ShopFood;                                            // food탭 패널
    public GameObject _Shopinterior;                                        // interio탭 패널
    public GameObject _Purchase_Panel;                                      // 결제창 패널
    //결제창에서 가격,수량 텍스트
    public TMP_Text _Price_count;                                           // 가격 텍스트
    public TMP_Text _Quantity_count;                                        // 수량 텍스트






    // 메인화면에서 상점 메뉴 버튼 , 상점 닫기 버튼, 먹이탭 버튼, 인테리어탭 버튼 
    // 메인화면에서 상점 메뉴 버튼
    public void Main_Shop_Btn()
    {
        _Shop_Panel.SetActive(true);
        
    }
    // 상점 닫기 버튼(X버튼)
    public void ShopCancel_Btn()
    {
        _Shop_Panel.SetActive(false);
    }
    // 먹이탭 버튼
    public void ShopFood_Btn()
    {
        _Shopinterior.SetActive(false);
        _ShopFood.SetActive(true);
    }
    //인테리어탭 버튼
    public void Shopinterior_Btn()
    {
        _ShopFood.SetActive(false);
        _Shopinterior.SetActive(true);
    }


    // 먹이 탭에서 각각의 먹이 버튼(선택)--------------------------------------------------------------------------------------------------------------------
    public void Food_Basic_Btn()
    {
        itemtype = 0;
        foodtype = 0;
        foodQuantity = 1;
        foodPrice_A[foodtype] = 100;
        _Quantity_count.text = foodQuantity.ToString();
        _Price_count.text = foodPrice_A[foodtype].ToString();
        _Purchase_Panel.SetActive(true);
        
    }

    public void Food_Satiety_Btn()
    {
        itemtype = 0;
        foodtype = 1;
        foodQuantity = 1;
        foodPrice_A[foodtype] = 200;
        _Quantity_count.text = foodQuantity.ToString();
        _Price_count.text = foodPrice_A[foodtype].ToString();
        _Purchase_Panel.SetActive(true);
        
    }

    public void Food_Cleanliness_Btn()
    {
        itemtype = 0;
        foodtype = 2;
        foodQuantity = 1;
        foodPrice_A[foodtype] = 200;
        _Quantity_count.text = foodQuantity.ToString();
        _Price_count.text = foodPrice_A[foodtype].ToString();
        _Purchase_Panel.SetActive(true);
        
    }

    public void Food_Affection_Btn()
    {
        itemtype = 0;
        foodtype = 3;
        foodQuantity = 1;
        foodPrice_A[foodtype] = 200;
        _Quantity_count.text = foodQuantity.ToString();
        _Price_count.text = foodPrice_A[foodtype].ToString();
        _Purchase_Panel.SetActive(true);
       
    }

    public void Food_Special_Btn()
    {
        itemtype = 0;
        foodtype = 4;
        foodQuantity = 1;
        foodPrice_A[foodtype] = 300;
        _Quantity_count.text = foodQuantity.ToString();
        _Price_count.text = foodPrice_A[foodtype].ToString();
        _Purchase_Panel.SetActive(true);
    }

    // 인테리어 탭에서 각각의 인테리어 버튼(선택)----------------------------------------------------------------------------------------------------------------
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

    // 결제창 ---------------------------------------------------------------------------------------------------------------------------------------

    // 수량 업 버튼
    public void Quantity_Up_Btn()
    {
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
                foodPrice_A[foodtype] += 200;
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                break;
            case 2:
                foodQuantity += 1;
                foodPrice_A[foodtype] += 200;
                _Quantity_count.text = foodQuantity.ToString();
                _Price_count.text = foodPrice_A[foodtype].ToString();
                break;
            case 3:
                foodQuantity += 1;
                foodPrice_A[foodtype] += 200;
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
        // 인테리어
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
    }

    // 수량 다운 버튼
    public void Quantity_Down_Btn()
    {
        // 먹이
        if (foodPrice_A[0] > 100 && foodQuantity > 1 && foodtype == 0)
        {
            foodQuantity -= 1;
            foodPrice_A[foodtype] -= 100;
            _Quantity_count.text = foodQuantity.ToString();
            _Price_count.text = foodPrice_A[foodtype].ToString();
            
        }
        if (foodPrice_A[1] > 200 && foodQuantity > 1 && foodtype == 1)
        {
            foodQuantity -= 1;
            foodPrice_A[foodtype] -= 200;
            _Quantity_count.text = foodQuantity.ToString();
            _Price_count.text = foodPrice_A[foodtype].ToString();
        }
        if (foodPrice_A[2] > 200 && foodQuantity > 1 && foodtype == 2)
        {
            foodQuantity -= 1;
            foodPrice_A[foodtype] -= 200;
            _Quantity_count.text = foodQuantity.ToString();
            _Price_count.text = foodPrice_A[foodtype].ToString();
        }
        if (foodPrice_A[3] > 200 && foodQuantity > 1 && foodtype == 3)
        {
            foodQuantity -= 1;
            foodPrice_A[foodtype] -= 200;
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
        
        // 인테리어
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
        
    }

    // 결제 확인 버튼
    public void Purchase_Ok_Btn()
    {
        if (itemtype == 0)
        {
            var result = FoodGet();
            Debug.Log($"아이템타입: {itemtype}, 종류: {result.Item1}, 수량: {result.Item2}, 가격: {result.Item3}");
        }
        else if (itemtype == 1)
        {
            var interioGet = InterioGet();
            Debug.Log($"아이템타입: {itemtype}, 종류: {interioGet.Item1}, 수량: {interioGet.Item2}, 가격: {interioGet.Item3}");
        }
       
        _Purchase_Panel.SetActive(false);
    }

    // 결제창 취소 버튼
    public void Purchase_Cancel_Btn()
    {
        
        _Purchase_Panel.SetActive(false);

    }


    //반환 함수 ---------------------------------------------------------------------------------------------------------------------------------------

    // 먹이 반환 함수
    public(int, int, int) FoodGet()
    {
        
        foodPrice = foodPrice_A[foodtype];
        

        return (foodtype, foodQuantity, foodPrice);
    }

    // 인테리어 반환 함수
    public(int,int,int) InterioGet()
    {
        interioPrice = interiorPrice_A[interiorType];
        return (interiorType, interiorQuantity, interioPrice);
    }




}