using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Background _backgound;
    [SerializeField] private CircleSlider _circleSlider;
    [SerializeField] private RestartButton _restartButton;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private GameObject[] _car;
    private IMovable _movableObject;

    private float _deliveryTime;
    private float _deliveryDistance = 400;
    public bool isGameActive = false;
   

    private void FixedUpdate()
    {
        if (isGameActive)
        {
            _backgound.BackgroundMove(_circleSlider.PlayerSpeed());
            _timerText.text = _deliveryTime.ToString();
            DistanceCount();
            MoveViechale();
            IsTimeUp();
            
        }
      
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            GameOver();

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
            YouWin();
        }
        _distanceText.text = _deliveryDistance.ToString();
    }

    private void GameOver()
    {
        isGameActive = false;
        Destroy(gameObject);
        Destroy(_circleSlider.gameObject);

        _restartButton.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
    }
    private void YouWin()
    {
        _deliveryDistance = 0;
        _gameOverText.text = "YOU WIN";
        _restartButton.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }
    private void IsTimeUp()
    {
        if (_deliveryTime <= 0)
        {
            GameOver();
        }
    }
    private void MoveViechale()
    {
        foreach (GameObject car in _car)
        {
            _movableObject = car.GetComponent<IMovable>();
            _movableObject.Move();
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
