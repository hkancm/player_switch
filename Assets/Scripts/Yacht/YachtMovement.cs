using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YachtMovement : MonoBehaviour
{
    public VariableJoystick _joystick;
    #region firstWay
    //public float Speed = 10f;
    //public Transform end;
    //Rigidbody rb;
    //public float ForwardSpeed = 100;
    //public float ForwardSpeedMultiplier = 1000;
    //public float SpeedMult=4;
    //public float smooothness = 5;

    //public float maxVertical = 0.1f;
    //public float maxHorizontal = 0.06f;
    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}
    //private void Update()
    //{
    //    if (!_joystick) return;
    //    HandleRotation(_joystick.Direction);
    //}
    //private void FixedUpdate()
    //{
    //    if (!_joystick) return;
    //    HandleMovement(_joystick.Direction);
    //}
    //void HandleMovement(Vector3 direction)
    //{
    //    rb.velocity = new Vector3(rb.velocity.x , rb.velocity.y, direction.y * ForwardSpeedMultiplier * Time.deltaTime);
    //    float xVelocity = direction.x * SpeedMult * Time.deltaTime;
    //    float yVelocity = -direction.y * SpeedMult * Time.deltaTime;

    //    rb.velocity = Vector3.Lerp(
    //        rb.velocity,
    //        new Vector3(xVelocity,  yVelocity, rb.velocity.z),
    //        Time.deltaTime * smooothness
    //        ); 

    //}
    //void HandleRotation(Vector3 direction)
    //{
    //    direction = new Vector3(-direction.x * maxHorizontal, -direction.y * maxVertical, direction.z);
    //    transform.rotation = Quaternion.Lerp(
    //        transform.rotation, new Quaternion(
    //            direction.y, transform.rotation.y, direction.x, transform.rotation.w), Time.deltaTime*smooothness);
    //}
    #endregion
    public float rotSpeedX = 20.0f;
    private float rotSpeedY = 1.5f;
    public float baseSpeed = 30f;
    Player player;
    public Transform Marina;
    public PlayerController playerController;
    public ParticleSystem SeaParticle;
    private void Awake()
    {
        player = GetComponent<Player>();
    }
    //int isMoving;
    private void Update()
    {
        if (!_joystick || !player.isCurrentPlayer) return;
        float isMoving = (Input.GetMouseButton(0) == true) || Input.touchCount != 0 ? 1 : 0;

        
        Vector3 moveVector = transform.forward * baseSpeed * isMoving;
        Vector3 inputs = _joystick.Direction;

        Vector3 dir = inputs.x * transform.right * rotSpeedX * Time.deltaTime;
        
        Vector3 lookDirection = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
        if (lookDirection.magnitude != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.rotation * lookDirection, Vector3.up), Time.deltaTime * rotSpeedX);
        }

        Move(moveVector * Time.deltaTime);
    }
    private void Move(Vector3 move)
    {
        transform.position += move;

    }
    public void Land()
    {
        SeaParticle.Stop();
        transform.DOMove(Marina.position, 2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DORotate(transform.forward, 1f, RotateMode.Fast).OnComplete(() =>
                playerController.SwitchPlayer(PlayerType.Stickman));
        });
    }
}
