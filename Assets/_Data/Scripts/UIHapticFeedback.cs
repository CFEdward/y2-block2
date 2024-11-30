using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.UI;

[System.Serializable]
public class HapticSettings
{
    public bool active = true;
    [Range(0f, 1f)]
    public float amplitude = .3f;
    public float duration = .5f;
    public float frequency = 2f;
}

public class UIHapticFeedback : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private HapticSettings OnHoverEnter;
    [SerializeField] private HapticSettings OnHoverExit;
    [SerializeField] private HapticSettings OnSelectEnter;
    [SerializeField] private HapticSettings OnSelectExit;

    private XRUIInputModule InputModule => EventSystem.current.currentInputModule as XRUIInputModule;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnHoverEnter.active)
        {
            TriggerHaptic(eventData, OnHoverEnter);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnHoverExit.active)
        {
            TriggerHaptic(eventData, OnHoverExit);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnSelectEnter.active)
        {
            TriggerHaptic(eventData, OnSelectEnter);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnSelectExit.active)
        {
            TriggerHaptic(eventData, OnSelectExit);
        }
    }

    private void TriggerHaptic(PointerEventData eventData, HapticSettings hapticSettings)
    {
        NearFarInteractor interactor = InputModule.GetInteractor(eventData.pointerId) as NearFarInteractor;
        if (!interactor) return;

        var controller = interactor.handedness == InteractorHandedness.Left ? HapticsUtility.Controller.Left : HapticsUtility.Controller.Right;

        HapticsUtility.SendHapticImpulse(hapticSettings.amplitude, hapticSettings.duration, controller, hapticSettings.frequency);
    }
}
