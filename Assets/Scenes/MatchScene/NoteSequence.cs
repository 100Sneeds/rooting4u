using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSequence
{
    public enum SuccessState
    {
        NotComplete,
        Fail,
        Success,
        FullCombo,
    }

    public enum SpawnState
    {
        NotFullySpawned,
        Spawned,
        PastHitZone,
    }

    private static float SEQUENCE_FAIL_PERCENT_UPPER_LIMIT = 0.7f;

    SequenceDifficulty difficulty;
    Note[] notes;
    public List<GameObject> arrowObjects = new List<GameObject>();

    public NoteSequence(SequenceDifficulty difficulty, Note[] notes)
    {
        this.difficulty = difficulty;
        this.notes = notes;
    }

    public SequenceDifficulty GetDifficulty()
    {
        return this.difficulty;
    }

    public Note[] GetNotes()
    {
        return this.notes;
    }

    public void AddArrowObject(GameObject arrowObject)
    {
        this.arrowObjects.Add(arrowObject);
    }

    public SpawnState GetSpawnState()
    {
        if (this.arrowObjects.Count < this.notes.Length)
        {
            return SpawnState.NotFullySpawned;
        }

        bool isAllArrowsReachedHitZone = this.arrowObjects.TrueForAll((arrow) => {
            return arrow.GetComponent<Arrow>().GetSuccessState() != Arrow.SuccessState.HasNotReachedHitZone;
        });
        if (isAllArrowsReachedHitZone)
        {
            return SpawnState.PastHitZone;
        } else
        {
            return SpawnState.Spawned;
        }
    }

    public SuccessState GetSuccessState()
    {
        if (this.GetSpawnState() != SpawnState.PastHitZone)
        {
            return SuccessState.NotComplete;
        }

        float hitPercentage = this.GetHitArrowsPercentage();
        if (hitPercentage == 1.0f)
        {
            return SuccessState.FullCombo;
        }
        if (hitPercentage < NoteSequence.SEQUENCE_FAIL_PERCENT_UPPER_LIMIT)
        {
            return SuccessState.Fail;
        }
        else
        {
            return SuccessState.Success;
        }
    }

    private float GetHitArrowsPercentage()
    {
        int numArrows = this.arrowObjects.Count;
        int hitArrows = 0;
        foreach (GameObject arrowObject in this.arrowObjects)
        {
            Arrow arrow = arrowObject.GetComponent<Arrow>();
            if (arrow.GetSuccessState() == Arrow.SuccessState.Hit)
            {
                hitArrows++;
            }
        }
        return (float)hitArrows / (float)numArrows;
    }
}
