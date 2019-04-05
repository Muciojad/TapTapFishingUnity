using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashBubbleController : MonoBehaviour
{

   public void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
