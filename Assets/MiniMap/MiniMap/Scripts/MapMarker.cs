using UnityEngine;
using UnityEngine.UI;

using System.Collections;

[AddComponentMenu("MiniMap/Map marker")]
public class MapMarker : MonoBehaviour
{

    #region Public

    /* Sprite that will be shown on the map
     */
    public Sprite markerSprite;

    /* Size of the marker on the map (width & height)
     */
    public float markerSize = 2f;

    /* Enables or disables this marker on the map
     */
    public bool isActive = true;

    public Image MarkerImage
    {
        get
        {
            return markerImage;
        }
    }

    #endregion

    #region Private

    private Image markerImage;
    public GameObject markerImageObject;

    public MapCanvasController controller;

    #endregion

    #region Unity methods

    void Start () {
        if (!markerSprite)
        {
            Debug.LogError(" Please, specify the marker sprite.");
        }markerImageObject = new GameObject("Marker");
        markerImageObject.AddComponent<MarkerImageBehaviour>();
        markerImageObject.GetComponent<MarkerImageBehaviour>().ParentObject = this.gameObject;
        markerImageObject.AddComponent<Image>();
        this.controller = GameManager.instance.radarController;
        if (!controller)
        {
    //        Destroy(gameObject);
    //        return;
        }
        markerImageObject.transform.SetParent(controller.MarkerGroup.MarkerGroupRect);
        markerImage = markerImageObject.GetComponent<Image>();
        markerImage.sprite = markerSprite;
        markerImage.rectTransform.localPosition = Vector3.zero;
        markerImage.rectTransform.localScale = Vector3.one;
        markerImage.rectTransform.sizeDelta = new Vector2(markerSize, markerSize);
        markerImage.gameObject.SetActive(false);
		markerImage.transform.localScale = new Vector3 (5, 5, 5);
	}

    public GameObject FollowRotationObject;
    public bool ForceRotation = true;

	void Update () {
//        MapCanvasController controller = MapCanvasController.Instance;
//        if (!controller)
//        {
//            return;
//        }
        this.controller.checkIn(this);
        if (!ForceRotation)
            return;
        if (!this.FollowRotationObject)
            markerImage.rectTransform.rotation = Quaternion.identity;
        else
            markerImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, -this.FollowRotationObject.transform.eulerAngles.y);
	}

    void OnDestroy()
    {
        if (markerImage)
        {
            Destroy(markerImage.gameObject);
        }
    }

    #endregion

    #region Custom methods

    public void show()
    {
        markerImage.gameObject.SetActive(true);
    }

    public void hide()
    {

        if(this.markerImage)
        markerImage.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        this.hide();
    }

    public bool isVisible()
    {
        return markerImage.gameObject.activeSelf;
    }

    public Vector3 getPosition()
    {
        return gameObject.transform.position;
    }

    public void setLocalPos(Vector3 pos)
    {
        markerImage.rectTransform.localPosition = pos;

    }

    public void setOpacity(float opacity)
    {
        markerImage.color = new Color(1.0f, 1.0f, 1.0f, opacity);
    }

    #endregion
}
