using System.Linq;
using UnityEngine;

public class ActivateTest : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    public void ShowDebug()
    {
        if (Collect.collectedItems.Count <= 0) return;

        Instantiate(Collect.collectedItems.Last(), spawnPoint.position, Quaternion.identity);
        Collect.collectedItems.RemoveAt(Collect.collectedItems.Count - 1);
    }
}
