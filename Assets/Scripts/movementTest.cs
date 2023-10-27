using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movementTest : MonoBehaviour
{


    public InputActionAsset movementActions;
    private InputAction actionUp;
    private InputAction actionHorizontal;
    public float speed = 10;
    public float lookSpeed = 10;
    public Transform aimObject;
    private Transform playerModel;
    [SerializeField] float leanLimit = 3.0f;
    // Start is called before the first frame update

    void Start()
    {
        playerModel = transform.GetChild(0);
        actionUp = movementActions.FindActionMap("movement").FindAction("Up");
        actionHorizontal = movementActions.FindActionMap("movement").FindAction("Horizontal");


    }

    // Update is called once per frame
    void Update()
    {
        float vertical = actionUp.ReadValue<float>();
        float horizontal = actionHorizontal.ReadValue<float>();
        LocalMove(horizontal, vertical, speed);
        RotationLook(horizontal, vertical, lookSpeed);
        HorizontalLean(playerModel, horizontal, leanLimit, .1f);
    }

    void LocalMove(float x, float y, float _speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * _speed * Time.deltaTime;
        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void RotationLook(float h, float v, float _speed)
    {
        aimObject.parent.position = Vector3.zero; 
        aimObject.localPosition = new Vector3(h, v, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimObject.position), Mathf.Deg2Rad * _speed * Time.deltaTime);
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimObject.position, .009f);
        Gizmos.DrawSphere(aimObject.position, .001f);
    }

    private void OnEnable()
    {
        movementActions.Enable();

    }
    private void OnDisable()
    {
        movementActions.Disable();

    }
}
