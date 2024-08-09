
using Jelly;
using Jelly.Core;
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
            UIManager.Instance.interactionUI.alpha = 0;
        }
        catch
        {

        }
    }
}
