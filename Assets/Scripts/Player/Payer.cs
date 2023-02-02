using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stash))]
public class Payer : MonoBehaviour
{
    private Stash _stash;
    private float nextTimeToPay = 0;
    private float paymentDelay = 0.1f;

    //public DropArea dropArea;
    private void Awake()
    {
        _stash = GetComponent<Stash>();
        nextTimeToPay = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_stash.CollectedObjects.Count <= 0)
            return;


        if (other.CompareTag("Unlockable"))
        {
            nextTimeToPay = Time.time + paymentDelay;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_stash.CollectedObjects.Count <= 0)
            return;


        if (other.CompareTag("Unlockable"))
        {
            if (Time.time < nextTimeToPay)
                return;

            nextTimeToPay = Time.time + paymentDelay;

            if (other.TryGetComponent(out UnlockArea unlockable))
            {
                StartPayment(unlockable);
            }
        }
        //if (other.CompareTag("DropArea"))
        //{
        //    if (Time.time < nextTimeToPay)
        //        return;

        //    nextTimeToPay = Time.time + paymentDelay;

        //    if (other.TryGetComponent(out DropArea unlockable))
        //    {
        //        StartDrop(unlockable);
        //    }
        //}
    }

    private void StartPayment(UnlockArea unlockable)
    {
        if (unlockable.unlockableData.RemainingPrice <= 0)
            return;

        var stashable = _stash.RemovedStashable();
        if (stashable == null)
            return;

        unlockable.Pay(stashable);
    }
    //public void Drop()
    //{
    //    //if ( _stash. <=0)
    //    //    return; 
    //    var stashable = _stash.RemovedStashable();
    //    if (stashable == null)
    //        return;

    //    dropArea.Drain(stashable);
    //}

}