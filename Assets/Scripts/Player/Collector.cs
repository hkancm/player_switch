using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Stash))]
public class Collector : MonoBehaviour
{
    private Stash stash;
    private void Awake()
    {
        stash = GetComponent<Stash>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            if (other.TryGetComponent(out Collectable collected))
                stash.TakeResource(collected);
            //else if (other.TryGetComponent(out Stashable stashed))
            //    stash.TakeResource(collected);
        }

        //else if (other.CompareTag("TakeArea"))
        //{
        //    if (other.GetComponentInParent<DropArea>())
        //    {
        //        var dropArea = other.GetComponentInParent<DropArea>();
        //        if (dropArea.CollectedResourceCount <= 0 || stash.IsStashFull()) return;
        //        var stashable = dropArea.CreateStashable();
        //        stash.TakeResource(stashable);
        //    }
        //}
    }
}
