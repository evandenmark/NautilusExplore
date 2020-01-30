using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementScript : MonoBehaviour
{
    /// <summary>
    /// This controls the movement of Herc with regards to the joystick controllers.
    /// Joysticks are only activated once Herc reaches the bottom.
    /// </summary>

    public Joystick leftJoystick;
    public Joystick rightJoystick;
    private Vector2 lateralDirection;
    private Vector2 verticalDirection;
    public GameObject herc;

    private float VELOCITY_CONSTANT = 5.0f;
    private float ANGULAR_CONSTANT = 5.0f;

    private bool joysticksActivated;

    // Start is called before the first frame update
    void Start()
    {
        joysticksActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (joysticksActivated)
        {
            //move Herc according to joystick
            lateralDirection = new Vector2(leftJoystick.Horizontal, leftJoystick.Vertical) * VELOCITY_CONSTANT;
            verticalDirection = new Vector2(rightJoystick.Horizontal * ANGULAR_CONSTANT, rightJoystick.Vertical * VELOCITY_CONSTANT);
            herc.transform.Translate(-lateralDirection.y, verticalDirection.y, lateralDirection.x);
            herc.transform.Rotate(0, verticalDirection.x * Time.deltaTime, 0);
        }
    }

    public void activateJoysticks()
    {
        joysticksActivated = true;
    }

    public void deactivateJoysticks()
    {
        joysticksActivated = false;
    }
}
