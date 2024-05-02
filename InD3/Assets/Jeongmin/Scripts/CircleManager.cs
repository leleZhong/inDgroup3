using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class CircleManager : MonoBehaviour
{
    public static CircleManager Instance;

    public GameObject[] _circles;
    public Button _leftButton;
    public Button _rightButton;
    public Transform[] _position;
    public float _speed;
    Queue<GameObject> _circleQueue = new Queue<GameObject>();

    // UI
    public GameObject _gameOverPanel;
    int _score;
    public event Action<int> OnScoreChange;
    int _life;
    public event Action<int> OnLifeChange;

    int _level = 1;
    public event Action<int> OnLevelChange;

    float _timeLimit = 20f; // 초기 제한 시간 설정
    float _timeRemaining;   // 남은 시간

    void Awake()
    {
        Instance = this;

        _gameOverPanel.SetActive(false);
        _score = 0;
        _life = 3;
        _timeRemaining = _timeLimit;

        // // 처음에 5개의 오브젝트를 큐에 추가
        // _circleQueue.Enqueue(Instantiate(_circles[0]));
        // _circleQueue.Enqueue(Instantiate(_circles[1]));
        // _circleQueue.Enqueue(Instantiate(_circles[1]));
        // _circleQueue.Enqueue(Instantiate(_circles[0]));
        // _circleQueue.Enqueue(Instantiate(_circles[0]));

        // 초기 원소 추가
        AddInitialCircles();
    }

    void Start()
    {
        MovePosition();
        OnScoreChange?.Invoke(_score);
        OnLevelChange?.Invoke(_level); // 초기 레벨 알림
    }

    void Update()
    {
        _timeRemaining -= Time.deltaTime;
        UIManager.Instance.UpdateTimer(_timeRemaining / _timeLimit);
        
        if (_timeRemaining <= 0)
        {
            GameOver();
        }

    }

    void AddInitialCircles()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject newCircle = Instantiate(_circles[UnityEngine.Random.Range(0, _level * 2)]);
            _circleQueue.Enqueue(newCircle);
        }
    }

    void AddData()
    {
        // 새로운 원소를 랜덤으로 큐에 추가
        GameObject newCircle = Instantiate(_circles[UnityEngine.Random.Range(0, _level * 2)]);
        _circleQueue.Enqueue(newCircle);
    }

    void RemoveData()
    {
        // 큐의 첫번째 원소를 제거
        if (_circleQueue.Count > 0)
        {
            GameObject circleToRemove = _circleQueue.Dequeue();
            Destroy(circleToRemove);
        }
    }

    public void OnClickLeftButton()
    {
        if (_circleQueue.Peek().CompareTag("Left"))
        {
            RemoveData();
            AddData();
            MovePosition();
            AddScore();
        }
        else
        {
            ChangeLife();
        }
    }

    public void OnClickRightButton()
    {
        if (_circleQueue.Peek().CompareTag("Right"))
        {
            RemoveData();
            AddData();
            MovePosition();
            AddScore();
        }
        else
        {
            ChangeLife();
        }
    }

    void MovePosition()
    {
        int index = 0;
        foreach (GameObject obj in _circleQueue)
        {
            obj.transform.position = _position[index].position;
            index++;
        }
    }

    // 맞는 색일 때 점수 증가
    public void AddScore()
    {
        _score++;
        UpdateLevel();
        OnScoreChange?.Invoke(_score);
    }

    // 틀리면 라이프 감소
    public void ChangeLife()
    {
        _life--;
        if(_life <= 0)
        {
            OnLifeChange?.Invoke(_life);
            // 게임오버
            GameOver();
        }
        else
        {
            OnLifeChange?.Invoke(_life);
        }
    }

    void UpdateLevel()
    {
        int previousLevel = _level;
        if (_score < 10)
            _level = 1;
        else if (_score < 30)
            _level = 2;
        else
            _level = 3;

        if (_level != previousLevel)
        {
            _timeLimit = GetLevelTimeLimit(_level);
            _timeRemaining = _timeLimit;  // Reset timer with new level's time limit
            OnLevelChange?.Invoke(_level);
        }
    }

    float GetLevelTimeLimit(int level)
    {
        switch (level)
        {
            case 1: return 10f;
            case 2: return 15f;
            case 3: return 20f;
            default: return 10f;
        }
    }

    void GameOver()
    {
        _gameOverPanel.SetActive(true);
        int finalScore = CalculateFinalScore();
        UIManager.Instance.ShowFinalScore(finalScore);
    }

    int CalculateFinalScore()
    {
        return _score * _level;
    }
}
