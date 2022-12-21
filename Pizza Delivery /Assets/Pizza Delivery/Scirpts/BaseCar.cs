using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCar : MonoBehaviour,IMovable
{
    private Rigidbody _carRigidbody;
    private Vector3 _carStartPosition;

    private float _carSpeed = 20;
    private float _carresetTime = 2;
    void Start()
    {
        _carRigidbody = gameObject.GetComponent<Rigidbody>();
        _carStartPosition = transform.localPosition;
       
    }
    public virtual void Drive(float carSpeed)
    {
       
        _carRigidbody.velocity = Vector3.forward * carSpeed;
        //transform.Translate(Vector3.forward);
    }

    public void Move()
    {
        Drive(_carSpeed);
        StartCoroutine(ResetPositionDelay());
        //throw new System.NotImplementedException();
    }
    public void ResetPosition()
    {
        transform.localPosition = _carStartPosition;
    }
    IEnumerator ResetPositionDelay()
    {
        yield return new WaitForSeconds(_carresetTime);
        gameObject.SetActive(false);
        ResetPosition();
        gameObject.SetActive(true);
    }

}
