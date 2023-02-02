using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SeaResourceController : MonoBehaviour
{
    public Collectable ResourcePrefab;
    public List<Vector3> positions;
    public float Max;
    public float Min;
    public int ResorceCount;
    List<Collectable> Resources;
    private float nextSpawnTime = 0;
    public float SpawnPeriod = 2f;
    public List<Collectable> SpawnedCollectables;
    public List<Collectable> AllCollectables;
    private void Start()
    {
        SetResources();
    }
    public void SetResources()
    {
        for (int i = 0; i < ResorceCount; i++)
        {
            AddNewResource();
        }
    }
    Vector3 SelectRandomPosition()
    {
        return new Vector3(Random.Range(Min, Max), 0, Random.Range(Min, Max));
    }
    public void AddNewResource()
    {
        Vector3 pos = transform.position;
        pos = SelectRandomPosition() + transform.position;
        positions.Add(pos);

        
        var collectable = Instantiate(ResourcePrefab, pos, Quaternion.identity, transform);
        collectable.transform.localScale = Vector3.zero;
        AllCollectables.Add(collectable);

    }
    private void Update()
    {
        if (SpawnedCollectables.Count >= positions.Count) return;
        if (Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + SpawnPeriod;
            Spawn();


        }
    }
    void Spawn()
    {
        if (AllCollectables.Count == 0) return;
        var i = Random.Range(0, AllCollectables.Count);
        var collectable = AllCollectables[i];
        SpawnedCollectables.Add(collectable);
        collectable.transform.DOScale(1f, .5f).SetEase(Ease.OutBack, 2.5f);
        collectable.InitializeScaling();
        AllCollectables.RemoveAt(i);
        collectable.transform.DORotate(Vector3.up * 360f, 5f, RotateMode.LocalAxisAdd).SetLoops(-1);
    }

}
