using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityMethods : MonoBehaviour
{
   public static Vector3 ClampedAngle(Vector3 v)
    {
        v.x = v.x > 180 ? v.x - 360 : v.x;
        v.y = v.y > 180 ? v.y - 360 : v.y;
        v.z = v.z > 180 ? v.z - 360 : v.z;
        
        return v;
    }

    public static void StopRoutines()
    {
        
    }

    private static char[] c;
    public static IEnumerator TypeText(string text,float time,UnityEngine.UI.Text uiText)
    {
        uiText.text = "";
        c = ((text.TrimStart()).TrimEnd()).ToCharArray();
        Debug.LogError(text);
        float perCharTime = time / c.Length;
        
        for(int i=0;i<c.Length;i++)
        {
            uiText.text += c[i].ToString();
            yield return new WaitForSeconds(perCharTime);
        }
    }
}
