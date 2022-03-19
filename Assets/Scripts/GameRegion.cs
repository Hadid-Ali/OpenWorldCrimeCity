using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRegion : MonoBehaviour
{
    public Wave enemyWaveToStart;
    public bool drawGizmo = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.instance.regionManager.canFight)
            return;
        this.gameObject.SetActive(false);
        GameManager.instance.regionManager.Count++;
        if(other.CompareTag(Constant.TAGS.PLAYER))
        {
            if (this.enemyWaveToStart.entityType != EntityType.NONE)
                GameManager.instance.playerWavesSpawner.StartWave(this.enemyWaveToStart);
        }
    }

    private void OnDrawGizmos()
    {
        if(this.drawGizmo)
        {

        Gizmos.color = Color.magenta;
        BoxCollider c = this.GetComponent<BoxCollider>();
        Gizmos.DrawCube(this.transform.position, c.size);

        }
    }
}
