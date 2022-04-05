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


    public static IEnumerator Coroutine_InvokeEvent(EventWithDelay eventWithDelay)
    {
        yield return new WaitForSeconds(eventWithDelay.durationBeforeInvoke);
        eventWithDelay.eventToInvoke.Invoke();
    }

    public static Vector3 FixEulerAngles(Vector3 sourceAngle) => new Vector3(sourceAngle.x >= 180 ? sourceAngle.x - 360 : sourceAngle.x, sourceAngle.y >= 180 ? sourceAngle.y - 360 : sourceAngle.y, sourceAngle.z >= 180 ? sourceAngle.z - 360 : sourceAngle.z);

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
