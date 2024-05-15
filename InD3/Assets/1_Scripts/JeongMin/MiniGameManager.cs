using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    public GameObject _playerNamePanel;
    public GameObject _monsterNamePanel;


    void Awake()
    {
        Instance = this;
    }
    
    
    public void Save()
    {
        TMP_InputField inputField;

        if (_playerNamePanel.activeSelf)
        {
            inputField = _playerNamePanel.GetComponentInChildren<TMP_InputField>();
            PlayerPrefs.SetString("PlayerName", inputField.text);

            _playerNamePanel.SetActive(false);
            GameManager._instance.uiManager.gameIntroScene.SetActive(false);
            GameManager._instance.uiManager.GoRoom();
        }
        else if (_monsterNamePanel.activeSelf)
        {
            inputField = _monsterNamePanel.GetComponentInChildren<TMP_InputField>();
            PlayerPrefs.SetString("MonsterName", inputField.text);

            _monsterNamePanel.SetActive(false);
        }
    }
}
