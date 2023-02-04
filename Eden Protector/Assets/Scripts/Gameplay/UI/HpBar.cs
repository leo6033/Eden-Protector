using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{

    public SpriteRenderer background;

    public SpriteRenderer handle;
    

    public void SetValue(float percent)
    {
        var scale = background.transform.localScale;

        if (percent > 1)
            percent = 1;
        else if (percent < 0)
            percent = 0;
        
        scale.x = percent;
        handle.transform.localScale = scale;
    }
}
