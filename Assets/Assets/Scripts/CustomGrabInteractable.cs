using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomGrabInteractable : XRGrabInteractable
{
    private TwoHandManipulation twoHandManipulation;
    public Material highlightMaterial;
    private Material originalMaterial;
    private MeshRenderer meshRenderer;

    protected override void Awake()
    {
        base.Awake();
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            originalMaterial = meshRenderer.material;
        }
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        if (meshRenderer != null && highlightMaterial != null)
        {
            meshRenderer.material = highlightMaterial;
        }
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        if (meshRenderer != null && originalMaterial != null)
        {
            meshRenderer.material = originalMaterial;
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Find the TwoHandManipulation component and start manipulation
        twoHandManipulation = GetComponent<TwoHandManipulation>();
        if (twoHandManipulation != null)
        {
            // twoHandManipulation.StartManipulating();
            var controllerObject = args.interactorObject.transform;
            string controllerTag = controllerObject.tag;
            Debug.Log($"Grabbed by: {controllerTag}");
            if (controllerObject.name.ToLower().Contains("left"))
            {
                twoHandManipulation.StartManipulating(TwoHandManipulation.ControllerSide.Left);
            }
            else if (controllerObject.name.ToLower().Contains("right"))
            {
                twoHandManipulation.StartManipulating(TwoHandManipulation.ControllerSide.Right);
            }
            else
            {
                twoHandManipulation.StartManipulating(TwoHandManipulation.ControllerSide.None);
            }
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // Stop manipulation when deselected
        if (twoHandManipulation != null)
        {
            twoHandManipulation.StopManipulating();
        }
    }
}
