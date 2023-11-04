using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Constructor
    public static UIManager Instance;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    #endregion

    public Slider Health;
    public Slider Stamina;
    public Slider Shield;

    [Space(5)]
    public Slider SP_A;
    public Slider SP_B;
}
