using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclingPhysics : MonoBehaviour
{
    [SerializeField] private CircleSlider _circleSlider;
    [SerializeField] private HingeJoint _pedalHinge;
    [SerializeField] private HingeJoint _backweelHinge;
    
    private float _pedalSpeedCoef = -2000f;
    private float _weelSpeedCoef = -200f;

    private void FixedUpdate()
    {
        CyclingPhysicsReturn();
    }

    private void CyclingPhysicsReturn()
    {
        JointMotor pedalMotor = _pedalHinge.motor;
        JointMotor backweelMotor = _backweelHinge.motor;
        pedalMotor = _pedalHinge.motor;
        backweelMotor = _pedalHinge.motor;

        if (_circleSlider.PlayerSpeed() > 0)
        {

            pedalMotor.targetVelocity = _circleSlider.PedalSpeed() * _pedalSpeedCoef;
            backweelMotor.targetVelocity = _circleSlider.PlayerSpeed() * _weelSpeedCoef;

        }
        else
        {

            pedalMotor.targetVelocity = 0f;
            backweelMotor.targetVelocity = 0f;


        }
        _pedalHinge.motor = pedalMotor;
        _backweelHinge.motor = backweelMotor;
    }
}
