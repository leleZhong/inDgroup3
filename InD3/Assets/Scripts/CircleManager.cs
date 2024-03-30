using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    void Awake()
    {
        Instance = this;

        // 처음에 4개의 오브젝트를 큐에 추가
        _circleQueue.Enqueue(Instantiate(_circles[0]));
        _circleQueue.Enqueue(Instantiate(_circles[1]));
        _circleQueue.Enqueue(Instantiate(_circles[1]));
        _circleQueue.Enqueue(Instantiate(_circles[0]));
        _circleQueue.Enqueue(Instantiate(_circles[0]));
    }

    void Start()
    {
        MovePosition();
    }

    void Update()
    {
        
    }

    void AddData()
    {
        // 새로운 원소를 랜덤으로 큐에 추가
        GameObject newCircle = Instantiate(_circles[Random.Range(0, _circles.Length)]);
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
        RemoveData();
        AddData();
        MovePosition();
    }

    public void OnClickRightButton()
    {
        RemoveData();
        AddData();
        MovePosition();
    }

    void MovePosition()
    {
        int index = 0;
        foreach (GameObject obj in _circleQueue)
        {
            obj.transform.position = Vector2.Lerp(obj.transform.position, _position[index].position, _speed);
            index++;
        }
    }
}
