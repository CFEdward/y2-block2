using UnityEngine;

public class TargetMovementScript : MonoBehaviour
{
    private bool changeDir = false;
    [SerializeField]
    private float moveSpeed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= 4 && !changeDir)
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            if (transform.position.x >= 4) 
            {
                changeDir = true;
            }
        }
        else if (changeDir)
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            if(transform.position.x <= -4)
            {
                changeDir = false;
            }
        }
    }
}
