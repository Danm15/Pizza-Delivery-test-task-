using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CircleSlider : MonoBehaviour
{
    [SerializeField] private Transform _handle;
    [SerializeField] private float _rotationsPerSecond;
    [SerializeField] private GameManager _gameManager;
    private Vector3 _mousePosition;
    private bool _isworking = true;
    private float _handleAngle;
    private float _readInputTime = 0.01f;
    private float _currentJostickAngle;
    private float _previousJostickAngle;
    private float _deltaAngle;
    private float _variableSpeed;


    

    public void onHandleDrag()
    {
        _mousePosition = Input.mousePosition;
        Vector2 direction = _mousePosition - _handle.position;
        _handleAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion handleRotation = Quaternion.AngleAxis(_handleAngle, Vector3.forward);
        _handle.rotation = handleRotation;
    }

    public float PlayerSpeed(float accelerationSpeedCoefficient = 200f,float slowdownSpeed = 1f, float slowdownValue = 0.002f)
    {
        if (_gameManager.isGameActive)
        {
            StartCoroutine(ResistanceSlowdown(slowdownSpeed, slowdownValue));
            _variableSpeed += _rotationsPerSecond / accelerationSpeedCoefficient;
            _variableSpeed = Mathf.Clamp01(_variableSpeed);
        }
        
        return _variableSpeed;
    }

    public float PedalSpeed(float accelerationSpeedCoefficient = 20f, float slowdownSpeed = 1f, float slowdownValue = 0.02f)
    {
        float k = 0;
        StartCoroutine(ResistanceSlowdown(slowdownSpeed, slowdownValue));
        k += _rotationsPerSecond / accelerationSpeedCoefficient;
        k = Mathf.Clamp(k, -1, 1);


        return k;
    }

    public void StartRotationsPerSecondCoroutine()
    {
        StartCoroutine(RotationsPerSecond());
    }

    IEnumerator ResistanceSlowdown(float slowdownSpeed,float slowdownValue)
    {

        if(_variableSpeed > 0)
        {
            yield return new WaitForSeconds(slowdownSpeed);
            _variableSpeed -= slowdownValue;
        }
        else
        {
            _variableSpeed = 0;
        }
     
    }

    IEnumerator RotationsPerSecond()
    {
        while (_isworking)
        {
            _previousJostickAngle = _handleAngle;
            yield return new WaitForSeconds(_readInputTime);
            _currentJostickAngle = _handleAngle;
            if (_previousJostickAngle <= 180 && _previousJostickAngle >= 90 && _currentJostickAngle >= -180 && _currentJostickAngle <= -90)
            {
                _deltaAngle = (_previousJostickAngle - _currentJostickAngle) - 360;
            }
            else if (_currentJostickAngle <= 180 && _currentJostickAngle >= 90 && _previousJostickAngle >= -180 && _previousJostickAngle <= -90)
            {
                _deltaAngle = (_previousJostickAngle - _currentJostickAngle) + 360;
            }
            else
            {
                _deltaAngle = _previousJostickAngle - _currentJostickAngle;
            }

            _rotationsPerSecond = _deltaAngle * 100 / 360;


        }

    }

}
