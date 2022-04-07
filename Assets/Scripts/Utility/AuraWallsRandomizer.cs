using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraWallsRandomizer : MonoBehaviour
{
    public Material auraWallMaterial;
    public Vector2 wallRandomizerValues;

    public string textureKeyword = "_MainTex";

    public float randomizerRate = 5f;

    public bool useX = true;
    public bool useY = true;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.wallRandomizerValues.Set(this.useX ? this.wallRandomizerValues.x + Time.deltaTime * this.randomizerRate : 0, this.useY ? this.wallRandomizerValues.y + Time.deltaTime * this.randomizerRate : 0);
        this.auraWallMaterial.SetTextureOffset(this.textureKeyword,  this.wallRandomizerValues);
    }
}
