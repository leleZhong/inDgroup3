using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject _playerNamePanel;
    public GameObject _monsterNamePanel;


    void Awake()
    {
        Instance = this;

        _playerNamePanel.SetActive(true);
        _monsterNamePanel.SetActive(false);
    }
    
    public void Save()
    {
        TMP_InputField inputField;

        if (_playerNamePanel.activeSelf)
        {
            inputField = _playerNamePanel.GetComponentInChildren<TMP_InputField>();
            PlayerPrefs.SetString("PlayerName", inputField.text);

            _playerNamePanel.SetActive(false);
            _monsterNamePanel.SetActive(true);
        }
        else if (_monsterNamePanel.activeSelf)
        {
            inputField = _monsterNamePanel.GetComponentInChildren<TMP_InputField>();
            PlayerPrefs.SetString("MonsterName", inputField.text);

            _monsterNamePanel.SetActive(false);
        }
    }
}
