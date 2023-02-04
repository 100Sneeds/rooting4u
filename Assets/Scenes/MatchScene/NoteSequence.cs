using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSequence
{
    SequenceDifficulty difficulty;
    Note[] notes;

    public NoteSequence(SequenceDifficulty difficulty, Note[] notes)
    {
        this.difficulty = difficulty;
        this.notes = notes;
    }

    public Note[] GetNotes()
    {
        return this.notes;
    }
}
