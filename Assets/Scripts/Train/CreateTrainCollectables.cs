using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CreateTrainCollectables : MonoBehaviour
{
    public List<Transform> SpawnPointList;
    public Collectable collectablePrefab;
  //  public List<Collectable> Collectables;

    public int SpawnAmount;
    private void Start()
    {
        //SpawnCollectables();

    }
    public void SpawnCollectables()
    {
        for (int i = 0; i < SpawnAmount; i++)
        {
            if (i % 2 != 0)
            {
                continue;
            }
            var resource = Instantiate(collectablePrefab,SpawnPointList[i].position, Quaternion.identity, this.transform);
            //Collectables.Add(resource);
            resource.InitializeScaling();
            resource.transform.DORotate(Vector3.up * 360f, 5f, RotateMode.LocalAxisAdd).SetLoops(-1);
        }
        
    }
    
}
