using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossfade : MonoBehaviour
{
    public Material crossfade;

    public void OnTransitionProgress(float veilProgress) {
        SetProgress(veilProgress);
    }

    public void TransitionEnd() {
        SetProgress(0);
    }

    private void SetProgress(float veilProgress) {
        crossfade.SetFloat("_Progress", veilProgress * 1.2f);
    }
}
