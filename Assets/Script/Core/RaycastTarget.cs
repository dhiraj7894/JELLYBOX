
using Game;
using UnityEngine;

public class RaycastTarget : MonoBehaviour
{
    ///
    /// 
    /// Ray cast targetor, don't write anycode unless if needed
    /// ask before writing any code
    /// 
    ///
    private void OnDestroy()
    {
        try
        {
            GetComponent<PressF_UI>().CanvasGroup.alpha = 0.0f;
        }
        catch
        {

        }
    }
}
