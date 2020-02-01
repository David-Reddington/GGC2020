using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public List<string> ClickTagList;
    public string UITag;

    private GameObject CurrentSelected;
    private GameObject menu;

    public GameObject MenuPrefab;
    public GameObject MenuContentHolder;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            //if (results.Count > 0)
            //{
            //    //WorldUI is my layer name
            //    if (results[0].gameObject.layer == LayerMask.GetMask("UI"))
            //    {
            //        string dbg = "Root Element: {0} \n GrandChild Element: {1}";
            //        Debug.Log(string.Format(dbg, results[results.Count - 1].gameObject.name, results[0].gameObject.name));
            //        //Debug.Log("Root Element: "+results[results.Count-1].gameObject.name);
            //        //Debug.Log("GrandChild Element: "+results[0].gameObject.name);
            //        results.Clear();
            //    }
            //}
            //else
            //{
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
                    foreach(var s in ClickTagList)
                    {
                        if(hitInfo.transform.gameObject.tag == s)
                        {
                            CurrentSelected = hitInfo.transform.gameObject;
                            menu = Instantiate(MenuPrefab, new Vector3(MenuContentHolder.transform.position.x, MenuContentHolder.transform.position.y + 10, MenuContentHolder.transform.position.z), Quaternion.identity);
                            menu.transform.SetParent(MenuContentHolder.transform);
                            menu.SetActive(true);
                            
                            //menu = CurrentSelected.transform.Find("BuildingMenu");

                            //if(!menu.gameObject.activeSelf)
                            //{
                            //    menu.gameObject.SetActive(true);
                            //}
                            //else
                            //{
                            //    menu.gameObject.SetActive(false);
                            //}
                            Debug.Log("It's Working!");
                        }
                    }
                }
        }


            Debug.Log("Mouse is down");
    }

    void SetData()
    {

    }
}
