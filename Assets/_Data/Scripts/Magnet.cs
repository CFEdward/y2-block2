using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public enum MagnetType
{
    Metal,
    Fish,
    Rock,
}

public class Magnet : MonoBehaviour
{
    public static RaycastHit magnetHit;
    [SerializeField] private MagnetType magnetType = 0;
    private LayerMask mask = 0;
    private GameObject hitLastFrame = null;
    [SerializeField] private Material highlightMat;

    //[SerializeField] private float speed = .5f;
    public InputActionReference pullButton;
    private bool validHit;
    private bool shouldPull;

    [SerializeField] private Transform attachPoint;
    public bool isItemAttached = false;
    public GameObject itemAttached = null;

    private void Awake()
    {
        pullButton.action.performed += i => shouldPull = true;
        pullButton.action.canceled += i => shouldPull = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SearchRay() <= .3f)
        {
            validHit = true;
        }
        else if (SearchRay() >= 1f)
        {
            validHit = false;
        }
    }

    private void Update()
    {
        if (shouldPull) Pull();
    }

    public void ChangeMagnet(int i)
    {
        magnetType = (MagnetType)i;

        switch (magnetType)
        {
            case MagnetType.Metal:
                mask = LayerMask.GetMask("Metal");
                Debug.Log("Current Magnet equipped: Metal");
                break;
            case MagnetType.Fish:
                mask = LayerMask.GetMask("Fish");
                Debug.Log("Current Magnet equipped: Fish");
                break;
            case MagnetType.Rock:
                mask = LayerMask.GetMask("Rock");
                Debug.Log("Current Magnet equipped: Rock");
                break;

            default: break;
        }
    }

    private float SearchRay()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out magnetHit, Mathf.Infinity, mask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * magnetHit.distance, Color.red);
            Debug.Log(magnetHit.collider);

            float distance = HandleUtility.DistancePointLine(magnetHit.collider.transform.position, transform.position, transform.TransformDirection(Vector3.forward) * 1100f);
            Debug.Log(distance);

            hitLastFrame = magnetHit.transform.gameObject;
            List<Material> mats = new();
            magnetHit.transform.GetComponent<MeshRenderer>().GetMaterials(mats);
            if (mats.Count == 1) magnetHit.transform.GetComponent<MeshRenderer>().AddMaterial(highlightMat);
            HapticsUtility.SendHapticImpulse(1f - Mathf.Clamp(distance, 0f, 1f), .1f, HapticsUtility.Controller.Right);

            return distance;
        }
        else
        {
            if (hitLastFrame)
            {
                List<Material> mats = new();
                hitLastFrame.GetComponent<MeshRenderer>().GetMaterials(mats);
                mats.RemoveAt(1);
                hitLastFrame.GetComponent<MeshRenderer>().SetMaterials(mats);
            }
            hitLastFrame = null;
            return Mathf.Infinity;
        }
    }

    private void Pull()
    {
        //var step = (13f - Mathf.Clamp(magnetHit.distance, 3f, 10f)) * Time.deltaTime;
        var step = Mathf.Lerp(8f, 4f, magnetHit.distance) * Time.deltaTime;
        if (validHit)
        {
            magnetHit.rigidbody.linearVelocity = Vector3.zero;
            magnetHit.rigidbody.angularVelocity = Vector3.zero;
            magnetHit.transform.position = Vector3.MoveTowards(magnetHit.transform.position, transform.position, step);

            if (Vector3.Distance(magnetHit.transform.position, transform.position) <= 2f)
            {
                itemAttached = magnetHit.transform.gameObject;
                pullButton.action.Disable();
                shouldPull = false; // just in case
                isItemAttached = true;
                itemAttached.transform.parent = attachPoint;
                itemAttached.transform.localPosition = Vector3.zero;
                itemAttached.GetComponent<Rigidbody>().isKinematic = true;

                return;
            }
        }
    }
}
