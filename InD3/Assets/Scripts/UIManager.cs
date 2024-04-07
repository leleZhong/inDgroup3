using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text _score;
    public GameObject[] _life;
    public Image _timer;

    void Update()
    {
        _timer.fillAmount -= Time.deltaTime / 10;

        if (_timer.fillAmount <= 0)
        {
            CircleManager.Instance._gameOverPanel.SetActive(true);
        }
    }

    void OnEnable()
    {
        CircleManager.Instance.OnScoreChange += OnScoreChange;
        CircleManager.Instance.OnLifeChange += OnLifeChange;
    }

    void OnDisable()
    {
        CircleManager.Instance.OnScoreChange -= OnScoreChange;
        CircleManager.Instance.OnLifeChange -= OnLifeChange;
    }
    
    void OnScoreChange(int score)
    {
        _score.text = score.ToString();
    }

    void OnLifeChange(int life)
    {
        for (int i = 0; i < _life.Length; i++)
        {
            if (i < life)
            {
                _life[i].SetActive(true);
            }
            else
            {
                _life[i].SetActive(false);
            }
        }
    }
}
