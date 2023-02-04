using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSequences
{
    public static NoteSequence[] EASY_NOTE_SEQUENCES = new NoteSequence[]
    {
        new NoteSequence(SequenceDifficulty.Easy, new Note[] {
            new Note(NoteDuration.Quarter, true, false, false, false),
            new Note(NoteDuration.Quarter, false, false, false, true),
            new Note(NoteDuration.Eighth, false, false, true, false),
            new Note(NoteDuration.Eighth, false, true, false, false),
            new Note(NoteDuration.Quarter, false, false, true, false),

            new Note(NoteDuration.Eighth, false, false, true, false),
            new Note(NoteDuration.Eighth, false, true, false, false),
            new Note(NoteDuration.Eighth, false, false, true, false),
            new Note(NoteDuration.Eighth, false, true, false, false),

            new Note(NoteDuration.Eighth, false, false, false, false), // rest
            new Note(NoteDuration.Eighth, true, false, false, true),
            new Note(NoteDuration.Quarter, false, true, true, false),
        })
    };

    public static NoteSequence[] MEDIUM_NOTE_SEQUENCES = new NoteSequence[]
    {
        new NoteSequence(SequenceDifficulty.Easy, new Note[] {
            new Note(NoteDuration.Quarter, true, true, false, false),
        })
    };

    public static NoteSequence[] HARD_NOTE_SEQUENCES = new NoteSequence[]
    {
        new NoteSequence(SequenceDifficulty.Easy, new Note[] {
            new Note(NoteDuration.Quarter, true, true, true, false),
        })
    };
}
