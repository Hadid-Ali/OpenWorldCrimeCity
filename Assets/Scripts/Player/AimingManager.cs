using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerAiming
{
    public void AimWeapon(bool toggle);

}

public class AimingManager : MonoBehaviour,PlayerAiming
{

    public Transform spine,transformHipBone;
    public HumanBodyBones aimingBone;

    public Vector3 AimOffsetVector, aimingAtPoint;

    public float AimPoint;
    public float smoothing = 15f;
    
    public bool IsAiming;
    public bool testAim;

    private PlayerController playerController;
    private Camera mainCam;
    
    public float raycastWait = 0.1f;
    public LayerMask aimMask;
    public GameObject aimedObject;
    private Coroutine routine;

    [SerializeField]
    private bool canFocusClose = false;
    
    void Start()
    {
        this.playerController = this.GetComponent<PlayerController>();
        this.spine = this.playerController.animatorController.GetAnimatorBone(this.aimingBone);
        this.transformHipBone = this.playerController.animatorController.GetAnimatorBone(HumanBodyBones.Hips);
        this.mainCam = GameManager.instance.cameraManager._mainCamera;
    }

    void OnEnable()
    {
        Invoke("StartAiming", 0.5f);
    }

    void StartAiming()
    {
        this.routine = StartCoroutine(this.RaycastRoutine());
    }

    public IEnumerator RaycastRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.raycastWait);
            this.AimRaycast();
        }
    }

    public void AimWeapon(bool toggle)
    {
        this.canFocusClose = toggle;
    }

    public void AimRaycast()
    {
        this.aimedObject = null;
        /*
        Ray R = this.mainCam.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
        RaycastHit hit;
        if(Physics.Raycast(R,out hit,40f,this.aimMask))
        {
            this.aimedObject = hit.transform.gameObject;
            this.aimingAtPoint = hit.point;
            GameManager.instance.gameplayHUD.ChangeCrosshair(this.aimedObject);
        }
        */

        Vector3 R = this.mainCam.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f,0));
        RaycastHit[] hit = Physics.RaycastAll(R,mainCam.transform.forward, 40f, this.aimMask);
        if (hit.Length>0)
        {
            bool isHeadhot = false;
            RaycastHit hitObj=new RaycastHit();
            foreach (var item in hit)
            {
                if (item.transform.CompareTag(Constant.TAGS.HEAD))
                {
                    hitObj = item;
                    isHeadhot = true;
                    Debug.LogWarning("Headshot");
                    break;
                }
            }
            if (!isHeadhot)
            {
                foreach (var item in hit)
                {
                    if (item.transform.CompareTag(Constant.TAGS.ENEMY))
                    {
                        hitObj = item;
                        break;
                    }
                }
            }
            this.aimedObject = hitObj.transform.gameObject;
            this.aimingAtPoint = hitObj.point;
            GameManager.instance.gameplayHUD.ChangeCrosshair(this.aimedObject);
        }

        else
        {
            this.aimingAtPoint = Vector3.zero;
            GameManager.instance.gameplayHUD.ChangeCrosshair(null);
            GameplayHUD.Instance.IsHeadShot = false;
        }
    }

    public void AimWithWeapon()
    {
        Vector3 v = this.transform.eulerAngles;

        this.transform.eulerAngles = new Vector3(v.x, GameManager.instance.mainCamera.transform.eulerAngles.y, v.z);
        this.AimAtTarget(this.AimOffsetVector, this.AimPoint);
    }

    void LateUpdate()
    {
        if ((GameplayHUD.isShooting | this.testAim | this.canFocusClose) & this.playerController.CanAim)
            this.Aim(this.canFocusClose);
        else
            this.DisableAim();
    }

    public void ToggleAim(bool b)
    {
        this.IsAiming = b;
        this.playerController.animatorController.SetAim(b);
    }

    public void DisableAim()
    {
        this.IsAiming = false;
        this.playerController.animatorController.SetAim(false);
        GameManager.instance.mainCamera.ChangeCameraMode(CameraMode.Orbit, true);
        GameManager.instance.gameplayHUD.ToggleCrosshair(false);

    }


    public bool isOverrideAiming = false;

    public void Aim(bool canFocusClose)
    {
        this.IsAiming = true;
        this.playerController.animatorController.SetAim(true);
        this.AimWithWeapon();
        float sb = this.FixedEulerAngle(this.spine.transform.localEulerAngles).y;
        float hb = this.FixedEulerAngle(this.transformHipBone.transform.localEulerAngles).y;
        GameManager.instance.mainCamera.ChangeCameraMode(CameraMode.Aim, canFocusClose);
        GameManager.instance.gameplayHUD.ToggleCrosshair(true);

        if (sb - hb > 60f)
        {

        }
    }


    public Vector3 FixedEulerAngle(Vector3 v)
    {
        float x = v.x >= 180 ? 360 - v.x:v.x;
        float y = v.y >= 180 ? 360 - v.y : v.y;
        float xy = v.z >= 180 ? 360 - v.z : v.z;

        return new Vector3(x, y, xy);
    }

    public Vector3 offset;

    public float yoffset = 20f;
    private void AimAtTarget(Vector3 vector, float point)
    {
        Ray ray = new Ray(GameManager.instance.mainCamera.transform.position, GameManager.instance.mainCamera.transform.forward);
        Vector3 lookPosition = ray.GetPoint(point);

        this.spine.LookAt(lookPosition);
        this.spine.rotation = this.spine.rotation * Quaternion.Euler(vector);
        //this.spine.Rotate(vector, Space.Self);
    }

}
