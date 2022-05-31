using System;
using System.Collections;
using UnityEngine;

//Based on code by user coderDarren on github. Refactored and changed to fit our needs
public class AudioManager : MonoBehaviour {
    private static AudioManager _instance;

    [SerializeField] private AudioTrack[] tracks;

    private Hashtable _audioTable; //relationship of audio types (key) and tracks (value)
    private Hashtable _jobTable; //relationship between audio types (key) and jobs (value)

    private enum AudioAction {
        Start,
        Stop,
        Restart
    }

    [Serializable]
    public class AudioObject {
        public AudioType type;
        public AudioClip clip;
    }

    [Serializable]
    public class AudioTrack {
        public AudioSource source;
        public AudioTrackSO audioTrackSO;
    }

    private class AudioJob {
        public readonly AudioAction action;
        public readonly AudioType type;
        public readonly bool fade;
        public readonly WaitForSeconds delay;

        public AudioJob(AudioAction action, AudioType type, bool fade, float delay) {
            this.action = action;
            this.type = type;
            this.fade = fade;
            this.delay = delay > 0f ? new WaitForSeconds(delay) : null;
        }
    }

    private void Awake() {
        if (_instance != null) return;
        _instance = this;
        Setup();
    }

    public static void PlayAudio(AudioType type, bool fade = false, float delay = 0.0F) {
        if (_instance == null) {
            Debug.LogWarning("No instance of AudioManager!");
            return;
        }
        _instance.AddJob(new AudioJob(AudioAction.Start, type, fade, delay));
    }

    public static void StopAudio(AudioType type, bool fade = false, float delay = 0.0F) {
        if (_instance != null) {
            Debug.LogWarning("No instance of AudioManager!");
            return;
        }
        _instance.AddJob(new AudioJob(AudioAction.Stop, type, fade, delay));
    }

    public static void RestartAudio(AudioType type, bool fade = false, float delay = 0.0F) {
        if (_instance != null) {
            Debug.LogWarning("No instance of AudioManager!");
            return;
        }
        _instance.AddJob(new AudioJob(AudioAction.Restart, type, fade, delay));
    }

    #region Backend

    private void Setup() {
        _audioTable = new Hashtable();
        _jobTable = new Hashtable();
        GenerateAudioTable();
    }

    private void AddJob(AudioJob job) {
        //cancel any job that might be using this job's audio source
        RemoveConflictingJobs(job.type);

        Coroutine jobRunner = StartCoroutine(RunAudioJob(job));
        _jobTable.Add(job.type, jobRunner);
    }

    private void RemoveJob(AudioType type) {
        if (!_jobTable.ContainsKey(type)) {
            return;
        }

        Coroutine runningJob = (Coroutine) _jobTable[type];
        if (runningJob != null) StopCoroutine(runningJob);
        _jobTable.Remove(type);
    }

    private void RemoveConflictingJobs(AudioType type) {
        //cancel the job if one exists with the same type
        if (_jobTable.ContainsKey(type)) {
            RemoveJob(type);
        }

        //cancel jobs that share the same audio track
        AudioType conflictAudio = AudioType.None;
        AudioTrack audioTrackNeeded = GetAudioTrack(type, "Get Audio Track Needed");
        foreach (DictionaryEntry entry in _jobTable) {
            AudioType audioType = (AudioType) entry.Key;
            AudioTrack audioTrackInUse = GetAudioTrack(audioType, "Get Audio Track In Use");
            if (audioTrackInUse.source == audioTrackNeeded.source) {
                conflictAudio = audioType;
                break;
            }
        }

        if (conflictAudio != AudioType.None) {
            RemoveJob(conflictAudio);
        }
    }

    private IEnumerator RunAudioJob(AudioJob job) {
        if (job.delay != null) yield return job.delay;

        AudioTrack track = GetAudioTrack(job.type); //track existence should be verified by now
        track.source.clip = GetAudioClipFromAudioTrack(job.type, track);

        float initial = 0f;
        float target = 1f;
        switch (job.action) {
            case AudioAction.Start:
                track.source.Play();
                break;
            case AudioAction.Stop when !job.fade:
                track.source.Stop();
                break;
            case AudioAction.Stop:
                initial = 1f;
                target = 0f;
                break;
            case AudioAction.Restart:
                track.source.Stop();
                track.source.Play();
                break;
        }

        //fade volume
        if (job.fade) {
            float duration = 1.0f;
            float timer = 0.0f;

            while (timer <= duration) {
                track.source.volume = Mathf.Lerp(initial, target, timer / duration);
                timer += Time.deltaTime;
                yield return null;
            }

            //if _timer was 0.9999 and Time.deltaTime was 0.01 we would not have reached the target
            //make sure the volume is set to the value we want
            track.source.volume = target;

            if (job.action == AudioAction.Stop) {
                track.source.Stop();
            }
        }

        _jobTable.Remove(job.type);
    }

    private void GenerateAudioTable() {
        foreach (AudioTrack track in tracks) {
            foreach (AudioObject obj in track.audioTrackSO.audio) {
                //do not duplicate keys
                if (!_audioTable.ContainsKey(obj.type)) {
                    _audioTable.Add(obj.type, track);
                }
            }
        }
    }

    private AudioTrack GetAudioTrack(AudioType type, string job = "") {
        if (!_audioTable.ContainsKey(type)) {
            return null;
        }

        return (AudioTrack) _audioTable[type];
    }

    private AudioClip GetAudioClipFromAudioTrack(AudioType type, AudioTrack track) {
        foreach (AudioObject obj in track.audioTrackSO.audio) {
            if (obj.type == type) {
                return obj.clip;
            }
        }

        return null;
    }

    #endregion

}