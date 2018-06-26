using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Transform uiCanvasTrans;


    private GameObject menuUI;
    private EasyTween[] uiButtonEventTween;

    // Use this for initialization
    void OnEnable() {

        menuUI = GameObject.Instantiate(Resources.Load("UI/MenuUI") as GameObject);

        menuUI.transform.parent = uiCanvasTrans;
        menuUI.transform.localPosition = Vector3.zero;
        menuUI.transform.localScale = Vector3.one;

        uiButtonEventTween = menuUI.GetComponentsInChildren<EasyTween>();

        StartCoroutine(Iestartcoroutine());
    }

    IEnumerator Iestartcoroutine()
    {
        yield return new WaitForSeconds(0.001f);
        foreach (EasyTween tween in uiButtonEventTween)
        {
            tween.SetStartValues();
            tween.OpenCloseObjectAnimation();

            UnityEngine.UI.Button buttonevent = tween.gameObject.GetComponent<UnityEngine.UI.Button>();
            buttonevent.onClick.AddListener(OnButtonEvent);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnButtonEvent()
    {
        Debug.Log(name);
    }
}
