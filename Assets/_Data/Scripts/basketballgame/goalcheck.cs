using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

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
            audioSource.volume = .5f;
            audioSource.Play();
            HapticsUtility.SendHapticImpulse(1f, .5f, HapticsUtility.Controller.Left);
            HapticsUtility.SendHapticImpulse(1f, .5f, HapticsUtility.Controller.Right);
            Destroy(other.gameObject);
            if (currentScore <= 3) currentScore++;
            else currentScore = 3;
            scoreText.text = currentScore.ToString() + "/3";
        }
    }
}
