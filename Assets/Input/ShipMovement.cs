using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    public InputActionAsset movementVerticalActions;
    private InputAction actionUp;
    private InputAction actionHorizontal;
    private InputAction powerButton;
    private Rigidbody rb;
    public float verticalSpeed = 5f;
    public float horizontalSpeed = 5f;
    public float standartSpeed = 5;
    public float powerMultipliyerSpeed = 1.5f;
    public float maxRotationSpeed;
    private float final_roll = 0f;

    // Start is called before the first frame update
    void Start()
    {
        actionUp = movementVerticalActions.FindActionMap("movement").FindAction("Up");
        actionHorizontal = movementVerticalActions.FindActionMap("movement").FindAction("Horizontal");
        powerButton = movementVerticalActions.FindActionMap("movement").FindAction("Power");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (powerButton.ReadValue<float>() > 0)
        {
            rb.velocity = new Vector3(actionHorizontal.ReadValue<float>() * horizontalSpeed, actionUp.ReadValue<float>() * verticalSpeed, standartSpeed * powerMultipliyerSpeed);
            //rb.MovePosition(rb.transform.position + rb.transform.forward * standartSpeed * powerMultipliyerSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector3(actionHorizontal.ReadValue<float>() * horizontalSpeed, actionUp.ReadValue<float>() * verticalSpeed, standartSpeed);
            //rb.MovePosition(rb.transform.position + rb.transform.forward * standartSpeed * Time.deltaTime);
        }

        float up = actionUp.ReadValue<float>();
        float right = actionHorizontal.ReadValue<float>();
        float power = powerButton.ReadValue<float>();
        //Rotation(up, right);



    }

    private void Rotation(float pitch, float yaw)
    {
        float inc_pitch = pitch * maxRotationSpeed * Time.deltaTime;
        float current_pitch = rb.rotation.eulerAngles.x;
        if (current_pitch > 180f)
        {
            current_pitch -= 360;
        }
        float final_pitch = current_pitch + inc_pitch;
        final_pitch = Mathf.Clamp(final_pitch, -60, 60f);

        float inc_yaw = yaw * maxRotationSpeed * Time.deltaTime;
        float current_yaw = rb.rotation.eulerAngles.y;
        if (current_yaw > 180f)
        {
            current_yaw -= 360;
        }
        float final_yaw = current_yaw + inc_yaw;
        final_yaw = Mathf.Clamp(final_yaw, -60, 60f);

        if (yaw != 0)
        {
            final_roll = Mathf.Lerp(final_roll, -yaw * 60f, 01f);
        }
        else
        {
            final_roll = Mathf.Lerp(final_roll, 0f, 0.3f);
        }

        rb.rotation = Quaternion.Euler(final_pitch, final_yaw, final_roll);

    }


    private void OnEnable()
    {
        movementVerticalActions.Enable();
    }

    private void OnDisable()
    {
        movementVerticalActions.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Crash");
    }
}
