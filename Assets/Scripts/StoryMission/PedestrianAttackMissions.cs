using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianAttackMissions : Mission
{
    public float numberOfPedestriansToAttack = -100;
    public float numberOfPedestrians = 0;

    public override void Start()
    {
        base.Start();
        this.isPedestrianAttackMission = true;
        this.isTutorialMission = true;
    }

    public override void OnEnemyNeutralized()
    {
        this.numberOfPedestrians++;

        if(this.numberOfPedestriansToAttack>0)
        {
            if(this.numberOfPedestrians>=this.numberOfPedestriansToAttack)
            {
                this.MissionComplete();
            }
        }
    }
}
