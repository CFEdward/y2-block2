using UnityEngine;
using UnityEngine.InputSystem;

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
        //magnetType = MagnetType.Metal;

        pullButton.action.performed += i => shouldPull = true;
        pullButton.action.canceled += i => shouldPull = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        validHit = SearchRay();
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

    private bool SearchRay()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out magnetHit, Mathf.Infinity, mask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * magnetHit.distance, Color.red);
            Debug.Log(magnetHit.collider);
            return true;
        }

        return false;
    }

    private void Pull()
    {
        var step = speed * Time.deltaTime;
        if (validHit)
        {
            magnetHit.rigidbody.linearVelocity = Vector3.zero;
            magnetHit.rigidbody.angularVelocity = Vector3.zero;
            //magnetHit.transform.position = magnetHit.point;
            //magnetHit.rigidbody.AddForce(transform.position.normalized * -.3f, ForceMode.VelocityChange);
            magnetHit.transform.position = Vector3.MoveTowards(magnetHit.transform.position, transform.position, step);

            if (Vector3.Distance(magnetHit.transform.position, transform.position) < 1f)
            {
                itemAttached = magnetHit.transform.gameObject;
                pullButton.action.Disable();
                //Destroy(magnetHit.transform.gameObject);
                shouldPull = false; // just in case
                isItemAttached = true;
                itemAttached.transform.parent = attachPoint;
                itemAttached.transform.localPosition = Vector3.zero;
                itemAttached.GetComponent<Rigidbody>().isKinematic = true;
                //itemAttached.GetComponent<Collider>().enabled = false;

                return;
            }
        }
    }
}
