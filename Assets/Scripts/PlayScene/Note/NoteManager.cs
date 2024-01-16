using OverNotes;
using OverNotes.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class NoteManager : MonoBehaviour {
    [SerializeField] private NoteFactory _noteFactory;
    private List<NoteParam> _notes;

    public void OnAwake() {
        _notes = OverNotesSystem.Instance.GetChart().notes;
        foreach (NoteParam note in _notes) {
            note.noteState = NoteParam.NOTE_STATE.NONE;
            PlayContext.LastBeatTime = Mathf.Max((float)PlayContext.LastBeatTime, (float)(note.beatEndTime));
        }
    }

    public void OnUpdate() {
        foreach (NoteParam note in _notes) {
            if (note.beatTime <= PlayContext.DisplayTime && note.noteState == NoteParam.NOTE_STATE.NONE) {
                _noteFactory.CreateNote(note.beatTime, note.beatEndTime, note.column);
                note.noteState = NoteParam.NOTE_STATE.NORMAL;
            }
        }
    }
}
