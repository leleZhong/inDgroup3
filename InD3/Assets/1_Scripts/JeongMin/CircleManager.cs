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
    public GameObject startBtn;
    public Transform[] _position;
    public float _speed;
    Queue<GameObject> _circleQueue = new Queue<GameObject>();

    // UI
    public GameObject _gameOverPanel;
    int _score;
    public event Action<int> OnScoreChange;
    int _life;
    public event Action<int> OnLifeChange;

    int _level;
    public event Action<int> OnLevelChange;

    float _timeLimit = 10f; // 초기 제한 시간 설정
    float _timeRemaining;   // 남은 시간

    private int finalScore;




    void Update()
    {
        if(GameManager._instance.uiManager.isCountDown)
            return;

        if (GameManager._instance.uiManager.isMiniGameStart && !_gameOverPanel.activeSelf )
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
                MiniUIManager.Instance.UpdateTimer(_timeRemaining / _timeLimit);
            }
            if (_timeRemaining <= 0)
            {
                _timeRemaining = 0;
                UpdateLevel();
            }
        }

    }

    private void OnEnable()
    {
        Instance = this;
    }

    public void GameStart()
    {

        _gameOverPanel.SetActive(false);
        _score = 0;
        _life = 3;
        _timeRemaining = _timeLimit;
        _level = 1;

        // // 처음에 5개의 오브젝트를 큐에 추가
        // _circleQueue.Enqueue(Instantiate(_circles[0]));
        // _circleQueue.Enqueue(Instantiate(_circles[1]));
        // _circleQueue.Enqueue(Instantiate(_circles[1]));
        // _circleQueue.Enqueue(Instantiate(_circles[0]));
        // _circleQueue.Enqueue(Instantiate(_circles[0]));
        
        OnScoreChange?.Invoke(_score);
        OnLevelChange?.Invoke(_level); // 초기 레벨 알림
        
        startBtn.SetActive(true);
    }
    public void MiniGameStart()
    {
        startBtn.SetActive(false);
        StartCoroutine(GameManager._instance.uiManager.GameCountDown());
        GameManager._instance.uiManager.isMiniGameStart = true;
        
        // 초기 원소 추가
        AddInitialCircles();
        MovePosition();
        GameManager._instance.soundManager.ChangeBGM(3); // 추가
        GameManager._instance.soundManager.ChangeAndPlaySfx(0); // 추가
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

    IEnumerator MoveAndRemove(GameObject circleToRemove, Vector3 direction, float moveTime)
    {
        float startTime = Time.time;
        Vector3 startPostion = circleToRemove.transform.position;

        while (Time.time - startTime < moveTime)
        {
            circleToRemove.transform.position = Vector3.Lerp(startPostion, startPostion + direction, (Time.time - startTime) / moveTime);
            yield return null;
        }

        Destroy(circleToRemove);
    }

    void RemoveData(Vector3 direction)
    {
        // 큐의 첫번째 원소를 제거
        if (_circleQueue.Count > 0)
        {
            GameObject circleToRemove = _circleQueue.Dequeue();
            // 원하는 이동 시간을 설정합니다. (예: 1초 동안 이동)
            float moveTime = 0.1f;
            StartCoroutine(MoveAndRemove(circleToRemove, direction, moveTime));
        }
    }

    public void OnClickLeftButton()
    {
        if (_gameOverPanel.activeSelf)
        {
            return;
        }

        if (_circleQueue.Peek().CompareTag("Left"))
        {
            RemoveData(new Vector3(-1, 0, 0));
            AddData();
            MovePosition();
            AddScore();
            GameManager._instance.soundManager.ChangeAndPlaySfx(4); // 추가
        }
        else
        {
            ChangeLife();
            GameManager._instance.soundManager.ChangeAndPlaySfx(5); // 추가
        }
    }

    public void OnClickRightButton()
    {
        if (_gameOverPanel.activeSelf)
        {
            return;
        }
        
        if (_circleQueue.Peek().CompareTag("Right"))
        {
            RemoveData(new Vector3(1, 0, 0));
            AddData();
            MovePosition();
            AddScore();
            GameManager._instance.soundManager.ChangeAndPlaySfx(4); // 추가
        }
        else
        {
            ChangeLife();
            GameManager._instance.soundManager.ChangeAndPlaySfx(5); // 추가
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

        if (_level == 1 && _score >= 10)
            _level = 2;
        else if (_level == 2 && _score >= 30)
            _level = 3;
        else if (_timeRemaining <= 0) // 시간 초과인데 점수가 다음 레벨 기준에 도달하지 못한 경우
        {
            GameOver();
            return; // 게임 오버가 호출되면 이후 코드를 실행하지 않도록 리턴
        }

        if (_level != previousLevel)
        {
            _timeLimit = GetLevelTimeLimit(_level);
            _timeRemaining = _timeLimit;  // Reset timer with new level's time limit
            OnLevelChange?.Invoke(_level);
            GameManager._instance.soundManager.ChangeAndPlaySfx(1); // 추가
        }
    }

    float GetLevelTimeLimit(int level)
    {
        switch (level)
        {
            case 1: return 10f;
            case 2: return 15f;
            case 3: return 15f;
            default: return 5f;
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver called"); // 로그 추가
        GameManager._instance.soundManager.ChangeAndPlaySfx(1); // 게임 오버 사운드를 재생
        _gameOverPanel.SetActive(true);
        finalScore = CalculateFinalScore();
        MiniUIManager.Instance.ShowFinalScore(finalScore);
        ClearCircles();
    }

    // 큐를 비우고 서클을 파괴하는 메서드
    void ClearCircles()
    {
        while (_circleQueue.Count > 0)
        {
            GameObject circle = _circleQueue.Dequeue();
            Destroy(circle);
        }
    }

    public void GoRoomScene()
    {
        GameManager._instance.coin += finalScore;
        GameManager._instance.uiManager.miniGameScene.SetActive(false);
        GameManager._instance.uiManager.SetCurGameCoinText();
        GameManager._instance.uiManager.isPopUp = false;
        GameManager._instance.soundManager.ChangeAndPlaySfx(3); // 추가
        GameManager._instance.soundManager.ChangeBGM(0); // 추가
    }

    int CalculateFinalScore()
    {
        return _score * _level;
    }
}
