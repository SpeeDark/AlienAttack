using System.Collections;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip[] Tracks;
    private AudioSource currentTrack;

    private Coroutine waitingCoroutine = null;

    [Space(15f)]
    [SerializeField] private int _startTrackNumber = 0;

    private int _currentTrackNumber = 0;

    private void OnValidate()
    {
        if (_startTrackNumber < 0)
            _startTrackNumber = 0;
        if (_startTrackNumber >= Tracks.Length)
            _startTrackNumber = Tracks.Length - 1;
    }

    private void Awake()
    {
        currentTrack = GetComponent<AudioSource>();

        _currentTrackNumber = _startTrackNumber;
    }

    private void Start()
    {
        PlayTrack(Tracks[_startTrackNumber]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            PlayPreviousTrack();

        if (Input.GetKeyDown(KeyCode.X))
            PlayNextTrack();
    }

    private void PlayNextTrack()
    {
        _currentTrackNumber++;
        if (_currentTrackNumber > Tracks.Length - 1)
            _currentTrackNumber = 0;

        PlayTrack(Tracks[_currentTrackNumber]);
    }

    private void PlayPreviousTrack()
    {
        _currentTrackNumber--;
        if (_currentTrackNumber < 0)
            _currentTrackNumber = Tracks.Length - 1;

        PlayTrack(Tracks[_currentTrackNumber]);
    }

    private void PlayTrack(AudioClip CurrentTrack)
    {
        currentTrack.clip = CurrentTrack;
        currentTrack.Play();

        if (waitingCoroutine != null)
            StopCoroutine(waitingCoroutine);

        waitingCoroutine = StartCoroutine(WaitTrackDuration(CurrentTrack.length));
    }

    private IEnumerator WaitTrackDuration(float time)
    {
        yield return new WaitForSeconds(time);

        PlayNextTrack();
    }
}