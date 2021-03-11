using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird
{
    private Vector2 _position;
    public Vector2 Position { get { return _position; } }

    private float _size;

    private float _jumpTimeSec;

    private float _jumpEnd;

    private bool _inJump;
    public bool InJump { get { return _inJump; } }

    private float _speed;

    private float _jumpSpeed;

    public Bird(float size = 2.5f, float jumpTimeSec = 0.5f, float speed = 0.1f, float jumpSpeed = 0.1f)
    {
        _position = new Vector2(0f, 3.5f);
        _size = size;
        _jumpTimeSec = jumpTimeSec;
        _inJump = false;
        _speed = speed;
        _jumpSpeed = jumpSpeed;
    }

    public void Update()
    {
        if(_inJump && Time.time >= _jumpEnd)
        {
            _inJump = false;
        }

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
        _jumpEnd = Time.time + _jumpTimeSec;
    }


}
