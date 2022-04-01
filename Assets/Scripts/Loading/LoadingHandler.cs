using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingHandler : MonoBehaviour
{
    public Image fillerImg;
    public Text fillerTxt;

    public AsyncOperation operation;

    private float dummyfill;
    private float speed = 5f;

<<<<<<< Updated upstream
    public delegate void OnLoadingComplete();
    public static OnLoadingComplete OnLoadingCompleteEvent;
=======
>>>>>>> Stashed changes
    void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        dummyfill = 0;
        UpdateUI();

        operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        while (dummyfill < 100)
        {
            yield return new WaitForEndOfFrame();
            dummyfill += speed * Time.deltaTime;
            UpdateUI();
            if (operation.isDone)
            {
                speed = 50;
            }
        }
        dummyfill = 100;
        UpdateUI();

        yield return new WaitForSeconds(2);
<<<<<<< Updated upstream
        if (OnLoadingCompleteEvent != null)
        {
            OnLoadingCompleteEvent();
        }
=======
>>>>>>> Stashed changes
        Destroy(gameObject);
    }

    private void UpdateUI()
    {
        fillerImg.fillAmount = dummyfill/100;
<<<<<<< Updated upstream
        fillerTxt.text = Mathf.FloorToInt(dummyfill)+"%";
=======
        fillerTxt.text = string.Format("{0}%", dummyfill);
>>>>>>> Stashed changes
    }
}