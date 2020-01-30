using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class DiveStartup : MonoBehaviour
{
    /// <summary>
    /// This controls the items in a Dive session to the coral reef.
    /// This involves all environment movies and sounds as well as instructions and GUI controls. 
    /// </summary>

    //game object connections
    public GameObject launchScreen;
    public GameObject herc;
    public GameObject argus;
    public GameObject mainCamButton;
    public GameObject argusCamButton;
    public GameObject bottomCamButton;
    public GameObject slider;
    public GameObject leftJoystick;
    public GameObject rightJoystick;
    public TextMeshProUGUI textObject;

    //Timing
    private float SCREEN_DISAPPEAR_TIME = 50;
    private float START_DESCENT_TIME = 30;
    private float END_DESCENT_TIME = 85;

    //initial conditions
    private float downVelocity = 0.0f;
    private float acceleration = -10.0f;
    private bool diveSessionStarted = false;
    private float diveStartTime;
    private Vector3 hercStartPosition;
    private Vector3 argusStartPosition;
    private MovementScript hercMovement;
    


    private float TIME_WELCOME = 0;
    private float TIME_HERC = 8;
    private float TIME_MISSION = 16;
    private float TIME_DESCENT = 24;
    private float TIME_CONTROLS = 32;
    private float TIME_CAMERA = 40;
    private float TIME_LEFTJOY = 48;
    private float TIME_RIGHTJOY = 56;
    private float TIME_SLIDER = 64;
    private float TIME_NOTHING = 72;
    private float TIME_BOTTOM = 80;

    private String TEXT_WELCOME = "Welcome to the E/V Nautilus!";
    private String TEXT_HERC = "You are now onboard HERCULES,\n the deep sea exploration vehicle";
    private String TEXT_MISSION = "Your Mission: \n EXPLORE DEEP SEA CORAL REEFS";
    private String TEXT_DESCENT = "Let's begin our descent to 5000 feet";
    private String TEXT_CONTROLS = "When we get to the bottom, \nyou'll have control of Hercules";
    private String TEXT_CAMERA = "You're looking through MAIN CAMERA. \nYou can also view BOTTOM CAMERA or from ARGUS";
    private String TEXT_LEFTJOY = "LEFT JOYSTICK controls your movement";
    private String TEXT_RIGHTJOY = "RIGHT JOYSTICK controls our DEPTH and ROTATION";
    private String TEXT_SLIDER = "The SLIDER controls the LIGHTS.";
    private String TEXT_BOTTOM = "We're nearly at the bottom! Get ready to explore!";


    // Start is called before the first frame update
    void Start()
    {
        //reset 
        hercStartPosition = herc.transform.position;
        argusStartPosition = argus.transform.position;
        hercMovement = herc.GetComponent<MovementScript>();

        //reset buttons to not be seen
        mainCamButton.SetActive(false);
        argusCamButton.SetActive(false);
        bottomCamButton.SetActive(false);
        slider.SetActive(false);
        leftJoystick.SetActive(false);
        rightJoystick.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {

        if (diveSessionStarted)
        {
            

            //CONTROL DESCENT MOVEMENT OF HERC
            if ((Time.time - diveStartTime) > START_DESCENT_TIME)
            {
                herc.transform.position += herc.transform.up * Time.deltaTime * downVelocity;
                argus.transform.position += argus.transform.up * Time.deltaTime * downVelocity;
                if ((Time.time - diveStartTime) > END_DESCENT_TIME)
                {
                    acceleration = 1010.0f;
                    downVelocity = Math.Min(downVelocity, 0);
                    if (downVelocity > -20)
                    {
                        endDescent();
                    }
                }
                downVelocity += downVelocity * Time.deltaTime + acceleration * Time.deltaTime;
                downVelocity = Math.Max(downVelocity, -1000);
            }

            //CONTROL INITIAL VIDEO SCREEN
            if ((Time.time - diveStartTime) > SCREEN_DISAPPEAR_TIME)
            {
                launchScreen.SetActive(false);
            
            } else if ((Time.time - diveStartTime)< SCREEN_DISAPPEAR_TIME)
            {
                launchScreen.SetActive(true);
            }

            ///CONTROL TEXT
            if ((Time.time - diveStartTime) > TIME_WELCOME)
            {
                textObject.text = TEXT_WELCOME;
            }

            if ((Time.time - diveStartTime) > TIME_HERC)
            {
                textObject.text = TEXT_HERC;
            }

            if ((Time.time - diveStartTime) > TIME_MISSION)
            {
                textObject.text = TEXT_MISSION;
            }

            if ((Time.time - diveStartTime) > TIME_DESCENT)
            {
                textObject.text = TEXT_DESCENT;
            }

            if ((Time.time - diveStartTime) > TIME_CONTROLS)
            {
                textObject.text = TEXT_CONTROLS;
            }

            if ((Time.time - diveStartTime) > TIME_CAMERA)
            {
                textObject.text = TEXT_CAMERA;
                mainCamButton.SetActive(true);
                argusCamButton.SetActive(true);
                bottomCamButton.SetActive(true);
            }

            if ((Time.time - diveStartTime) > TIME_LEFTJOY)
            {
                textObject.text = TEXT_LEFTJOY;
                leftJoystick.SetActive(true);
            }

            if ((Time.time - diveStartTime) > TIME_RIGHTJOY)
            {
                textObject.text = TEXT_RIGHTJOY;
                rightJoystick.SetActive(true);
            }

            if ((Time.time - diveStartTime) > TIME_SLIDER)
            {
                textObject.text = TEXT_SLIDER;
                slider.SetActive(true);
            }

            if ((Time.time - diveStartTime) > TIME_NOTHING)
            {
                textObject.text = " ";
            }

            if ((Time.time - diveStartTime) > TIME_BOTTOM)
            {
                textObject.text = TEXT_BOTTOM;
                hercMovement.activateJoysticks();
            }

            
        }

        
    }

    public void startDive()
    {
        diveSessionStarted = true;
        diveStartTime = Time.time;
        acceleration = -10.0f;
        downVelocity = 0.0f;
        resetHerc();
        textObject.gameObject.SetActive(true);

    }

    public void endDiveSession()
    {
        launchScreen.SetActive(false);
        hercMovement.deactivateJoysticks();
    }

    public void endDescent()
    {
        diveSessionStarted = false;
        downVelocity = 0.0f;
        textObject.gameObject.SetActive(false);
        textObject.text = " ";
    }

    public void resetHerc()
    {
        herc.transform.position = hercStartPosition;
        argus.transform.position = argusStartPosition;
    }


}
