using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AircraftMovement : MonoBehaviour
{
    public VariableJoystick joystick;
    public Transform AirPosition;
    public Transform LandingPosition;
    bool isFlying = false;
    Player player;
    public PlayerController playerController;
    Animator animator;
    private void Start()
    {
        player = GetComponent<Player>();
        Subscribe();
        animator = GetComponentInChildren<Animator>();
        //rb = GetComponent<Rigidbody>();
    }
    void Subscribe()
    {
        playerController.OnPlayerChange += Fly;
    }
    void Unsubscribe()
    {
        playerController.OnPlayerChange -= Fly;
    }
    #region firstWay
    //public float Speed = 10f;
    //Rigidbody rb;
    //public float ForwardSpeed = 100;
    //public float ForwardSpeedMultiplier = 10;
    //public float SpeedMult = 4;
    //public float smooothness = 5;

    //public float maxVertical = 0.1f;
    //public float maxHorizontal = 0.06f;
    //private void Update()
    //{
    //    if (!_joystick || !player.isCurrentPlayer) return;
    //    HandleRotation(_joystick.Direction);
    //}
    //private void FixedUpdate()
    //{
    //    if (!_joystick || !player.isCurrentPlayer) return;
    //    HandleMovement(_joystick.Direction);
    //}
    //void HandleMovement(Vector3 direction)
    //{
    //    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, ForwardSpeed * ForwardSpeedMultiplier * Time.deltaTime);
    //    float xVelocity = direction.x * SpeedMult * Time.deltaTime;
    //    float yVelocity = -direction.y * SpeedMult * Time.deltaTime;

    //    rb.velocity = Vector3.Lerp(
    //        rb.velocity,
    //        new Vector3(xVelocity, yVelocity, rb.velocity.z),
    //        Time.deltaTime * smooothness
    //        );

    //}
    //void Move(Vector3 direction)
    //{
    //    float isMoving = (Input.GetMouseButton(0) == true) || Input.touchCount != 0 ? 1 : 0;

    //    Vector3 movementVector = new Vector3(direction.x, 0, Speed);

    //    movementVector = movementVector * Time.deltaTime * Speed;

    //    transform.position += movementVector;

    //}

    //void HandleRotation(Vector3 direction)
    //{
    //    direction = new Vector3(-direction.x * maxHorizontal, -direction.y * maxVertical, direction.z);
    //    transform.rotation = Quaternion.Lerp(
    //        transform.rotation, new Quaternion(
    //            direction.y, transform.rotation.y, direction.x, transform.rotation.w), Time.deltaTime * smooothness);
    //}
    #endregion
    #region second
    //private float rotSpeedX = 3.0f;
    //private float rotSpeedY = 1.5f;
    //float baseSpeed = 10f;

    //private void Update()
    //{
    //    if (!_joystick) return;
    //    Vector3 moveVector = transform.forward * baseSpeed;
    //    Vector3 inputs = _joystick.Direction;

    //    Vector3 yaw = inputs.x * transform.right * rotSpeedX * Time.deltaTime;
    //    Vector3 pitch = inputs.y * transform.up * rotSpeedY * Time.deltaTime;
    //    Vector3 dir = yaw + pitch;

    //    float maxX = Quaternion.LookRotation(moveVector + dir).eulerAngles.x;
    //    if (maxX < 90 && maxX > 70 || maxX > 270 && maxX < 290) { }
    //    else
    //    {
    //        moveVector += dir;
    //        transform.rotation = Quaternion.LookRotation(moveVector);

    //    }
    //    Move(moveVector * Time.deltaTime);
    //}
    //private void Move(Vector3 move)
    //{
    //    transform.position += move;
    //}
    #endregion
    void ActivateMovement()
    {
        isFlying = true;
    }
    void Fly()
    {
        if (playerController.currentPlayer != PlayerType.Aircraft) return;
        //    transform.DOMove(AirPosition.position, 1f).SetEase(Ease.Linear).OnComplete(() => ActivateMovement());
        //transform.DORotate(AirPosition.position, 3f, RotateMode.Fast).OnComplete(() =>
        //{
        transform.DORotate(transform.forward, .5f, RotateMode.Fast).SetSpeedBased(false).OnComplete(() =>
        {
            transform.DOMove(AirPosition.position, 3f).SetEase(Ease.Linear).OnComplete(() => ActivateMovement());

        });

        
    }


    public float RotationSpeed = 2;
    public float Speed = 30;
    private void Update()
    {
        if (playerController.currentPlayer != PlayerType.Aircraft)
        {

            animator.SetTrigger("Stop");
            return;
        }

        Vector2 direction = joystick.Direction;
        animator.SetTrigger("Flying");

        Vector3 movementVector = new Vector3(direction.x, 0, direction.y);


        movementVector = movementVector * Time.deltaTime * Speed;
        float isMoving = (Input.GetMouseButton(0) == true) || Input.touchCount != 0 ? 1 : 0;

        transform.position += transform.forward * Time.deltaTime * Speed * isMoving;

        if (movementVector.magnitude != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation( transform.rotation * movementVector, Vector3.up), Time.deltaTime * RotationSpeed);
        }
    }
    public void Land()
    {
        
        transform.DORotate(-Vector3.forward, 3f, RotateMode.Fast).OnComplete(() =>
        {
            transform.DOMove(LandingPosition.position, 3f).SetEase(Ease.Linear).OnComplete(() =>
                playerController.SwitchPlayer(PlayerType.Stickman));
        });
    }
    private void OnDisable()
    {
        Unsubscribe();
    }
}
