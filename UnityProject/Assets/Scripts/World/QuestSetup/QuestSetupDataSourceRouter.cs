using Jalopy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSetupDataSourceRouter : DataSource
{
    public delegate int NumberOfCellsDel();
    private NumberOfCellsDel m_numberOfCellsDelegate = null;

    public delegate void CellAtIndexDel(InfiniteScrollCell cell, int index);
    private CellAtIndexDel m_cellAtIndexDelegate = null;

    public NumberOfCellsDel NumberOfCellsDelegate
    {
        set { m_numberOfCellsDelegate = value; }
    }

    public CellAtIndexDel CellAtIndexDelegate
    {
        set { m_cellAtIndexDelegate = value; }
    }

    public override int NumberOfCells
    {
        get
        {
            int cells = 0;
            if (m_numberOfCellsDelegate != null)
            {
                cells = m_numberOfCellsDelegate();
            }

            return cells;
        }
    }

    public override void CellAtIndex(InfiniteScrollCell cell, int index)
    {
        if (m_cellAtIndexDelegate != null)
        {
            m_cellAtIndexDelegate(cell, index);
        }
    }
}
