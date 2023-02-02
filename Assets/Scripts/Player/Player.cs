using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerData playerData;
    public bool isCurrentPlayer = false;
    public Rigidbody rb;
    private void Awake()
    {
        TryGetComponent<Rigidbody>(out rb);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerSwitchArea"))
        {
            var player = other.GetComponentInParent<Player>();
            playerController.SwitchPlayer(player.playerData.playerType);
            //player.transform.parent = this.transform;
        }
    }
    
    public void ActivateMovement()
    {
        isCurrentPlayer = true;
        UIManager.Instance.PanelEnabled(playerData.playerType, true);
    }
    public void DeactivateMovement()
    {
        isCurrentPlayer = false;
        if (!rb) Debug.Log(gameObject.name, gameObject);
        rb.isKinematic = true;
    }
}
