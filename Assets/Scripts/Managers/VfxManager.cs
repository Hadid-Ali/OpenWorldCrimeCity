using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxManager : MonoBehaviour
{
    public GameObject punchHitVfx,
         kickHitVfx, bigExplosionVfx, smallEplosionVfx;

    public void TriggerSmallExplosionAt(Vector3 position)
    {
        if (IsInvoking("HideSmallExplosion"))
            CancelInvoke("HideSmallExplosion");
        this.smallEplosionVfx.transform.position = position;
        this.smallEplosionVfx.SetActive(true);
        Invoke("HideSmallExplosion", 1f);
    }

    public void TriggerBigExplosionAt(Vector3 position)
    {
        if (IsInvoking("HideBigExplosion"))
            CancelInvoke("HideBigExplosion");
        this.bigExplosionVfx.transform.position = position;
        this.bigExplosionVfx.SetActive(true);
        Invoke("HideBigExplosion", 1f);
    }

    public void HideBigExplosion()
    {
        this.bigExplosionVfx.SetActive(false);
    }

    void HideSmallExplosion()
    {
        this.smallEplosionVfx.SetActive(true);
    }

}
