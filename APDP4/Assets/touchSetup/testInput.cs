using UnityEngine;

public class testInput : MonoBehaviour
{
    private inputManager inputManager;

    private void Awake()
    {
        inputManager = GetComponent<inputManager>();
    }

    private void OnEnable()
    {
        inputManager.OnSwipeLeft += OnSwipeLeft;
        inputManager.OnSwipeRight += OnSwipeRight;
        inputManager.OnSwipeUp += OnSwipeUp;
        inputManager.OnSwipeDown += OnSwipeDown;
    }

    private void OnDisable()
    {
        inputManager.OnSwipeLeft -= OnSwipeLeft;
        inputManager.OnSwipeRight -= OnSwipeRight;
        inputManager.OnSwipeUp -= OnSwipeUp;
        inputManager.OnSwipeDown -= OnSwipeDown;
    }

    private void OnSwipeLeft() => Debug.Log("Left swipe detected");
    private void OnSwipeRight() => Debug.Log("Right swipe detected");
    private void OnSwipeUp() => Debug.Log("Up swipe detected");
    private void OnSwipeDown() => Debug.Log("Down swipe detected");
    
}
