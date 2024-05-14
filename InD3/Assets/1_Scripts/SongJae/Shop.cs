using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    //���� ����
    static int itemtype = 100;                                              // 0 = 먹이, 1 = 가구
    static int[] foodPrice_A = new int[] { 100, 200, 200, 200, 300 };      // 먹이 4개
    static int foodtype = 100;                                              // 먹이 타입 0~3
    static int foodQuantity = 0;                                           // 아이템 수량 담긴 변수
    static int foodPrice = 0;                                              // 먹이 가격
    static int[] interiorPrice_A = new int[] { 100, 200, 200, 200 };      // 
    static int interiorType = 100;                                         
    static int interiorQuantity = 0;                                       
    static int interioPrice = 0;                                           





    // ����� ������Ʈ
    public GameObject _Shop_Panel;                                          // shop �г�
    public GameObject _ShopFood;                                            // food�� �г�
    public GameObject _Shopinterior;                                        // interio�� �г�
    public GameObject _Purchase_Panel;                                      // ����â �г�
    //����â���� ����,���� �ؽ�Ʈ
    public TMP_Text _Price_count;                                           // ���� �ؽ�Ʈ
    public TMP_Text _Quantity_count;                                        // ���� �ؽ�Ʈ






    // ����ȭ�鿡�� ���� �޴� ��ư , ���� �ݱ� ��ư, ������ ��ư, ���׸����� ��ư 
    // ����ȭ�鿡�� ���� �޴� ��ư
    public void Main_Shop_Btn()
    {
        _Shop_Panel.SetActive(true);
        
    }
    // ���� �ݱ� ��ư(X��ư)
    public void ShopCancel_Btn()
    {
        _Shop_Panel.SetActive(false);
    }
    // ������ ��ư
    public void ShopFood_Btn()
    {
        _Shopinterior.SetActive(false);
        _ShopFood.SetActive(true);
    }
    //���׸����� ��ư
    public void Shopinterior_Btn()
    {
        _ShopFood.SetActive(false);
        _Shopinterior.SetActive(true);
    }


    // ���� �ǿ��� ������ ���� ��ư(����)--------------------------------------------------------------------------------------------------------------------
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

    // ���׸��� �ǿ��� ������ ���׸��� ��ư(����)----------------------------------------------------------------------------------------------------------------
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

    // ����â ---------------------------------------------------------------------------------------------------------------------------------------

    // ���� �� ��ư
    public void Quantity_Up_Btn()
    {
        // ����
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
        // ���׸���
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

    // ���� �ٿ� ��ư
    public void Quantity_Down_Btn()
    {
        // ����
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
        
        // ���׸���
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

    // ���� Ȯ�� ��ư
    public void Purchase_Ok_Btn()
    {
        if (itemtype == 0)
        {
            var result = FoodGet();
            Debug.Log($"������Ÿ��: {itemtype}, ����: {result.Item1}, ����: {result.Item2}, ����: {result.Item3}");
        }
        else if (itemtype == 1)
        {
            var interioGet = InterioGet();
            Debug.Log($"������Ÿ��: {itemtype}, ����: {interioGet.Item1}, ����: {interioGet.Item2}, ����: {interioGet.Item3}");
        }
       
        _Purchase_Panel.SetActive(false);
    }

    // ����â ��� ��ư
    public void Purchase_Cancel_Btn()
    {
        
        _Purchase_Panel.SetActive(false);

    }


    //��ȯ �Լ� ---------------------------------------------------------------------------------------------------------------------------------------

    // ���� ��ȯ �Լ�
    public(int, int, int) FoodGet()
    {
        
        foodPrice = foodPrice_A[foodtype];
        

        return (foodtype, foodQuantity, foodPrice);
    }

    // ���׸��� ��ȯ �Լ�
    public(int,int,int) InterioGet()
    {
        interioPrice = interiorPrice_A[interiorType];
        return (interiorType, interiorQuantity, interioPrice);
    }




}