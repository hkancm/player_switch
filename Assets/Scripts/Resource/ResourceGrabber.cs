using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGrabber : MonoBehaviour
{
    public List<Vector3> StashPositions;
    public int XAmount;
    public int YAmount;
    public int ZAmount;
    public float Space;

    public Transform FirstStashPoint;
    public Transform test;
    public Collectable collectablePrefab;

    public List<Collectable> CollectedObjects;



    //private void Start()
    //{
    //    CreateStashPositions();
    //    GrabResource();
    //}
    //void CreateStashPositions()
    //{
    //    Vector3 pos = Vector3.zero;
    //    for (int i = 0; i < XAmount; i++)
    //    {
    //        for (int j = 0; j < YAmount; j++)
    //        {
    //            for (int k = 0; k < ZAmount; k++)
    //            {
    //                pos = FirstStashPoint.position + Vector3.right * Space * i + Vector3.forward * k * Space + Vector3.up * Space * j;
    //                StashPositions.Add(pos);
    //                //Instantiate(test, pos, Quaternion.identity, this.transform);
    //            }

    //        }
    //    }
    //}
    //private int index;
    //Vector3 GetStashPos()
    //{
    //    var newPos = StashPositions[index];
    //    index++;
    //    return newPos;
    //}
    //void GrabResource()
    //{
    //    StartCoroutine("CreateResource");
    //}
    //public bool TrainActive;
    //IEnumerator CreateResource()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(2);
    //        var collectable = Instantiate(collectablePrefab,  FirstStashPoint.transform);
    //        collectable.transform.localPosition = GetStashPos();
    //        CollectedObjects.Add(collectable);
    //    }
    //}


}
