using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public static List<GameObject> collectedItems = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collectedItems = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Metal") || other.CompareTag("Rock") || other.CompareTag("Fish"))
        {
            if (other.GetComponentInParent<Magnet>().isItemAttached)
            collectedItems.Add(other.GetComponentInParent<Magnet>().itemAttached);
            other.GetComponentInParent<Magnet>().isItemAttached = false;
            Debug.Log("Collecting " + other.GetComponentInParent<Magnet>().itemAttached);
            Destroy(other.GetComponentInParent<Magnet>().itemAttached);
            other.GetComponentInParent<Magnet>().pullButton.action.Enable();
        }
    }
}
