using UnityEngine;
using System.Collections;

public class RFX4_CameraShake : MonoBehaviour
{
    public AnimationCurve ShakeCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    public Transform cameraTransform;

    public vThirdPersonCamera cameraController;

    public float Duration = 2;
    public float Speed = 22;
    public float Magnitude = 1;
    public float DistanceForce = 100;
    public float RotationDamper = 2;
    public bool IsEnabled = true;

    bool isPlaying;
    [HideInInspector]
    public bool canUpdate;
    
    private void Start()
    {
        
    }

    void OnEnable()
    {
        isPlaying = true;
        canUpdate = true;

        if (this.cameraController)
            this.cameraController.enabled = false;
    }

    void FixedUpdate()
    {
        var elapsed = 0.0f;
        var originalCamRotation = cameraTransform.rotation.eulerAngles;
        var direction = (transform.position - cameraTransform.position).normalized;
        var time = 0f;
        var randomStart = Random.Range(-1000.0f, 1000.0f);
        var distanceDamper = 1 - Mathf.Clamp01((cameraTransform.position - transform.position).magnitude / DistanceForce);
        Vector3 oldRotation = Vector3.zero;
        while (elapsed < Duration && canUpdate)
        {
            elapsed += Time.deltaTime;
            var percentComplete = elapsed / Duration;
            var damper = ShakeCurve.Evaluate(percentComplete) * distanceDamper;
            time += Time.deltaTime * damper;
            cameraTransform.position -= direction * Time.deltaTime * Mathf.Sin(time * Speed) * damper * Magnitude / 2;

            var alpha = randomStart + Speed * percentComplete / 10;
            var x = Mathf.PerlinNoise(alpha, 0.0f) * 2.0f - 1.0f;
            var y = Mathf.PerlinNoise(1000 + alpha, alpha + 1000) * 2.0f - 1.0f;
            var z = Mathf.PerlinNoise(0.0f, alpha) * 2.0f - 1.0f;

            if (Quaternion.Euler(originalCamRotation + oldRotation) != cameraTransform.rotation)
                originalCamRotation = cameraTransform.rotation.eulerAngles;
            oldRotation = Mathf.Sin(time * Speed) * damper * Magnitude * new Vector3(0.5f + y, 0.3f + x, 0.3f + z) * RotationDamper;
            cameraTransform.rotation = Quaternion.Euler(originalCamRotation + oldRotation);
        }
        if (this.cameraController)
            this.cameraController.enabled = true;
    }
}
