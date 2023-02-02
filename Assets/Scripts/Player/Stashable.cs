using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class Stashable : MonoBehaviour
{
    public void CollectStashable(Transform target)
    {
        transform.parent = target;
        transform.DOLocalJump(Vector3.zero, 3, 1, .5f).SetSpeedBased(true).OnComplete(() => {
            transform.localRotation = Quaternion.identity;
        });

    }
    public void PayStashable(Transform target, Action onCompletePay)
    {
        transform.DOLocalJump(target.position, 3, 1, 1).SetSpeedBased(true).OnComplete(() => {
            transform.localRotation = Quaternion.identity;
            onCompletePay?.Invoke();
            Destroy(gameObject);
        });
    }

}
