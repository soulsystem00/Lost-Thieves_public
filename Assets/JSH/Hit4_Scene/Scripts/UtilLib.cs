using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UtilLib
{
    public static Vector2 GetImagesize(GameObject target)
    {
        var image = target.GetComponent<Image>();
        var imagePos = new Vector2(image.rectTransform.sizeDelta.x, image.rectTransform.sizeDelta.y);
        return imagePos;
    }
    //var image = target.GetComponent<RectTransform>();
    //var imagePos = new Vector3(image.rect.width, image.rect.height, 0);
    //    return imagePos;
}
