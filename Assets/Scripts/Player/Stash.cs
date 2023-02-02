using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stash : MonoBehaviour
{
    public List<Vector3> StashPositions;
    public int XAmount;
    public int YAmount;
    public int ZAmount;
    public float Space;
    public Transform CollectableParent;
    public List<Transform> PointList;
    public Transform FirstStashPoint;

    public List<Stashable> CollectedObjects;
    private void Awake()
    {
        CreateStashPoints();

    }
    
    void CreateStashPoints()
    {
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < XAmount; i++)
        {
            for (int j = 0; j < YAmount; j++)
            {
                for (int k = 0; k < ZAmount; k++)
                {
                    var point = new GameObject("point_" + (i + j + k).ToString()).transform;
                    point.position = FirstStashPoint.position + Vector3.right * Space*1.5f * i + Vector3.forward * k * Space + Vector3.up * Space * j;
                    point.SetParent(CollectableParent);
                    PointList.Add(point);

                    //pos = FirstStashPoint.position + Vector3.right * Space * i + Vector3.forward * k * Space + Vector3.up * Space * j;
                    //StashPositions.Add(pos);
                    //Instantiate(test, pos, Quaternion.identity, this.transform);
                }

            }
        }
    }
    private int index;
    Vector3 GetStashPos()
    {
        var newPos = StashPositions[index];
        index++;
        return newPos;
    }

    Transform GetStashPoint()
    {
        var point = PointList[index];
        index++;
        return point;
    }
    public void TakeResource(Collectable collectedObj)
    {
        if (CollectedObjects.Count >= XAmount * YAmount * ZAmount) return;
        var stashable = collectedObj.Collect();
        stashable.CollectStashable(GetStashPoint());
        CollectedObjects.Add(stashable);
    }
    public Stashable RemovedStashable()
    {
        if (CollectedObjects.Count <= 0)
            return null;

        var stashable = CollectedObjects[CollectedObjects.Count - 1];

        CollectedObjects.Remove(stashable);
        index--;
        stashable.transform.parent = null;

        return stashable;
    }
    public bool IsStashFull()
    {
        if (CollectedObjects.Count >= XAmount * YAmount * ZAmount)
            return true;
        else return false;
    }
}
