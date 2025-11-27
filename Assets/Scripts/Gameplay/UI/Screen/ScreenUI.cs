using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenUI : MonoBehaviour
{
    [Tooltip("要控制的目标对象")]
    public GameObject target; 
    [Tooltip("按下这个键时切换显示/隐藏")]
    public KeyCode toggleKey = KeyCode.X;   // 默认 X，可在 Inspector 修改
    private void Awake()
    {
        target = target.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            target.SetActive(!target.activeSelf);
        }
    }
}
