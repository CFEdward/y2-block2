using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.UI;

public class RadialSelection : MonoBehaviour
{
    [Range(2,10)]
    public int numberOfRadialPart;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    public float angleBetweenPart = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnRadialPart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRadialPart()
    {
        for (int i = 0; i < numberOfRadialPart; i++)
        {
            float angle = i * 360 / numberOfRadialPart - angleBetweenPart/2;
            Vector3 radialPartEulerAngle = new Vector3(0, 0, angle);

            GameObject spawnedRadialpart = Instantiate(radialPartPrefab, radialPartCanvas);
            spawnedRadialpart.transform.position = radialPartCanvas.position;
            spawnedRadialpart.transform.localEulerAngles = radialPartEulerAngle;

            spawnedRadialpart.GetComponent<Image>().fillAmount = (1 / (float)numberOfRadialPart) - (angleBetweenPart/360);
        }
    }
}
