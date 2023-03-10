using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

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

    public PerformancePhase performancePhase;
    public BeatMarker beatMarker;
    public AudioClipPlayer audioClipPlayer;
    public bool isPlayingBackingTrack;

    private static int BEATS_PER_MINUTE = 185; // quarter notes per minute

    private static float WHOLE_NOTES_PER_BEAT = 0.25f;
    private static float SECONDS_PER_MINUTE = 60f;

    private static float WHOLE_NOTES_PER_SECOND = ArrowSpawner.BEATS_PER_MINUTE * ArrowSpawner.WHOLE_NOTES_PER_BEAT / ArrowSpawner.SECONDS_PER_MINUTE;
    private static float SHORTEST_NOTE_DURATION = ArrowSpawner.GetNoteDurationInSeconds(NoteDuration.Sixteenth);

    private static int BEATS_FROM_SPAWN_TO_HITZONE = 8;
    private static float SECONDS_PER_BEAT = ArrowSpawner.GetNoteDurationInSeconds(NoteDuration.Quarter);
    private static float SECONDS_TO_HITZONE = SECONDS_PER_BEAT * (float)BEATS_FROM_SPAWN_TO_HITZONE;
    private static float HITZONE_Y = -0.25f;
    private static float SPAWNER_Y = -9.5f;
    private static float DISTANCE_TO_HITZONE = Mathf.Abs(SPAWNER_Y - HITZONE_Y);
    public static float RHYTHM_BOARD_SPEED = DISTANCE_TO_HITZONE / SECONDS_TO_HITZONE;

    private float leftArrowSpawnX;
    private float downArrowSpawnX;
    private float upArrowSpawnX;
    private float rightArrowSpawnX;

    private float spawnY;

    private float noteTimerSeconds = 0f;
    private float delayToNextNoteSeconds = 0f;

    private float beatTimerSeconds = 0f;
    private float delayToNextBeatSeconds = ArrowSpawner.GetNoteDurationInSeconds(NoteDuration.Quarter);

    private float measureTimerSeconds = 0f;
    private float delayToNextMeasureSeconds = ArrowSpawner.GetNoteDurationInSeconds(NoteDuration.Whole);

    private float audioClipTimerSeconds = 0f;
    private float delayToNextAudioClipSeconds = 0f;
    private string nextAudioFileName;

    private float backingTrackTimerSeconds = 0f;
    private float delayToNextBackingTrackSeconds = 0f;
    private bool isBackingTrackQueued = false;

    private int measureCounter = 0;
    private int previousMeasureCount = -1;
    private int measuresPerSequence = 8;

    private NoteSequence currentSequence;
    private Queue<Note> currentSequenceNotes;

    private Queue<NoteSequence> incompleteSequences = new Queue<NoteSequence>();
    public Queue<NoteSequence> sequenceSpawnQueue = new Queue<NoteSequence>();

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
        this.noteTimerSeconds += Time.deltaTime;
        this.beatTimerSeconds += Time.deltaTime; // Every quarter note
        this.measureTimerSeconds += Time.deltaTime; // Every measure
        this.audioClipTimerSeconds += Time.deltaTime;
        this.backingTrackTimerSeconds += Time.deltaTime;

        if (this.beatTimerSeconds >= this.delayToNextBeatSeconds)
        {
            this.beatTimerSeconds -= this.delayToNextBeatSeconds;
            this.SpawnBeatMarker();
        }

        if (this.measureTimerSeconds >= this.delayToNextMeasureSeconds)
        {
            this.measureTimerSeconds -= this.delayToNextMeasureSeconds;
            this.delayToNextMeasureSeconds = ArrowSpawner.GetNoteDurationInSeconds(NoteDuration.Whole);
            this.SpawnMeasureMarker();
            this.measureCounter++;
        }

        if (this.previousMeasureCount != this.measureCounter && this.measureCounter % this.measuresPerSequence == 0)
        {
            this.previousMeasureCount = this.measureCounter;

            if (this.isPlayingBackingTrack)
            {
                this.backingTrackTimerSeconds = 0f;
                this.delayToNextBackingTrackSeconds = 2.5f;
                this.isBackingTrackQueued = true;
            }

            if (this.currentSequenceNotes == null || this.currentSequenceNotes.Count == 0)
            {
                if (sequenceSpawnQueue.Count > 0)
                {
                    this.SetCurrentNoteSequence(sequenceSpawnQueue.Dequeue());

                    this.noteTimerSeconds = 0f;
                    this.delayToNextNoteSeconds = 0f;

                    this.audioClipTimerSeconds = 0f;
                    this.delayToNextAudioClipSeconds = 2.5f; //SECONDS_TO_HITZONE;
                }
            }
        }

        if (this.noteTimerSeconds >= this.delayToNextNoteSeconds)
        {
            this.noteTimerSeconds -= this.delayToNextNoteSeconds;
            if (this.currentSequenceNotes != null && this.currentSequenceNotes.Count != 0)
            {
                Note nextNote = this.currentSequenceNotes.Dequeue();
                LinkedList<Arrow.Direction> arrowDirections = nextNote.GetArrowDirections();
                this.SpawnArrowDirections(arrowDirections);
                this.delayToNextNoteSeconds = ArrowSpawner.GetNoteDurationInSeconds(nextNote.GetDuration());
            }
        }

        if (this.audioClipTimerSeconds >= this.delayToNextAudioClipSeconds && this.nextAudioFileName != null)
        {
            this.audioClipTimerSeconds = 0f;
            this.delayToNextAudioClipSeconds = 0f;
            this.audioClipPlayer.PlayAudioClipByName(this.nextAudioFileName, 0.05f);
            this.nextAudioFileName = null;
        }

        if (this.backingTrackTimerSeconds >= this.delayToNextBackingTrackSeconds && this.isBackingTrackQueued)
        {
            this.backingTrackTimerSeconds = 0f;
            this.delayToNextBackingTrackSeconds = 0f;
            this.audioClipPlayer.PlayAudioClipByName("Backbeat", 0.3f);
            this.isBackingTrackQueued = false;
        }

        if (this.incompleteSequences.Count != 0)
        {
            this.UpdateAndResolveIncompleteSequences(this.incompleteSequences);
        }
    }

    public void AddSequenceToQueue(NoteSequence sequence)
    {
        this.sequenceSpawnQueue.Enqueue(sequence);
    }

    public static float GetNoteDurationInSeconds(NoteDuration noteDuration)
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

    private void UpdateAndResolveIncompleteSequences(Queue<NoteSequence> incompleteSequences)
    {
        if (incompleteSequences.Count != 0)
        {
            NoteSequence nextSequence = incompleteSequences.Peek();
            NoteSequence.SpawnState spawnState = nextSequence.GetSpawnState();
            
            if (spawnState == NoteSequence.SpawnState.PastHitZone)
            {
                this.performancePhase.HandleSequenceResult(nextSequence.GetSuccessState());
                incompleteSequences.Dequeue();
            }
        }
    }

    private void SetCurrentNoteSequence(NoteSequence sequence)
    {
        this.currentSequence = sequence;
        this.currentSequenceNotes = new Queue<Note>(sequence.GetNotes());
        this.incompleteSequences.Enqueue(sequence);
        this.nextAudioFileName = sequence.GetAudioFileName();
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
        this.currentSequence.AddArrowObject(leftArrow);
    }

    private void SpawnDownArrow()
    {
        GameObject downArrow = Instantiate(this.downArrow, new Vector3(this.downArrowSpawnX, this.spawnY, 0), Quaternion.identity);
        HitZone hitZone = this.downHitZone.GetComponent<HitZone>();
        hitZone.AddArrow(downArrow);
        this.currentSequence.AddArrowObject(downArrow);
    }

    private void SpawnUpArrow()
    {
        GameObject upArrow = Instantiate(this.upArrow, new Vector3(this.upArrowSpawnX, this.spawnY, 0), Quaternion.identity);
        HitZone hitZone = this.upHitZone.GetComponent<HitZone>();
        hitZone.AddArrow(upArrow);
        this.currentSequence.AddArrowObject(upArrow);
    }

    private void SpawnRightArrow()
    {
        GameObject rightArrow = Instantiate(this.rightArrow, new Vector3(this.rightArrowSpawnX, this.spawnY, 0), Quaternion.identity);
        HitZone hitZone = this.rightHitZone.GetComponent<HitZone>();
        hitZone.AddArrow(rightArrow);
        this.currentSequence.AddArrowObject(rightArrow);
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

    private void SpawnBeatMarker()
    {
        BeatMarker beatMarker = Instantiate(this.beatMarker, this.transform.position, Quaternion.identity);
        SpriteRenderer beatMarkerSprite = beatMarker.GetComponent<SpriteRenderer>();
        Color color = beatMarkerSprite.color;
        color.a -= 0.5f;
        beatMarkerSprite.color = color;
    }

    private void SpawnMeasureMarker()
    {
        Instantiate(beatMarker, this.transform.position, Quaternion.identity);
    }
}
