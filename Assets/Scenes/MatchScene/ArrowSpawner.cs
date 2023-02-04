using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject leftHitZone;
    public GameObject downHitZone;
    public GameObject upHitZone;
    public GameObject rightHitZone;

    public GameObject leftArrow;
    public GameObject downArrow;
    public GameObject upArrow;
    public GameObject rightArrow;

    private static int BEATS_PER_MINUTE = 120; // quarter notes per minute

    private static float WHOLE_NOTES_PER_BEAT = 0.25f;
    private static float SECONDS_PER_MINUTE = 60f;

    private static float WHOLE_NOTES_PER_SECOND = ArrowSpawner.BEATS_PER_MINUTE * ArrowSpawner.WHOLE_NOTES_PER_BEAT / ArrowSpawner.SECONDS_PER_MINUTE;
    private static float SHORTEST_NOTE_DURATION = ArrowSpawner.GetNoteDurationInSeconds(NoteDuration.Sixteenth);

    private float leftArrowSpawnX;
    private float downArrowSpawnX;
    private float upArrowSpawnX;
    private float rightArrowSpawnX;

    private float spawnY;

    private float timerSeconds = 0f;
    private float delayToNextNoteSeconds = 0f;

    private Queue<Note> currentSequenceNotes;
    public Queue<NoteSequence> noteSequenceQueue;

    // Start is called before the first frame update
    void Start()
    {
        this.spawnY = this.transform.position.y;
        this.leftArrowSpawnX = this.leftHitZone.transform.position.x;
        this.downArrowSpawnX = this.downHitZone.transform.position.x;
        this.upArrowSpawnX = this.upHitZone.transform.position.x;
        this.rightArrowSpawnX = this.rightHitZone.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        this.timerSeconds += Time.deltaTime;
        if (this.timerSeconds >= this.delayToNextNoteSeconds)
        {
            this.timerSeconds -= this.delayToNextNoteSeconds;
            if (this.currentSequenceNotes == null || this.currentSequenceNotes.Count == 0)
            {
                // this.delayToNextNoteSeconds = ArrowSpawner.SHORTEST_NOTE_DURATION;
                // TODO allow some mechanism for external objects to add sequences to the queue
                this.SetCurrentNoteSequence(NoteSequenceGenerator.GetRandomEasyNoteSequence());
                this.delayToNextNoteSeconds = 0f;
            }
            else
            {
                Note nextNote = this.currentSequenceNotes.Dequeue();
                LinkedList<Arrow.Direction> arrowDirections = nextNote.GetArrowDirections();
                this.SpawnArrowDirections(arrowDirections);
                this.delayToNextNoteSeconds = ArrowSpawner.GetNoteDurationInSeconds(nextNote.GetDuration());
            }
        }
    }

    private static float GetNoteDurationInSeconds(NoteDuration noteDuration)
    {
        float secondsPerWholeNote = 1f / WHOLE_NOTES_PER_SECOND;
        switch (noteDuration)
        {
            case NoteDuration.Whole:
                return secondsPerWholeNote;
            case NoteDuration.Half:
                return secondsPerWholeNote / 2f;
            case NoteDuration.Quarter:
                return secondsPerWholeNote / 4f;
            case NoteDuration.Eighth:
                return secondsPerWholeNote / 8f;
            case NoteDuration.Sixteenth:
                return secondsPerWholeNote / 16f;
            default:
                return secondsPerWholeNote;
        }
    }

    private void SetCurrentNoteSequence(NoteSequence sequence)
    {
        this.currentSequenceNotes = new Queue<Note>(sequence.GetNotes());
    }

    private void SpawnArrowDirections(LinkedList<Arrow.Direction> arrowDirections)
    {
        if (arrowDirections.Contains(Arrow.Direction.Left))
        {
            this.SpawnArrow(Arrow.Direction.Left);
        }
        if (arrowDirections.Contains(Arrow.Direction.Down))
        {
            this.SpawnArrow(Arrow.Direction.Down);
        }
        if (arrowDirections.Contains(Arrow.Direction.Up))
        {
            this.SpawnArrow(Arrow.Direction.Up);
        }
        if (arrowDirections.Contains(Arrow.Direction.Right))
        {
            this.SpawnArrow(Arrow.Direction.Right);
        }
    }

    private void SpawnArrow(Arrow.Direction direction)
    {
        switch (direction)
        {
            case Arrow.Direction.Left:
                this.SpawnLeftArrow();
                break;
            case Arrow.Direction.Down:
                this.SpawnDownArrow();
                break;
            case Arrow.Direction.Up:
                this.SpawnUpArrow();
                break;
            case Arrow.Direction.Right:
                this.SpawnRightArrow();
                break;
            default:
                this.SpawnLeftArrow();
                break;
        }
    }

    private void SpawnLeftArrow()
    {
        GameObject leftArrow = Instantiate(this.leftArrow, new Vector3(this.leftArrowSpawnX, this.spawnY, 0), Quaternion.identity);
        HitZone hitZone = this.leftHitZone.GetComponent<HitZone>();
        hitZone.AddArrow(leftArrow);
    }

    private void SpawnDownArrow()
    {
        GameObject downArrow = Instantiate(this.downArrow, new Vector3(this.downArrowSpawnX, this.spawnY, 0), Quaternion.identity);
        HitZone hitZone = this.downHitZone.GetComponent<HitZone>();
        hitZone.AddArrow(downArrow);
    }

    private void SpawnUpArrow()
    {
        GameObject upArrow = Instantiate(this.upArrow, new Vector3(this.upArrowSpawnX, this.spawnY, 0), Quaternion.identity);
        HitZone hitZone = this.upHitZone.GetComponent<HitZone>();
        hitZone.AddArrow(upArrow);
    }

    private void SpawnRightArrow()
    {
        GameObject rightArrow = Instantiate(this.rightArrow, new Vector3(this.rightArrowSpawnX, this.spawnY, 0), Quaternion.identity);
        HitZone hitZone = this.rightHitZone.GetComponent<HitZone>();
        hitZone.AddArrow(rightArrow);
    }

    private Arrow.Direction GetRandomArrowDirection()
    {
        int range = Random.Range(0, 4);
        switch (range)
        {
            case 0:
                return Arrow.Direction.Left;
            case 1:
                return Arrow.Direction.Down;
            case 2:
                return Arrow.Direction.Up;
            case 3:
                return Arrow.Direction.Right;
            default:
                return Arrow.Direction.Left;
        }
    }
}
