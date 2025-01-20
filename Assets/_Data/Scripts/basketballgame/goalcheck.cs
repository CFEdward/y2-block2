using TMPro;
using UnityEngine;

public class goalcheck : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip goalSound;
    [SerializeField] private string tagName = "Fish";
    [SerializeField] private TextMeshProUGUI scoreText;
    public int currentScore = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(tagName))
        {
            audioSource.clip = goalSound;
            audioSource.Play();
            Destroy(other.gameObject);
            if (currentScore <= 3) currentScore++;
            else currentScore = 3;
            scoreText.text = currentScore.ToString() + "/3";
        }
    }
}
