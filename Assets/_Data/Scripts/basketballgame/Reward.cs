using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] private GameObject[] hoops;
    [SerializeField] private GameObject rewardPrefab;
    [SerializeField] private GameObject spawnPoint;
    private int hoopsCompleted = 0;
    private bool alreadySpawned = false;

    // Update is called once per frame
    void Update()
    {
        if (alreadySpawned) return;

        foreach (var hoop in hoops)
        {
            if (hoop.GetComponentInChildren<goalcheck>().currentScore == 1) hoopsCompleted++;
            else hoopsCompleted = 0;
            if (hoopsCompleted == 3)
            {
                Instantiate(rewardPrefab, spawnPoint.transform);
                // TODO: add vfx
                alreadySpawned = true;
            }
        }
    }
}
