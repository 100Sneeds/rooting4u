using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoteSequenceGenerator
{
    private static int EASY_SEQUENCE_CARD_VALUE_UPPER_LIMIT = 3;
    private static int MEDIUM_SEQUENCE_CARD_VALUE_UPPER_LIMIT = 6;

    private static NoteSequence[] easyNoteSequences = NoteSequences.EASY_NOTE_SEQUENCES;
    private static NoteSequence[] mediumNoteSequences = NoteSequences.MEDIUM_NOTE_SEQUENCES;
    private static NoteSequence[] hardNoteSequences = NoteSequences.HARD_NOTE_SEQUENCES;

    private static int numEasySequences = NoteSequences.EASY_NOTE_SEQUENCES.Length;
    private static int numMediumSequences = NoteSequences.MEDIUM_NOTE_SEQUENCES.Length;
    private static int numHardSequences = NoteSequences.HARD_NOTE_SEQUENCES.Length;

    public static NoteSequence GetRandomEasyNoteSequence()
    {
        int randomIndex = Random.Range(0, numEasySequences);
        return NoteSequenceGenerator.easyNoteSequences[randomIndex];
    }

    public static NoteSequence GetRandomMediumNoteSequence()
    {
        int randomIndex = Random.Range(0, numMediumSequences);
        return NoteSequenceGenerator.mediumNoteSequences[randomIndex];
    }

    public static NoteSequence GetRandomHardNoteSequence()
    {
        int randomIndex = Random.Range(0, numHardSequences);
        return NoteSequenceGenerator.hardNoteSequences[randomIndex];
    }

    public static NoteSequence GetNoteSequenceOfCard(Card card)
    {
        SequenceDifficulty difficulty = NoteSequenceGenerator.GetSequenceDifficultyOfCard(card);
        switch (difficulty)
        {
            case SequenceDifficulty.Easy:
                return NoteSequenceGenerator.GetRandomEasyNoteSequence();
            case SequenceDifficulty.Medium:
                return NoteSequenceGenerator.GetRandomMediumNoteSequence();
            case SequenceDifficulty.Hard:
                return NoteSequenceGenerator.GetRandomHardNoteSequence();
            default:
                return NoteSequenceGenerator.GetRandomEasyNoteSequence();
        }
    }

    private static SequenceDifficulty GetSequenceDifficultyOfCard(Card card)
    {
        int cardValue = card.statAttack + card.statDefence;
        if (cardValue <= NoteSequenceGenerator.EASY_SEQUENCE_CARD_VALUE_UPPER_LIMIT)
        {
            return SequenceDifficulty.Easy;
        }
        else if (cardValue <= NoteSequenceGenerator.MEDIUM_SEQUENCE_CARD_VALUE_UPPER_LIMIT)
        {
            return SequenceDifficulty.Medium;
        }
        else
        {
            return SequenceDifficulty.Hard;
        }
    }
}
