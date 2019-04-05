using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reward message controller.
/// Display TMP_Text component and fade it smooth.
/// </summary>
public class RewardMessageController : MonoBehaviour
{
    private float textAlpha;
    private TMPro.TMP_Text textRender;
    private void Awake()
    {
        textRender = GetComponent<TMPro.TMP_Text>();
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        // simple fade out reward text - it's still active and waits for new message to display
        if(textRender.color.a > 0)
        {
            textRender.color = new Color(textRender.color.r, textRender.color.g, textRender.color.b, textRender.color.a - (0.1f * Time.deltaTime));
        }        
    }
}
