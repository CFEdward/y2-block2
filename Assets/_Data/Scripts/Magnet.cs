using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public enum MagnetType
{
    Metal,
    Rock,
    Fish
}

public class Magnet : MonoBehaviour
{
    public static RaycastHit magnetHit;
    [SerializeField] private MagnetType magnetType = 0;
    private LayerMask mask = 0;

    [SerializeField] private float speed = 1f;
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
        else
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
            case MagnetType.Rock:
                mask = LayerMask.GetMask("Rock");
                Debug.Log("Current Magnet equipped: Rock");
                break;
            case MagnetType.Fish:
                mask = LayerMask.GetMask("Fish");
                Debug.Log("Current Magnet equipped: Fish");
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

            HapticsUtility.SendHapticImpulse(1f - Mathf.Clamp(distance, 0f, 1f), .1f, HapticsUtility.Controller.Right);

            return distance;
        }
        else
        {
            return Mathf.Infinity;
        }
    }

    private void Pull()
    {
        var step = speed * Time.deltaTime;
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
