using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBarValue : MonoBehaviour
{
    public RectTransform RectTransform;

    // Start is called before the first frame update
    void Start()
    {
        Rect temp = RectTransform.rect;
        //temp.width = 225;
        // RectTransform.sizeDelta = new Vector2 (45, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
