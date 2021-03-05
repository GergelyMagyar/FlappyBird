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

    public Bird(float size = 2.5f, float jumpTimeSec = 0.5f)
    {
        _position = new Vector2(0, 0);
        _size = size;
        _dead = false;
        _jumpTimeSec = jumpTimeSec;
        _countDown = 0f;
        _inJump = false;
    }

    public void Update()
    {

    }

    public void Jump()
    {
        _inJump = true;
        _countDown = _jumpTimeSec;
    }


}
