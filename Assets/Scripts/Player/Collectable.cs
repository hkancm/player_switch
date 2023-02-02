using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectable : MonoBehaviour
{

    public Stashable stashablePrefab;

    private Collider coll;

    public DropArea DropArea { get; set; }
    private void Awake()
    {
        coll = GetComponent<Collider>();
        coll.enabled = false;
    }
    public Stashable Collect()
    {
        if (DropArea)
        {
            DropArea.RemoveList(this);
        }
        

        var stashable = Instantiate(stashablePrefab, null);
        stashable.transform.position = transform.position + Vector3.up * 1.5f;
        coll.enabled = false;

        transform.localScale = Vector3.zero;
        if(!DropArea)
            Respawn();
        return stashable;
    }

    public void Respawn()
    {
        StartCoroutine(SetActiveRoutine());

    }
    private IEnumerator SetActiveRoutine()
    {
        yield return new WaitForSeconds(5f);
        InitializeScaling();
    }

    public void InitializeScaling()
    {
        transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack, 2.5f).OnComplete(() =>
        {
            Enablecoll();
            transform.DORotate(Vector3.up * 360f, 5f, RotateMode.LocalAxisAdd).SetLoops(-1);
        });
    }

    public void InitializeDirect()
    {
        Enablecoll();
    }

    public void Enablecoll()
    {
        coll.enabled = true;

    }
}
