using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType
{
    HIT,
    BULLET_HIT,
    SKIDS,
    BULLET_HIT_ON_STEEL_SURFACE,
        BLOOD_SPLAT,
        EXPLOSION
}

[System.Serializable]
public class Particle
{
    public ParticleType particleType;
    public GameObject [] particles;

    public bool hasCameraShake = false;

    public float particleShowTime;
    
}

public class Particles : MonoBehaviour
{
    public static Particles Instance;
    public List<Particle> particles;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowParticle(ParticleType type,Vector3 position)
    {
        Particle p = this.particles.Find(x => x.particleType == type);
        int index = Random.Range(0, p.particles.Length);

        GameObject g = p.particles[index];
        g.transform.position = position;
        g.SetActive(true);

        if (p.hasCameraShake)
            GameManager.instance.ShakeCamera();
        StartCoroutine(this.HideParticle(g,p.particleShowTime));
    }

    public IEnumerator HideParticle(GameObject G, float particleS)
    {
        yield return new WaitForSeconds(particleS);
        G.SetActive(false);
    }

}
