using UnityEngine;

public class Hover : MonoBehaviour
{
    public string controllerTag = "VRController";
    public float vibrationDuration = 0.1f;
    public float vibrationStrength = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(controllerTag))
        {
            TriggerHapticFeedback(other);
        }
    }

    private void TriggerHapticFeedback(Collider controller)
    {
        Debug.Log("Hover detected, trigger haptic feedback");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
