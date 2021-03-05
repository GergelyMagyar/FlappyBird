using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird
{
    private Vector2 _position;

    private float _size;

    private bool _dead;

    private float _jumpTimeSec;

    private float _countDown;

    private bool _inJump;

    private float _speed;

    private float _jumpSpeed;

    public Bird(float size = 2.5f, float jumpTimeSec = 0.5f, float speed = 0.1f, float jumpSpeed = 0.1f)
    {
        _position = new Vector2(0f, 0f);
        _size = size;
        _dead = false;
        _jumpTimeSec = jumpTimeSec;
        _countDown = 0f;
        _inJump = false;
        _speed = speed;
        _jumpSpeed = jumpSpeed;
    }

    public void Update()
    {
        //cooldown

        Vector2 movement;

        if(_inJump)
        {
            movement = new Vector2(_speed, _jumpSpeed);
        }
        else
        {
            movement = new Vector2(_speed, -_jumpSpeed);
        }

        _position += movement;
    }

    public void Jump()
    {
        _inJump = true;
        _countDown = _jumpTimeSec;
    }


}
