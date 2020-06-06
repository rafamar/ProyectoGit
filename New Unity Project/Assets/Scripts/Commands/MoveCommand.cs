using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Transform _player;
    private float _speed;
    private Vector3 _vectorTranslation;
    private Vector3 _previousPosition;

    public MoveCommand(Transform player, float speed, Vector3 vector)
    {
        this._player = player;
        this._speed = speed;
        this._vectorTranslation = vector;
        this._previousPosition = player.position;
    }

    public void Execute()
    {
        _player.Translate(_vectorTranslation * _speed * Time.deltaTime);

        Debug.Log("Translate to " + _vectorTranslation.ToString());
    }

    public void Undue()
    {
        _player.position = _previousPosition;
    }
}
