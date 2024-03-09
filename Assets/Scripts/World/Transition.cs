using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// !!! has to be on the same object as camera !!!
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
public class Transition : MonoBehaviour
{
    public Material transition;
    [Range(0.0f, 2)] public float transitionDuration = 0.6f;

    public UnityEvent transitionStart;
    [System.Serializable]
    public class RoomChangeEvent : UnityEvent<int> {}
    public RoomChangeEvent roomChangeEvent;
    public UnityEvent transitionEnd;
    [System.Serializable]
    public class ProgressUpdateEvent : UnityEvent<float> {}
    public ProgressUpdateEvent progressUpdateEvent;

    private float midDuration => transitionDuration / 2f;
    private bool transitionHappening = false;
    private bool timeIsBeforeRoomChange = true;
    private float transitionTime = 0;
    private int next_room_id = -1;

    // from range [0, 1]
    // 0 - screen is visible
    // 1 - screen is not visible
    private float veilProgress {
        get {
            if (timeIsBeforeRoomChange) {
                return transitionTime / midDuration;
            }
            return 1 - ((transitionTime - midDuration) / midDuration);
        }
    }

    // function called to start transition
    public void StartTransition(int idOfNextRoom=-1) {
        next_room_id = idOfNextRoom;
        transitionHappening = true;
        transitionStart?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        HandleHappeningTransition();
    }

    private void HandleHappeningTransition() {
        // returning if there is no transition happening
        if (!transitionHappening) {
            return;
        }

        progressUpdateEvent?.Invoke(veilProgress);

        transitionTime += Time.deltaTime;

        if (transitionTime > midDuration && timeIsBeforeRoomChange) {
            roomChangeEvent?.Invoke(next_room_id);
            timeIsBeforeRoomChange = false;
        }

        // checking if transition is over
        if (transitionTime > transitionDuration) {
            transitionEnd?.Invoke();
            transitionHappening = false;
            timeIsBeforeRoomChange = true;
            transitionTime = 0;
            next_room_id = -1;
        }
    }

    private void SetPropertiesToTransitionShader() {

    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Debug.Log("pls");
        /* if (transition == null) {
            Graphics.Blit(src, dest);
            Debug.Log("nope");
            return;
        }

        SetPropertiesToTransitionShader();
        Graphics.Blit(src, dest, transition); */
    }
}
