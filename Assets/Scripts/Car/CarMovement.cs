using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public Transform leftWheelTransform;
    public Transform rightWheelTransform;
    public bool motor;
    public bool steering;
}
public class CarMovement : MonoBehaviour
{
    public VariableJoystick joystick;
    public PlayerController playerController;
    NavMeshAgent _agent;
    Player player;

    public float rotSpeedX = 20.0f;
    private float rotSpeedY = 1.5f;
    public float baseSpeed = 30f;
    public float maxSteeringAngle;
    public List<AxleInfo> axleInfos;
    private void Awake()
    {
        player = GetComponent<Player>();
        _agent = GetComponent<NavMeshAgent>();
        TryGetComponent<Player>(out player);
    }
    private void Update()
    {


        if (!joystick || !player.isCurrentPlayer) return;
        float isMoving = (Input.GetMouseButton(0) == true) || Input.touchCount != 0 ? 1 : 0;


        Vector3 moveVector = transform.forward * baseSpeed * isMoving;
        Vector3 inputs = joystick.Direction;

        Vector3 dir = inputs.x * transform.right * rotSpeedX * Time.deltaTime;

        Vector3 lookDirection = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        if (lookDirection.magnitude != 0)
        {
            Debug.Log("moving");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.rotation * lookDirection, Vector3.up), Time.deltaTime * rotSpeedX);
            float steering = maxSteeringAngle * joystick.Horizontal;
            Move(moveVector * Time.deltaTime, steering);
        }


    }
    public void ApplyLocalPositionToVisuals(WheelCollider collider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        transform.position = position;
        transform.rotation = rotation;
    }
    void Move(Vector3 move, float steering)
    {
        transform.position += move;

      
        foreach (AxleInfo axleInfo in axleInfos)
        {

            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }


            ApplyLocalPositionToVisuals(axleInfo.leftWheel, axleInfo.leftWheelTransform);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel, axleInfo.rightWheelTransform);

            axleInfo.leftWheelTransform.Rotate(Vector3.right * 90 * Time.deltaTime);
            axleInfo.rightWheelTransform.Rotate(Vector3.right * 150 * Time.deltaTime);
        }

    }

}
