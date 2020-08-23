using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSetupPortraitView : InfiniteScrollCell
{
    [SerializeField]
    RectTransform m_rectTransform;

    public override Rect CellSize
    {
        get => m_rectTransform.rect;
    }
}
