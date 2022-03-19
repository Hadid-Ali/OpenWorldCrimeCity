using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionType
{
    Day,Night,ZombieAttack,CopBattle,None,MafiaMode
}

[System.Serializable]
public class EnvironmentCondition
{
    public Color environmentSkyColor = Color.yellow;
    public Color fogColor = Color.yellow;
    public float fogIntensity = -100;
    public bool hasFog = false;
    public AudioClip ambienceMusic,secondayMusic;
    public ConditionType conditionType;

}

[RequireComponent(typeof(AudioSource))]
public class EnvironmentAnimator : MonoBehaviour
{
    public List<EnvironmentCondition> conditions;

    public Color nullColor = Color.yellow;
    public float nullIntentsity = -100f,skyChangeRate  = 0.01f, fogChangeRate = 0.01f, fogColorRate = 0.01f;

    private float fogToApply,skyColorToApply,fogColorToApply;
    private bool applyFog, applySkyColor, applyFogColor;

    public Material skyMaterial;

    [SerializeField]
    private AudioSource sound,secondarySource;

    [SerializeField]
    private int secondsInGame;
    [SerializeField]
    private int minutesToNight = 5;

    private ConditionType currentEnvironmentCondition;

    private void Start()
    {
        this.sound = this.GetComponent<AudioSource>();
       // this.secondarySource = this.GetComponentInChildren<AudioSource>();

        this.ApplyConditionAbrupt(ConditionType.Day);

        StartCoroutine(this.TimeCheck());
    }

    IEnumerator TimeCheck()
    {
        while(true)
        {
            this.secondsInGame++;
            if (this.secondsInGame > 0 & (this.secondsInGame / 60) % 5 >= this.minutesToNight)
            {
                ConditionType conditionToApply = this.currentEnvironmentCondition == ConditionType.Day ? ConditionType.Night : this.currentEnvironmentCondition == ConditionType.Night ? ConditionType.Day : ConditionType.None;

                if(conditionToApply!= ConditionType.None)
                {
                    this.ApplyCondition(conditionToApply);
                }
                this.secondsInGame = 0;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void ApplyConditionAbrupt(ConditionType type)
    {

        EnvironmentCondition conditionToApply = this.conditions.Find(x => x.conditionType.Equals(type));
        this.currentEnvironmentCondition = type;

        if (conditionToApply.hasFog != RenderSettings.fog)
        {

            RenderSettings.fog = conditionToApply.hasFog;
        }

        if (conditionToApply.hasFog)
        {
            RenderSettings.fogDensity = 0f;
            RenderSettings.fogColor = Color.white;

            if (conditionToApply.fogColor != this.nullColor)
                RenderSettings.fogColor = conditionToApply.fogColor;

            if (conditionToApply.fogIntensity != this.nullIntentsity)
            {
                RenderSettings.fogDensity = conditionToApply.fogIntensity;
            }
        }

        if (conditionToApply.environmentSkyColor != this.nullColor)
        {
            string propertyName = "_Tint";
            if (this.skyMaterial.HasProperty("_Tint"))
            {
                propertyName = "_Tint";
            }
            else if (this.skyMaterial.HasProperty("_SkyTint"))
            {
                propertyName = "_SkyTint";
            }
            
            this.skyMaterial.SetColor(propertyName, conditionToApply.environmentSkyColor);
        }

        if (conditionToApply.ambienceMusic != null)
        {
            this.sound.clip = conditionToApply.ambienceMusic;
            this.sound.Play();
        }

        if (conditionToApply.secondayMusic != null)
        {
            this.secondarySource.clip = conditionToApply.secondayMusic;
            this.secondarySource.Play();
        }

        else
            this.secondarySource.Stop();

    }

    public void ApplyCondition(ConditionType type)
    {
        EnvironmentCondition conditionToApply = this.conditions.Find(x => x.conditionType.Equals(type));
        this.currentEnvironmentCondition = type;

        if (conditionToApply.hasFog!=RenderSettings.fog)
        {
            RenderSettings.fog = conditionToApply.hasFog ;
        }

        if(conditionToApply.hasFog)
        {
            RenderSettings.fogDensity = 0f;
            RenderSettings.fogColor = Color.white;
            if (conditionToApply.fogColor != this.nullColor)
                StartCoroutine(this.LerpValue(this.nullIntentsity, conditionToApply.fogColor, false, false, true));

            if(conditionToApply.fogIntensity!= this.nullIntentsity)
            {
                StartCoroutine(this.LerpValue(conditionToApply.fogIntensity, this.nullColor, true));
            }
        }

        if(conditionToApply.environmentSkyColor!=this.nullColor)
        {
            StartCoroutine(this.LerpValue(this.nullIntentsity, conditionToApply.environmentSkyColor, false, true, false));
        }

        if(conditionToApply.ambienceMusic!=null)
        {
            this.sound.clip = conditionToApply.ambienceMusic;
            this.sound.Play();
        }

        if (conditionToApply.secondayMusic != null)
        {
            this.secondarySource.clip = conditionToApply.secondayMusic;
            this.secondarySource.Play();
        }

        else
            this.secondarySource.Stop();
    }

    public IEnumerator LerpValue(float targetvalue,Color targetColor,bool changeFogDensity = false,bool changeSkyColor = false,bool changeFogColor = false)
    {
        while(true)
        {
            if(changeFogDensity)
            {
                RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, targetvalue, this.fogChangeRate * Time.deltaTime);

                if(Mathf.Abs(RenderSettings.fogDensity-targetvalue) < 0.001)
                {
                    yield break;
                }
            }

            if(changeSkyColor)
            {
                string propertyName = "_Tint";
                if (this.skyMaterial.HasProperty("_Tint"))
                {
                    propertyName = "_Tint";
                }
                else if(this.skyMaterial.HasProperty("_SkyTint"))
                {
                    propertyName = "_SkyTint";
                }

                Color col = this.skyMaterial.GetColor(propertyName);
                this.skyMaterial.SetColor(propertyName, Color.Lerp(col, targetColor, this.skyChangeRate * Time.deltaTime));

                if (this.skyMaterial.GetColor(propertyName)== targetColor)
                {
                    yield break;
                }
            }

            if(changeFogColor)
            {
                RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, targetColor, this.fogColorRate * Time.deltaTime);
                if (RenderSettings.fogColor == targetColor)
                    yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

}
