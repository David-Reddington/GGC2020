using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public string ClickTag;

    private GameObject CurrentSelected;
    private Transform menu;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse is down");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if (menu != null && menu.gameObject.activeSelf)
                {
                    if (hitInfo.transform.gameObject.name != CurrentSelected.name)
                    {
                        menu.gameObject.SetActive(false);
                    }

                }
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.tag == ClickTag)
                {
                    CurrentSelected = hitInfo.transform.gameObject;
                    menu = CurrentSelected.transform.Find("BuildingMenu");

                    if (!menu.gameObject.activeSelf)
                    {
                        menu.gameObject.SetActive(true);
                    }
                    else
                    {
                        menu.gameObject.SetActive(false);
                    }

                    Debug.Log("It's Working!");
                }
            }
        }
    }
}
