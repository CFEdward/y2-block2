using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] private GameObject[] hoops;
    [SerializeField] private ParticleSystem[] confettis;
    [SerializeField] private GameObject rewardPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject confettiVFX;
    private int hoopsCompleted = 0;
    private bool alreadySpawned = false;

    // Update is called once per frame
    void Update()
    {
        if (alreadySpawned) return;

        foreach (var hoop in hoops)
        {
            if (hoop.GetComponentInChildren<goalcheck>().currentScore == 3) hoopsCompleted++;
            else hoopsCompleted = 0;
            if (hoopsCompleted == 3)
            {
                Instantiate(rewardPrefab, spawnPoint.transform);
                foreach (var confetti in confettis)
                {
                    confetti.Play();
                }
                alreadySpawned = true;
            }
        }
    }
}
