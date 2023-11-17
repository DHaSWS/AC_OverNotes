using OverNotes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerController : MonoBehaviour
{
    [SerializeField] AudioSource se;
    [SerializeField] private InputActionAsset asset;
    [SerializeField] InputActionTrace trace;

    private void Awake()
    {
        InputSystem.pollingFrequency = 240;
        trace = new InputActionTrace();
        PlayerInput playerInput = GetComponent<PlayerInput>();

        if (playerInput != null && playerInput.currentActionMap != null)
        {
            trace.SubscribeTo(playerInput.currentActionMap["Lane1"]);
            trace.SubscribeTo(playerInput.currentActionMap["Lane2"]);
            trace.SubscribeTo(playerInput.currentActionMap["Lane3"]);
            trace.SubscribeTo(playerInput.currentActionMap["Lane4"]);
        }
        else
        {
            Debug.LogError("PlayerInput or currentActionMap is null");
        }
    }

    private void Update()
    {
        foreach(var kvp in trace)
        {
            double triggeredTime = SystemData.nowTime - (Time.realtimeSinceStartup - kvp.time);
            string actionName = kvp.action.name;
            string num = actionName.Substring(4, 1);
            int index = int.Parse(num) - 1;

            if (SystemData.PlayData.lanes[index].childCount == 0)
            {
                continue;
            }
            if (kvp.phase == InputActionPhase.Started)
                Push(index, triggeredTime);
            if (kvp.phase == InputActionPhase.Canceled)
                Released(index, triggeredTime);
        }
        trace.Clear();
    }

    public void TriggeredKey(InputAction.CallbackContext context)
    {
        double triggeredTime = SystemData.nowTime - (Time.realtimeSinceStartup - context.time);
        string actionName = context.action.name;
        string num = actionName.Substring(4, 1);
        int index = int.Parse(num) - 1;

        if (SystemData.PlayData.lanes[index].childCount == 0)
        {
            return;
        }
        if (context.started)
            Push(index, triggeredTime);
        if (context.canceled)
            Released(index, triggeredTime);
    }

    private void Push(int index, double triggeredTime)
    {
        GameObject note = SystemData.PlayData.lanes[index].GetChild(0).gameObject;
        NoteController noteController = note.GetComponent<NoteController>();

        se.PlayOneShot(se.clip);

        noteController.JudgeNormal(Mathf.Abs((float)(triggeredTime - noteController.param.beatTime)));
    }
    private void Released(int index, double triggeredTime)
    {
        GameObject note = SystemData.PlayData.lanes[index].GetChild(0).gameObject;
        NoteController noteController = note.GetComponent<NoteController>();
        noteController.JudgeHold(Mathf.Abs((float)(triggeredTime - noteController.param.beatEndTime)));
    }

    private void OnDestroy()
    {
        if (trace != null)
        {
            trace.UnsubscribeFromAll();
            trace.Dispose();
        }
        else
        {
            Debug.LogWarning("trace is already null. Skipping unsubscribe and dispose.");
        }
    }
}
