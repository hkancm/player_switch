using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public PlayerType currentPlayer;
    public List<Player> players;
    public Action OnPlayerChange;
    public Transform Stickman;
    public Transform Marina;
    private void Start()
    {
        if (currentPlayer == PlayerType.Stickman) UIManager.Instance.PanelEnabled(false);
    }
    public void SwitchPlayer(PlayerType playerType)
    {
        currentPlayer = playerType;
        foreach (var item in players)
        {
            var player = item.GetComponent<Player>();
            if (item.playerData.playerType == currentPlayer)
            {
                if (playerType == PlayerType.Yacht)
                {
                    Stickman.transform.position = Marina.position;
                }
                else
                {
                    Stickman.transform.parent = item.transform;
                    Stickman.transform.position += new Vector3(2, 0, 4);

                }
                player.ActivateMovement();
                

            }
            else
                player.DeactivateMovement();

            if (playerType != PlayerType.Stickman)
            {
                
                Stickman.gameObject.SetActive(false);
                UIManager.Instance.PanelEnabled(playerType, true);

            }
            else
            { 
                UIManager.Instance.PanelEnabled(false);
                Stickman.gameObject.SetActive(true);
            }
        }
        OnPlayerChange?.Invoke();
    }

    public void OnGetOut()
    {
        SwitchPlayer(PlayerType.Stickman);
        Stickman.gameObject.SetActive(true);
        UIManager.Instance.PanelEnabled(false);

        
    }
    public void GetOutYacht()
    {
        SwitchPlayer(PlayerType.Stickman);
        Stickman.gameObject.SetActive(true);
        UIManager.Instance.PanelEnabled(false);

    }

}
