using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Background _backgound;
    [SerializeField] private CircleSlider _circleSlider;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private GameObject[] _car;
    private IMovable _movableObject;

    private float _deliveryTime;
    private float _deliveryDistance = 200;
    public bool isGameActive = false;
   

    private void FixedUpdate()
    {
        if (isGameActive)
        {
            _backgound.BackgroundMove(_circleSlider.PlayerSpeed());
            _timerText.text = _deliveryTime.ToString();
            DistanceCount();
            foreach (GameObject car in _car)
            {
                _movableObject = car.GetComponent<IMovable>();
                _movableObject.Move();
            }
           
            
            GameOver();
            
        }
      
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            isGameActive = false;
            Destroy(gameObject);
            Destroy(_circleSlider.gameObject);
            _gameOverText.gameObject.SetActive(true);

        }


    }

    public void StartGame(float deliveryTime = 20)
    {
        
        _circleSlider.StartRotationsPerSecondCoroutine();
       
        
       
        _deliveryTime = deliveryTime;
        isGameActive = true;
       
        StartCoroutine(Timer());

    }
    private void DistanceCount()
    {
        
        if(_deliveryDistance > 0)
        {
            _deliveryDistance -= _circleSlider.PlayerSpeed(); 
        }
        else 
        {
            _deliveryDistance = 0;
            _gameOverText.text = "YOU WIN";
            _gameOverText.gameObject.SetActive(true);
            isGameActive = false;
        }
        _distanceText.text = _deliveryDistance.ToString();
    }

    public void GameOver()
    {
        if (_deliveryTime <= 0)
        {
            isGameActive = false;
            Destroy(gameObject);
            Destroy(_circleSlider.gameObject);

            _gameOverText.gameObject.SetActive(true);

        }
    }
    
    IEnumerator Timer()
    {
        while (_deliveryTime > 0)
        {
            
            yield return new WaitForSeconds(1);
            _deliveryTime -= 1;
            
        }
        
    }
}
