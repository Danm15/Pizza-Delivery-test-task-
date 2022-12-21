
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private MoveForvardCar car;
    private Vector3 _startPos;
    private float _backgroundWidth = 108f;

    void Start()
    {
        _startPos = transform.position;
    }
    
    void Update()
    {
        ResetBackgroundPosition();
    }

    public void BackgroundMove(float backgroundSpeed)
    {
        transform.Translate(Vector3.right * backgroundSpeed);
    }

    private void ResetBackgroundPosition()
    {
        if (transform.position.x > _startPos.x + _backgroundWidth)
        {
            transform.position = _startPos;
            
        }
    }
    
}
