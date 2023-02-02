using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DrainResource : MonoBehaviour
{


    public DropArea dropArea;
    public Stash stash;
    private void Start()
    {
        //CreateResourcePositions();
        stash = GetComponent<Stash>();
    }

    public void DrainResources()
    {
        StartCoroutine(DrainRoutine());

    }

    private void SpawnCollectable(Vector3 pos)
    {
        var collectable = Instantiate(dropArea.CollectablePrefab);
        collectable.InitializeDirect();
        collectable.transform.position = pos;
        collectable.transform.eulerAngles = Vector3.forward * 50f;
        collectable.DropArea = dropArea;
        dropArea.collectables.Add(collectable);
       
    }
    IEnumerator DrainRoutine()
    {
        for (int i = stash.CollectedObjects.Count - 1; i >= 0; i--)
        {
            yield return new WaitForSeconds(.2f);
            var stashable = stash.RemovedStashable();
            var pos = dropArea.GetPos();
            stashable.transform.DOJump(pos, 3, 1, .5f).OnComplete(() =>
            {
                SpawnCollectable(pos);

                stashable.gameObject.SetActive(false);
            });
        }

    }

}
