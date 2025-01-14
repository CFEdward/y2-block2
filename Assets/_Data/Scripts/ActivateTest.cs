using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class ActivateTest : MonoBehaviour
{
    /*
    private XRGrabInteractable grabInt;
    [SerializeField] private float speed = 1f;
    private List<IXRSelectInteractor> intList;
    private Transform intTrans;
    private bool shouldMove = false;
    */

    [SerializeField] Material newMat;
    private MeshRenderer mr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //grabInt = GetComponent<XRGrabInteractable>();
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (shouldMove) Move();
    }

    /*
    public void Pull()
    {
        intList = grabInt.interactorsSelecting;
        intTrans = intList.First().transform;
        shouldMove = true;
    }

    public void StopPull()
    {
        shouldMove = false;
    }

    private void Move()
    {
        var step = speed * Time.deltaTime;
        var grabTransf = GetComponent<XRGeneralGrabTransformer>();
        transform.position = Vector3.MoveTowards(grabTransf.transform.position, intTrans.position, step);
        
    }
    */

    public void ChangeMat()
    {
        mr.material = newMat;
    }
}
