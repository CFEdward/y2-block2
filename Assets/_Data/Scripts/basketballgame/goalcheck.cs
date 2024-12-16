
using UnityEngine;

public class goalcheck : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip goalSound;
    [SerializeField] Transform ballSpawnPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Basketball"))
        {
            audioSource.clip = goalSound;
            audioSource.Play();
            GameObject ball = other.gameObject;
            ball.transform.position = ballSpawnPosition.position;
            ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
