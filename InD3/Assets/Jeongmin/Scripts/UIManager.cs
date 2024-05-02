using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text _score;
    public TMP_Text _finalScore;
    public GameObject[] _life;
    public Image _timer;
    public GameObject[] _levelCircles;

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < _levelCircles.Length; i++)
        {
            _levelCircles[i].SetActive(false);
        }
    }

    public void UpdateTimer(float fillAmount)
    {
        _timer.fillAmount = fillAmount;
    }

    public void ShowFinalScore(int score)
    {
        Debug.Log("ShowFinalScore called with score: " + score); // 로그 추가
        _finalScore.text = "보상 : " + score.ToString() + "코인";
    }

    void OnEnable()
    {
        CircleManager.Instance.OnScoreChange += OnScoreChange;
        CircleManager.Instance.OnLifeChange += OnLifeChange;
        CircleManager.Instance.OnLevelChange += OnLevelChange;
    }

    void OnDisable()
    {
        CircleManager.Instance.OnScoreChange -= OnScoreChange;
        CircleManager.Instance.OnLifeChange -= OnLifeChange;
        CircleManager.Instance.OnLevelChange -= OnLevelChange;
    }
    
    void OnScoreChange(int score)
    {
        _score.text = score.ToString();
    }

    void OnLifeChange(int life)
    {
        for (int i = 0; i < _life.Length; i++)
        {
            _life[i].SetActive(i < life);
        }
    }

    void OnLevelChange(int level)
    {
        switch (level)
        {
            case 1:
                _levelCircles[0].SetActive(true);
                _levelCircles[1].SetActive(true);
                break;
            case 2:
                _levelCircles[2].SetActive(true);
                _levelCircles[3].SetActive(true);
                break;
            case 3:
                _levelCircles[4].SetActive(true);
                _levelCircles[5].SetActive(true);
                break;
        }
    }
}
