using UnityEngine;

public class MenuPosition : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject startMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = player.transform.position + player.transform.forward;
        transform.LookAt(player.transform); transform.localEulerAngles -= new Vector3(0f, 180f, 0f);
        startMenu.SetActive(true);
    }
}
