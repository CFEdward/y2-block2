using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> subMenus;

    public void OpenSubMenu(int index)
    {
        foreach (GameObject menu in subMenus)
        {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
            }
        }
        subMenus[index].SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
