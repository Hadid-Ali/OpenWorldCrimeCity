using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public Constant.GameEntity gameEntityName;

    public IEnumerator ToggleObjectWithDelay(GameObject G, float wait, bool b)
    {
        yield return new WaitForSeconds(wait);
        G.SetActive(b);
    }
}
