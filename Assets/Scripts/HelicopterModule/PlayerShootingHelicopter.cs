using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingHelicopter : HelicopterController
{
    [SerializeField]
    private Transform positionForPlayer;
    
    public override void OnHelicopterEnable()
    {
        this.SetPlayerParent();
    }

    public override void OnAssignTarget()
    {
        this.SetPlayerParent();
    }

    public void SetPlayerParent()
    {
        GameManager.instance.playerController.SetParent(this._selfTransform, this.positionForPlayer, PlayerPositionMode.Aim);
    }
}
