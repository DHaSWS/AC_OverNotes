using OverNotes;
using OverNotes.System;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour {
    [SerializeField] private NoteFactory _noteFactory;
    private List<GameObject> _notes;
    private List<NoteParam> _noteParams;

    /// <summary>
    /// Awake
    /// </summary>
    public void Awake() {
        _noteParams = OverNotesSystem.Instance.GetChart().notes;
        _notes = new List<GameObject>();
        OnAwakeChart();
    }

    /// <summary>
    /// Awake chart
    /// </summary>
    private void OnAwakeChart() {
        foreach (NoteParam note in _noteParams) {
            note.noteState = NoteParam.NOTE_STATE.NONE;
            PlayContext.LastBeatTime = Mathf.Max((float)PlayContext.LastBeatTime,
                (float)(note.beatEndTime));
        }
    }

    /// <summary>
    /// Fixed Update
    /// </summary>
    void FixedUpdate() {
        OnUpdateNoteParams();
        OnUpdateNotes();
    }

    /// <summary>
    /// Update noteParam
    /// </summary>
    private void OnUpdateNoteParams() {
        foreach (NoteParam noteParam in _noteParams) {
            if (noteParam.beatTime <= PlayContext.DisplayTime &&
                noteParam.noteState == NoteParam.NOTE_STATE.NONE) {
                noteParam.noteState = NoteParam.NOTE_STATE.NORMAL;
                GameObject note = _noteFactory.CreateNote(
                    noteParam.beatTime, noteParam.beatEndTime, noteParam.column);
                _notes.Add(note);
            }
        }
    }

    /// <summary>
    /// Update notes
    /// </summary>
    private void OnUpdateNotes() {
        for (int i = 0; i < _notes.Count; i++) {
            if (_notes[i] == null) {
                _notes.Remove(_notes[i]);
                continue;
            }
            GameObject note = _notes[i];
            NoteController noteController = note.GetComponent<NoteController>();
            noteController.OnUpdate();
            float sub = noteController.GetSubTime();
            if (sub > SystemConstants.JudgementRange[(int)PlayContext.Judge.Bad]) {
                _notes.Remove(note);
                noteController.SetJudgeMiss();
            }
        }
    }
}
