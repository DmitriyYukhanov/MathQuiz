using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : UIAbstract
{    
    // button tweens
    [SerializeField]
    private EasyTween[] uiButtonEventTween;

    // Use this for initialization
    void Start () {

        uiButtonEventTween = GetComponentsInChildren<EasyTween>();
        
    }

    // enable active on
    private void OnEnable()
    {
        StartCoroutine(IEShowButton());
    }

    // Update is called once per frame
    void Update () {
		
	}

    // show button
    IEnumerator IEShowButton()
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

    // button event
    private void OnButtonEvent()
    {
        Debug.Log(name);
    }
}
