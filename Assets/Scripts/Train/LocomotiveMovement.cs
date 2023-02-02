using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class LocomotiveMovement : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 0.05f;
    //float distanceTravelled;
    public float startTime = 0;

    public float timeTravelled = 0;
    public DropArea DropArea;

    private bool enableMovement;

    private Payer payer;
    private DrainResource drainResource;
    public CreateTrainCollectables createTrainCollectables;
    private void Awake()
    {
        payer = GetComponent<Payer>();
        drainResource = GetComponent<DrainResource>();
        Subscribe();
    }
    void Start()
    {
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
            BeginMove();

        }
    }

    public void BeginMove()
    {
        timeTravelled = startTime;
        enableMovement = true;
        createTrainCollectables.SpawnCollectables();
    }
    void Update()
    {

        if (pathCreator != null && enableMovement)
        {
            timeTravelled += speed * Time.deltaTime;

            transform.position = pathCreator.path.GetPointAtTime(timeTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotation(timeTravelled, endOfPathInstruction);

            if (timeTravelled >= 1f)
            {
                drainResource.DrainResources();
                enableMovement = false;
            }


        }
    }
    void OnPathChanged()
    {
        //distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
    public float GetCurrentTimeTravelled()
    {
        return timeTravelled;
    }

    public void StopMovement()
    {
        speed = 0;
    }
    public void StartMovement()
    {
        speed = .1f;
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Station"))
    //        StopMovement();
    //}
    void Subscribe()
    {
        DropArea.AllResourcesTaken += StartMovement;
    }
    void Unsubscribe()
    {
        DropArea.AllResourcesTaken -= StartMovement;
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }
}
