using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[System.Serializable]
public class HapticSettings
{
    public bool active = true;
    [Range(0f, 1f)]
    public float amplitude = .3f;
    public float duration = .5f;
    public float frequency = 2f;
}

public class BallHaptics : XRGrabInteractable
{
    [SerializeField] private HapticSettings HapticOnSelectEntered;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        
        if (HapticOnSelectEntered.active)
        {
            TriggerHaptic(args, HapticOnSelectEntered);
        }
    }

    private void TriggerHaptic(SelectEnterEventArgs args, HapticSettings hapticSettings)
    {
        var controller = args.interactorObject.handedness == InteractorHandedness.Left ? HapticsUtility.Controller.Left : HapticsUtility.Controller.Right;
        HapticsUtility.SendHapticImpulse(hapticSettings.amplitude, hapticSettings.duration, controller, hapticSettings.frequency);
    }
}
