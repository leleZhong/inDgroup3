using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DokkaebiAI : MonoBehaviour
{
    public float speed;
    public float maxCoolTime;
    public float curCoolTime;

    private Vector2 _movePos;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private bool _isFlip;

    public SpriteRenderer spriteRndr;

    private void OnEnable()
    {
        curCoolTime = maxCoolTime;
        _movePos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        if (_movePos.x >= 0)
        {
            spriteRndr.flipX = false;
        }
        else
        {
            spriteRndr.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, 
            _movePos, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _movePos) < 0.2)
        {
            if (curCoolTime <= 0)
            {
                _movePos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                if (transform.position.x >= _movePos.x)
                {
                    spriteRndr.flipX = false;
                }
                else
                {
                    spriteRndr.flipX = true;
                }
                curCoolTime = maxCoolTime;
            }
            else
            {
                curCoolTime -= Time.deltaTime;
            }
        }
    }
}
