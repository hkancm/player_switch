using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCollectable : MonoBehaviour
{
    public Stashable stashablePrefab;
    private void Update()
    {

    }
    public Stashable Collect()
    {
        var stashable = Instantiate(stashablePrefab, null);
        stashable.transform.position = transform.position + Vector3.up * 1.5f;
        GetComponent<Collider>().enabled = false;
        return stashable;
    }
}
