using UnityEngine;
using UnityEngine.VFX;

public class MagnetVFXLine : MonoBehaviour
{
    private VisualEffect lineVFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineVFX = GetComponent<VisualEffect>();
        //lineVFX.SetVector3("AttractorV", );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
