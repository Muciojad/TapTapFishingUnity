using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBarController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image timeBarImg;
    private float maxWidth;
    private float currentWidth;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    void Start()
    {
        maxWidth = timeBarImg.rectTransform.sizeDelta.x;
    }

    void Update()
    {
        //currentTime += Time.deltaTime;
        currentWidth = maxWidth - maxWidth * (GameTimeController.instance.ElapsedTime / GameTimeController.instance.RoundTime);
        //timeBarImg.rectTransform.sizeDelta = new Vector2(currentWidth, timeBarImg.rectTransform.sizeDelta.y);
        timeBarImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
    }
}
