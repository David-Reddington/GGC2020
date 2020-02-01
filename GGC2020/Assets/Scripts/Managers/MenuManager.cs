using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private Quaternion cameraRotation;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] temp2 = GameObject.FindGameObjectsWithTag("CameraPosition");

        cameraRotation = temp2[0].GetComponent<Transform>().rotation;
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    public void PlayButton()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("GameManager");
        temp[0].GetComponent<GameManager>().currentGamestate = Gamestate.Play;
        transform.Find("MainMenu").gameObject.SetActive(false);

        GameObject[] temp2 = GameObject.FindGameObjectsWithTag("CameraPosition");

        temp2[0].GetComponent<Transform>().rotation = cameraRotation;
        //SetCamera Angle Again 

    }
    public void OptionsButton()
    {
        transform.Find("MainMenu").gameObject.SetActive(false);
        transform.Find("OptionsMenu").gameObject.SetActive(true);
    }



    public void OptionsBackButton()
    {
        transform.Find("OptionsMenu").gameObject.SetActive(false);
        transform.Find("MainMenu").gameObject.SetActive(true);

    }


}
