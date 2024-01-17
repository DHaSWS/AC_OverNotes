using OverNotes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using Effekseer;
using UnityEngine.SearchService;
using OverNotes.System;
using System;

public class PlayerController : MonoBehaviour {
    [SerializeField] AudioSource se;
    [SerializeField] private InputActionAsset asset;
    [SerializeField] InputActionTrace trace;
    [SerializeField] EffekseerEffectAsset effect;

    // Invoke actions
    static public event Action<int> OnPressLane;

    //エフェクトの拡大率
    private static Vector3 m_effectScale = new Vector3(0.7f, 0.7f, 0.7f);


    private void Awake() {
        InputSystem.pollingFrequency = 240;
        trace = new InputActionTrace();
        PlayerInput playerInput = GetComponent<PlayerInput>();

        if (playerInput != null && playerInput.currentActionMap != null) {
            trace.SubscribeTo(playerInput.currentActionMap["Lane1"]);
            trace.SubscribeTo(playerInput.currentActionMap["Lane2"]);
            trace.SubscribeTo(playerInput.currentActionMap["Lane3"]);
            trace.SubscribeTo(playerInput.currentActionMap["Lane4"]);
        } else {
            Debug.LogError("PlayerInput or currentActionMap is null");
        }
    }

    /// <summary>
    /// Update
    /// </summary>
    void Update() {
        // Update key
        OnUpdateKey();
    }

    /// <summary>
    /// Update key
    /// </summary>
    private void OnUpdateKey() {
        foreach (var kvp in trace) {
            double triggeredTime = OverNotesSystem.Instance.NowTime - (Time.realtimeSinceStartup - kvp.time);
            string actionName = kvp.action.name;
            string num = actionName.Substring(4, 1);
            int index = int.Parse(num) - 1;

            if (kvp.phase == InputActionPhase.Started) {
                if (OnPressLane != null) {
                    OnPressLane?.Invoke(0);
                }
                Push(index, triggeredTime);
            }
            if (kvp.phase == InputActionPhase.Canceled)
                Released(index, triggeredTime);
        }
        trace.Clear();
    }

    /// <summary>
    /// Push
    /// </summary>
    /// <param name="index">Lane index</param>
    /// <param name="triggeredTime">Triggered time</param>
    private void Push(int index, double triggeredTime) {
        if (PlayData.Lanes[index].childCount == 0) {
            return;
        }
        GameObject note = PlayData.Lanes[index].GetChild(0).gameObject;
        NoteController noteController = note.GetComponent<NoteController>();

        // Play SE
        se.PlayOneShot(se.clip);

        PlayTapEffect(index);

        // Judge
        noteController.JudgeNormal(Mathf.Abs((float)(triggeredTime - noteController.param.beatTime)));
    }

    /// <summary>
    /// Released
    /// </summary>
    /// <param name="index">Lane index</param>
    /// <param name="triggeredTime">Triggered time</param>
    private void Released(int index, double triggeredTime) {
        if (PlayData.Lanes[index].childCount == 0) {
            return;
        }
        GameObject note = PlayData.Lanes[index].GetChild(0).gameObject;
        NoteController noteController = note.GetComponent<NoteController>();
        noteController.JudgeHold(Mathf.Abs((float)(triggeredTime - noteController.param.beatEndTime)));
    }

    private void OnDestroy() {
        if (trace != null) {
            trace.UnsubscribeFromAll();
            trace.Dispose();
        } else {
            Debug.LogWarning("trace is already null. Skipping unsubscribe and dispose.");
        }
    }

    /// <summary>
    /// タップエフェクト描画
    /// </summary>
    /// <param name="pushNumber">押したキーの番号</param>
    private void PlayTapEffect(int pushNumber) {
        //エフェクトの座標計算
        Vector3 effectPosition = new Vector3(-1.5f + ((pushNumber) * 1.0f), -5, 0.5f);


        //エフェクト
        EffekseerHandle handle = EffekseerSystem.PlayEffect(effect, effectPosition);
        //拡大率
        handle.SetScale(m_effectScale);
        //角度
        //handle.SetRotation(effectRotation);

        Debug.Log("playEffect!!");
    }
}
