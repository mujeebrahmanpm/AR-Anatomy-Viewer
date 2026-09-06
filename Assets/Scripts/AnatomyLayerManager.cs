using UnityEngine;

[System.Serializable]
public class AnatomyLayer
{
    public string layerName;
    public GameObject layerObject;
    public Renderer targetRenderer;
    public Material transparencyMaterial;
    public Material clippingMaterial;
    public float maxAlpha = 0.3f;

    private Material activeInstance;

    public void SetMode(bool isClipMode)
    {
        Material source = isClipMode ? clippingMaterial : transparencyMaterial;
        activeInstance = new Material(source);
        targetRenderer.material = activeInstance;
    }

    public void SetVisible(bool visible)
    {
        layerObject.SetActive(visible);
    }

    public void SetTransparency(float sliderValue)
    {
        if (activeInstance == null) return;
        float alpha = Mathf.Lerp(0f, maxAlpha, sliderValue);
        activeInstance.SetFloat("_Transparency", alpha);
    }

    public void SetClipMinX(float value) { if (activeInstance != null) activeInstance.SetFloat("_ClipMinX", value); }
    public void SetClipMaxX(float value) { if (activeInstance != null) activeInstance.SetFloat("_ClipMaxX", value); }
    public void SetClipMinY(float value) { if (activeInstance != null) activeInstance.SetFloat("_ClipMinY", value); }
    public void SetClipMaxY(float value) { if (activeInstance != null) activeInstance.SetFloat("_ClipMaxY", value); }
    public void SetClipMinZ(float value) { if (activeInstance != null) activeInstance.SetFloat("_ClipMinZ", value); }
    public void SetClipMaxZ(float value) { if (activeInstance != null) activeInstance.SetFloat("_ClipMaxZ", value); }

    public Bounds GetLocalBounds() => targetRenderer.localBounds;
}

public class AnatomyLayerManager : MonoBehaviour
{
    [SerializeField] private AnatomyLayer skull;
    [SerializeField] private AnatomyLayer brain;

    private Vector3 boundsMin;
    private Vector3 boundsMax;

    void Start()
    {
        skull.SetMode(false);
        brain.SetMode(false);

        Bounds combined = skull.GetLocalBounds();
        combined.Encapsulate(brain.GetLocalBounds());
        boundsMin = combined.min;
        boundsMax = combined.max;

        Debug.Log($"Local bounds min: {boundsMin}, max: {boundsMax}");
    }

    public void SetMode(bool isClipMode)
    {
        skull.SetMode(isClipMode);
        brain.SetMode(isClipMode);
    }

    public void SetSkullVisible(bool visible) => skull.SetVisible(visible);
    public void SetBrainVisible(bool visible) => brain.SetVisible(visible);
    public void SetSkullTransparency(float value) => skull.SetTransparency(value);
    public void SetBrainTransparency(float value) => brain.SetTransparency(value);

    public void SetClipRangeX(float minVal, float maxVal)
    {
        // Inverted to match this model's mesh orientation
        SetClipRange(-maxVal, -minVal, Vector3.right);
    }

    public void SetClipRangeY(float minVal, float maxVal)
    {
        SetClipRange(minVal, maxVal, Vector3.up);
    }

    public void SetClipRangeZ(float minVal, float maxVal)
    {
        // Inverted to match this model's mesh orientation
        SetClipRange(-maxVal, -minVal, Vector3.forward);
    }

    void SetClipRange(float minVal, float maxVal, Vector3 axis)
    {
        float axisMin = Vector3.Dot(boundsMin, axis);
        float axisMax = Vector3.Dot(boundsMax, axis);

        float localMin = Mathf.Lerp(axisMin, axisMax, (minVal + 1f) / 2f);
        float localMax = Mathf.Lerp(axisMin, axisMax, (maxVal + 1f) / 2f);

        if (axis == Vector3.right)
        {
            skull.SetClipMinX(localMin); skull.SetClipMaxX(localMax);
            brain.SetClipMinX(localMin); brain.SetClipMaxX(localMax);
        }
        else if (axis == Vector3.up)
        {
            skull.SetClipMinY(localMin); skull.SetClipMaxY(localMax);
            brain.SetClipMinY(localMin); brain.SetClipMaxY(localMax);
        }
        else
        {
            skull.SetClipMinZ(localMin); skull.SetClipMaxZ(localMax);
            brain.SetClipMinZ(localMin); brain.SetClipMaxZ(localMax);
        }
    }
}