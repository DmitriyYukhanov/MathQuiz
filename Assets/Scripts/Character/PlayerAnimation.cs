using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {


    AniStateController controller;

    // Use this for initialization
    void Start () {
        Animator anim = gameObject.GetComponent<Animator>();        
        controller = new AniStateController(anim);
    }

    // Update is called once per frame
    void Update()
    {
        controller.Update(Time.deltaTime);
    }

    void OnEvent(int parameter)
    {
        controller.OnEvent(parameter);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Start");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision2" + gameObject.name);
    }
    
}





