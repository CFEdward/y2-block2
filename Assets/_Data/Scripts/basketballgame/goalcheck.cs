
using UnityEngine;

public class goalcheck : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip goalSound;
    //[SerializeField] private Transform ballSpawnPosition;
    [SerializeField] private string tagName = "Fish";

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
        if(other.gameObject.CompareTag(tagName))
        {
            audioSource.clip = goalSound;
            audioSource.Play();
            Destroy(other.gameObject);
            //GameObject ball = other.gameObject;
            //ball.transform.position = ballSpawnPosition.position;
            //ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            //ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
