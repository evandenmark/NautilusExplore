using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

    public enum MenuStates { Main, Vehicle, Dive};
    public MenuStates currentMenuState;

    public Canvas mainMenu;
    public Canvas vehicleScreen;
    public Canvas diveScreen;
    public ARTapToPlaceObject arTapScript;
    public DiveStartup diveStartupScript;
    public ARStartup arStartupScript;
    public GameObject diveSession;
    public GameObject arSession;
    public Material blackSkybox;

    private Material defaultSkybox;

    void Awake()
    {
        currentMenuState = MenuStates.Main;
        defaultSkybox = RenderSettings.skybox;
    }

    void Update()
    {
        switch (currentMenuState)
        {
            case MenuStates.Main:
                //display correct buttons
                mainMenu.gameObject.SetActive(true);
                vehicleScreen.gameObject.SetActive(false);
                diveScreen.gameObject.SetActive(false);

                //get correct scene setup
                diveSession.SetActive(false);
                arSession.SetActive(false);
                RenderSettings.skybox = defaultSkybox;

                break;

            case MenuStates.Vehicle:
                //display correct buttons
                mainMenu.gameObject.SetActive(false);
                vehicleScreen.gameObject.SetActive(true);
                diveScreen.gameObject.SetActive(false);

                //get correct scene setup
                diveSession.SetActive(false);
                arSession.SetActive(true);
                RenderSettings.skybox = defaultSkybox;

                break;

            case MenuStates.Dive:
                //display correct buttons
                mainMenu.gameObject.SetActive(false);
                vehicleScreen.gameObject.SetActive(false);
                diveScreen.gameObject.SetActive(true);

                //get correct scene setup
                diveSession.SetActive(true);
                arSession.SetActive(false);
                RenderSettings.skybox = blackSkybox;
                

                break;
        }
    }

    public MenuStates getMenuState()
    {
        return currentMenuState;
    }

    //Main Menu
    public void VehicleButtonPressed()
    {
        currentMenuState = MenuStates.Vehicle;
        arStartupScript.startAR();
    }

    public void DiveButtonPressed()
    {
        currentMenuState = MenuStates.Dive;
        diveStartupScript.startDive();
        diveStartupScript.resetHerc();
    }


    //AR Vehicle Buttons
    public void HercGraphicPressed()
    {
        arTapScript.TransitionToHercVehicleState();
        arTapScript.DestroyOrPlaceObject();

    }

    public void ArgusGraphicPressed()
    {
        arTapScript.TransitionToArgusVehicleState();
        arTapScript.DestroyOrPlaceObject();
    }

    //back button for both AR session and Dive session
    public void BackToMain()
    {
        currentMenuState = MenuStates.Main;
        arTapScript.DestroyHercAndArgus();
        diveStartupScript.endDiveSession();
    }
}
