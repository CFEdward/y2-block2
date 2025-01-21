using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class Collect : MonoBehaviour
{
    public static List<GameObject> collectedItems = null;
    private AudioSource audioSource;
    [SerializeField] private AudioClip collectSound;

    private GameObject prefabMetal;
    private GameObject prefabRock;
    private GameObject prefabFish;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = collectSound;

        collectedItems = new List<GameObject>();

        prefabMetal = Resources.Load("MetalThrowable") as GameObject;
        prefabRock = Resources.Load("RockThrowable") as GameObject;
        prefabFish = Resources.Load("FishThrowable") as GameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Metal") && other.GetComponentInParent<Magnet>() != null && other.GetComponentInParent<Magnet>().isItemAttached)
        {
            collectedItems.Add(prefabMetal);
            Detach(other);

            return;
        }
        if (other.CompareTag("Rock") && other.GetComponentInParent<Magnet>() != null && other.GetComponentInParent<Magnet>().isItemAttached)
        {
            collectedItems.Add(prefabRock);
            Detach(other);

            return;
        }
        if (other.CompareTag("Fish") && other.GetComponentInParent<Magnet>() != null && other.GetComponentInParent<Magnet>().isItemAttached)
        {
            collectedItems.Add(prefabFish);
            Detach(other);

            return;
        }
    }

    private void Detach(Collider other)
    {
        other.GetComponentInParent<Magnet>().isItemAttached = false;
        Debug.Log("Collecting " + other.GetComponentInParent<Magnet>().itemAttached);
        Destroy(other.GetComponentInParent<Magnet>().itemAttached);
        other.GetComponentInParent<Magnet>().pullButton.action.Enable();
        audioSource.Play();
        HapticsUtility.SendHapticImpulse(.3f, .2f, HapticsUtility.Controller.Left);
    }
}
