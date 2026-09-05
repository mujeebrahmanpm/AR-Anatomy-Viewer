using UnityEngine;
using UnityEngine.UIElements;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private AnatomyLayerManager layerManager;

    private VisualElement root;
    private VisualElement transparencyControls;
    private VisualElement clippingControls;
    private VisualElement knob;

    void OnEnable()
    {
        root = uiDocument.rootVisualElement;

        var skullToggle = root.Q<Toggle>("skull-toggle");
        var brainToggle = root.Q<Toggle>("brain-toggle");
        var skullSlider = root.Q<Slider>("skull-slider");
        var brainSlider = root.Q<Slider>("brain-slider");
        var modeSwitch = root.Q<Toggle>("mode-switch");
        knob = root.Q<VisualElement>("mode-switch-knob");

        var clipXSlider = root.Q<MinMaxSlider>("clip-x");
        var clipYSlider = root.Q<MinMaxSlider>("clip-y");
        var clipZSlider = root.Q<MinMaxSlider>("clip-z");

        transparencyControls = root.Q<VisualElement>("TransparencyControls");
        clippingControls = root.Q<VisualElement>("ClippingControls");

        skullToggle.RegisterValueChangedCallback(evt => layerManager.SetSkullVisible(evt.newValue));
        brainToggle.RegisterValueChangedCallback(evt => layerManager.SetBrainVisible(evt.newValue));
        skullSlider.RegisterValueChangedCallback(evt => layerManager.SetSkullTransparency(evt.newValue));
        brainSlider.RegisterValueChangedCallback(evt => layerManager.SetBrainTransparency(evt.newValue));

        clipXSlider.RegisterValueChangedCallback(evt => layerManager.SetClipRangeX(evt.newValue.x, evt.newValue.y));
        clipYSlider.RegisterValueChangedCallback(evt => layerManager.SetClipRangeY(evt.newValue.x, evt.newValue.y));
        clipZSlider.RegisterValueChangedCallback(evt => layerManager.SetClipRangeZ(evt.newValue.x, evt.newValue.y));

        modeSwitch.RegisterValueChangedCallback(evt =>
        {
            bool isClipMode = evt.newValue;

            if (knob != null) knob.style.left = isClipMode ? 47 : 5;

            if (transparencyControls != null && clippingControls != null)
            {
                transparencyControls.style.display = isClipMode ? DisplayStyle.None : DisplayStyle.Flex;
                clippingControls.style.display = isClipMode ? DisplayStyle.Flex : DisplayStyle.None;
            }

            layerManager.SetMode(isClipMode);
        });
    }
}