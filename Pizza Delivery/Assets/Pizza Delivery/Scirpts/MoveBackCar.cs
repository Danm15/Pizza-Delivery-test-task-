using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackCar : BaseCar
{
    public override void Drive(float carspeed)
    {
        base.Drive(-20);
    }
}
