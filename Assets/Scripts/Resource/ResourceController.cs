using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ResourceController : MonoBehaviour
{
    public Collectable collectablePrefab;
    public List<Collectable> SpawnedCollectables;
    public List<Collectable> AllCollectables;
    public int MaxSpawnCount = 10;
    public float SpawnPeriod = 2f;
    private float nextSpawnTime = 0;
    private void Start()
    {
        CreateResources();
    }
    private void Update()
    {
        HandleNullElements();
        if (SpawnedCollectables.Count >= MaxSpawnCount) return;
        if(Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + SpawnPeriod;
            Spawn();


        }
    }
    public float XSize, YSize;
    public float space;
    void CreateResources()
    {
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < XSize; i++)
        {
            for (int j = 0; j < YSize; j++)
            {
                pos = transform.position + Vector3.right * space * i + Vector3.forward * j * space;

                var resource = Instantiate(collectablePrefab, pos, Quaternion.identity, this.transform);
                resource.transform.localScale = Vector3.zero;
                AllCollectables.Add(resource);
                
            }
        }
    }
    void Spawn()
    {
        if (AllCollectables.Count == 0) return;
        var i = Random.Range(0, AllCollectables.Count);
        var collectable = AllCollectables[i];
        SpawnedCollectables.Add(collectable);
        collectable.InitializeScaling();
        //collectable.transform.DOScale(1f, .5f).SetEase(Ease.OutBack, 2.5f).OnComplete(col);
        //collectable.transform.DORotate(Vector3.up * 360f, 5f, RotateMode.LocalAxisAdd).SetLoops(-1);
        AllCollectables.RemoveAt(i);
    }

    
    void HandleNullElements()
    {
        for (int i = SpawnedCollectables.Count - 1; i >= 0; i--)
        {
            if (SpawnedCollectables[i] == null)
            {
                SpawnedCollectables.RemoveAt(i);
            }
        }
    }
}
