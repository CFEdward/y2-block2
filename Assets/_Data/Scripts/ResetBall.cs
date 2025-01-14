using UnityEngine;
using UnityEngine.InputSystem;

public class ResetBall : MonoBehaviour
{
    [SerializeField] private InputActionReference resetButton;
    [SerializeField] private Transform resetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        resetButton.action.performed += OnResetBall;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnResetBall(InputAction.CallbackContext context)
    {
        transform.position = resetPosition.position;
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    private void OnDestroy()
    {
        resetButton.action.performed -= OnResetBall;
    }
}
