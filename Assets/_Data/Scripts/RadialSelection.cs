using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;


public class RadialSelection : MonoBehaviour
{
    
    [Range(2,10)]
    public int numberOfRadialPart;
    public GameObject[] radialPartPrefabs;
    public GameObject radialCenter;
    public Transform radialPartCanvas;
    public float angleBetweenPart = 10;
    public Transform handTransform;

    public UnityEvent<int> OnPartSelected;

    private List<GameObject> spawnedParts = new List<GameObject>();
    private int currentSelectedRadialPart = -1;
    private int lastSelectedRadialPart = 0;

    [SerializeField] private InputActionReference spawnMenuButton;

    public void Awake()
    {
        spawnMenuButton.action.performed += SpawnRadialPart;
        spawnMenuButton.action.canceled += HideAndTriggerSelected;
    }

    // Update is called once per frame
    void Update()
    {
        if (radialPartCanvas.gameObject.activeSelf)
        {
            GetSelectedRadialPart();
        }
    }

    public void HideAndTriggerSelected(InputAction.CallbackContext context)
    {
        OnPartSelected.Invoke(currentSelectedRadialPart);
        radialPartCanvas.gameObject.SetActive(false);
    }

    public void GetSelectedRadialPart()
    {
        Vector3 centerToHand = handTransform.position - radialPartCanvas.position;
        Vector3 centerToHandProjected = Vector3.ProjectOnPlane(centerToHand, radialPartCanvas.forward);

        float angle = Vector3.SignedAngle(radialPartCanvas.up, centerToHandProjected, -radialPartCanvas.forward);

        if (angle < 0)
            angle += 360;

        //Debug.Log("ANGLE" +  angle);

        currentSelectedRadialPart = (int) angle * numberOfRadialPart / 360;

        for (int i = 0; i < spawnedParts.Count; i++)
        {
            if(i == currentSelectedRadialPart)
            {
                spawnedParts[i].GetComponent<Image>().color = Color.yellow;
                spawnedParts[i].transform.localScale = 1.1f * Vector3.one;
                if (lastSelectedRadialPart != currentSelectedRadialPart)
                {
                    HapticsUtility.SendHapticImpulse(.7f, .2f, HapticsUtility.Controller.Right);
                    
                    lastSelectedRadialPart = currentSelectedRadialPart;
                }
            }
            else
            {
                spawnedParts[i].GetComponent<Image>().color = Color.white;
                spawnedParts[i].transform.localScale = Vector3.one;
            }
        }
    }
    public void SpawnRadialPart(InputAction.CallbackContext context)
    {
        radialPartCanvas.gameObject.SetActive(true);
        radialPartCanvas.position = handTransform.position;
        radialPartCanvas.rotation = handTransform.rotation;

        foreach (var item in spawnedParts)
        {
            Destroy(item);
        }

        spawnedParts.Clear();


        for (int i = 0; i < numberOfRadialPart; i++)
        {
            float angle = - i * 360 / numberOfRadialPart - angleBetweenPart / 2;
            Vector3 radialPartEulerAngle = new Vector3(0, 0, angle);

            GameObject spawnedRadialPart = Instantiate(radialPartPrefabs[i], radialPartCanvas);
            spawnedRadialPart.transform.position = radialPartCanvas.position;
            spawnedRadialPart.transform.localEulerAngles = radialPartEulerAngle;

            spawnedRadialPart.GetComponent<Image>().fillAmount = 1; //(1 / (float)numberOfRadialPart) - (angleBetweenPart/360);

            spawnedParts.Add(spawnedRadialPart);
        }

        Instantiate(radialCenter, radialPartCanvas);
        radialCenter.transform.position = radialPartCanvas.position;
    }

    private void OnDisable()
    {
        spawnMenuButton.action.performed -= SpawnRadialPart;
        spawnMenuButton.action.canceled -= HideAndTriggerSelected;
    }
}
