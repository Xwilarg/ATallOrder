using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransitionStateShower : MonoBehaviour
{
    public TextMeshProUGUI stateText;

    public void TransitionStart() {
        stateText.text = "State: started";
    }

    public void RoomChange(int value) {
        stateText.text = "State: changed to room " + value;
    }

    public void TransitionEnd() {
        stateText.text = "State: ended";
    }

    public void PrintProgress(float veilProgress) {
        Debug.Log("veil progress: " + veilProgress);
    }
}
