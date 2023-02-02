using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System;

public class DropArea : MonoBehaviour
{
    public int CollectedResourceCount;
    public Transform FirstPos;
    public Stashable StashablePrefab;
    public Collectable CollectablePrefab;
    public Action AllResourcesTaken;

    public List<Collectable> collectables;
    public LocomotiveMovement LocomotiveMovement;

    private void Start()
    {
        CreateResourcePositions();
    }
    public void Drain(Stashable stashable)
    {

        //stashable.DrainStashable(FirstPos);
        //var collectable = Instantiate(CollectablePrefab, transform);

        //collectable.transform.DOJump(FirstPos.position, 3, 1, 1).SetSpeedBased(true);//.OnComplete(()=> Destroy(collectable.gameObject));
        //CollectedResourceCount++;
    }
    
    public int XSize, YSize;
    public float space;
    public List<Collectable> drianedCollectables;

    public Transform DrainPoint;
    public List<Vector3> Positions;

    void CreateResourcePositions()
    {
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < XSize; i++)
        {
            for (int j = 0; j < YSize; j++)
            {
                pos = DrainPoint.position + Vector3.right * space * 1.5f* i + Vector3.forward * j * space;

                Positions.Add(pos);
            }
        }
    }

    int index = 0;
    public Vector3 GetPos()
    {
        if (index >= Positions.Count)
        {
            Debug.LogError("need more available pos");
            return Vector3.zero;
        }
        var newPos = Positions[index];

        index++;
        return newPos;
    }
    public void RemoveList(Collectable collectable)
    {
        collectables.Remove(collectable);
        if (collectables.Count == 0)
        {
            LocomotiveMovement.BeginMove();
            
        }
        index--;
    }
}
