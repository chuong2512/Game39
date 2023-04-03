using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckJump : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (GameUI.Instance.currentState != State.Stop)
        {
            GameUI.Instance.SetState(State.Running);
            
            if (col.gameObject.CompareTag("Col"))
            {
                Player.Instance.Jump();
            }
            else if (col.gameObject.CompareTag("Point"))
            {
                Player.Instance.ForrceJump();
                col.gameObject.SetActive(false);
            }
        }
    }
}