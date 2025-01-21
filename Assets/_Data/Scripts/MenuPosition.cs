using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuPosition : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject startMenu;

    [SerializeField] private InputActionReference pullButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //transform.SetParent(null, false);
            startMenu.SetActive(true);
        }
        else
        {
            pullButton.action.performed += ToggleMenu;
            //pullButton.action.canceled += HideMenu;
        }

    }

    private void Show()
    {
        transform.position = player.transform.position + player.transform.forward;
        transform.LookAt(player.transform); transform.localEulerAngles -= new Vector3(0f, 180f, 0f);
        startMenu.SetActive(true);
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        if (!startMenu.activeSelf)
        {
            startMenu.SetActive(true);
            //transform.position = player.transform.position + player.transform.forward;
            //transform.LookAt(player.transform); transform.localEulerAngles -= new Vector3(0f, 180f, 0f);
        }
        else
        {
            startMenu.SetActive(false);
        }
    }

    private void HideMenu(InputAction.CallbackContext context)
    {
        startMenu.SetActive(false);
    }

    private void OnDestroy()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            pullButton.action.performed -= ToggleMenu;
        //pullButton.action.canceled -= HideMenu;
    }
}
