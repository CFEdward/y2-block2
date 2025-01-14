using UnityEngine;

public class BinTrigger : MonoBehaviour
{
    public ScoreSystem scoreManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scorable"))
        {
            scoreManager.IncreaseScore(10);
            Destroy(other.gameObject);
        }
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
