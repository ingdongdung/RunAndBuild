using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
public class STweenSize : STweenBase<Vector2>
{

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    // public

    public override void Restore()
    {
        base.Restore();
        Vector3 cur = this.transform.localScale;
        this.rectTransform.sizeDelta = new Vector2(this.start.x, this.start.y);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    // overrides STweenBase 

    protected override void PlayTween()
    {
        base.PlayTween();
        this.rectTransform = this.GetComponent<RectTransform>();
        if (this.rectTransform == null)
        {
            Debug.LogError("STWeenSizeX must with RectTransform component");
        }
        else
        {
            base.tweenValue = this.tweener.CreateTween(this.start, this.end);
        }
    }

    protected override void UpdateValue(Vector2 value)
    {
        this.rectTransform.sizeDelta = value;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    // protected 

    protected void Awake()
    {
        this.rectTransform = this.GetComponent<RectTransform>();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    // private 

    private RectTransform rectTransform;

}
