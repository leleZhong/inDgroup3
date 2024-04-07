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

    void Awake()
    {
        Instance = this;

        _gameOverPanel.SetActive(false);
        _score = 0;
        _life = 3;

        // 처음에 5개의 오브젝트를 큐에 추가
        _circleQueue.Enqueue(Instantiate(_circles[0]));
        _circleQueue.Enqueue(Instantiate(_circles[1]));
        _circleQueue.Enqueue(Instantiate(_circles[1]));
        _circleQueue.Enqueue(Instantiate(_circles[0]));
        _circleQueue.Enqueue(Instantiate(_circles[0]));
    }

    void Start()
    {
        MovePosition();
        OnScoreChange?.Invoke(_score);
    }

    void AddData()
    {
        // 새로운 원소를 랜덤으로 큐에 추가
        GameObject newCircle = Instantiate(_circles[UnityEngine.Random.Range(0, _circles.Length)]);
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
        if (_circleQueue.Peek().CompareTag("Red"))
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
        if (_circleQueue.Peek().CompareTag("Blue"))
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
            _gameOverPanel.SetActive(true);
        }
        else
        {
            OnLifeChange?.Invoke(_life);
        }
    }

    // 시간 제한
}
