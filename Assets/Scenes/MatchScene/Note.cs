using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Note
{
    NoteDuration duration;
    bool isLeftNote;
    bool isDownNote;
    bool isUpNote;
    bool isRightNote;

    public Note(NoteDuration duration, bool isLeftNote, bool isDownNote, bool isUpNote, bool isRightNote)
    {
        this.duration = duration;
        this.isLeftNote = isLeftNote;
        this.isDownNote = isDownNote;
        this.isUpNote = isUpNote;
        this.isRightNote = isRightNote;
    }

    public NoteDuration GetDuration()
    {
        return this.duration;
    }

    public LinkedList<Arrow.Direction> GetArrowDirections()
    {
        LinkedList<Arrow.Direction> arrowDirections = new LinkedList<Arrow.Direction>();
        if (this.isLeftNote)
        {
            arrowDirections.AddLast(Arrow.Direction.Left);
        }
        if (this.isDownNote)
        {
            arrowDirections.AddLast(Arrow.Direction.Down);
        }
        if (this.isUpNote)
        {
            arrowDirections.AddLast(Arrow.Direction.Up);
        }
        if (this.isRightNote)
        {
            arrowDirections.AddLast(Arrow.Direction.Right);
        }
        return arrowDirections;
    }
}
