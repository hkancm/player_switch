using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StickmanMovement : MonoBehaviour
{
    public VariableJoystick joystick;
    public float Speed;
    public float AnimationSpeed;
    StickmanAnimationController _animationController;
    NavMeshAgent _agent;
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animationController = GetComponentInChildren<StickmanAnimationController>();
    }
    private void Update()
    {
        if (!joystick ) return;
        if (!GetComponent<Player>().isCurrentPlayer) 
        {
            _animationController.PlayIdle();
            return;
        } 
        Vector3 move = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        move *= Time.deltaTime * Speed;
        _agent.Move(move);
        _agent.SetDestination(transform.position + move);
        if (move == Vector3.zero) 
            _animationController.PlayIdle();
        else
        {
            _animationController.PlayWalkAnimation(_agent.velocity.magnitude * AnimationSpeed * Time.deltaTime);

            Vector3 lookDirection = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 30 * Time.deltaTime);

        }
    }
}
