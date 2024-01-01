using System;
using UnityEngine;

public enum HexColor {
    Green,
    Brown,
    Blue
}

public class HexPrism : MonoBehaviour
{
    public Material greenMaterial, brownMaterial, blueMaterial; // Assign these in the Inspector
    private HexColor _color;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        SetColor(HexColor.Blue);
    }

    public void TransitionState()
    {
        switch (_color)
        {
            case HexColor.Green:
                SetColor(HexColor.Brown);
                break;
            case HexColor.Brown:
                SetColor(HexColor.Blue);
                break;
            case HexColor.Blue:
                SetColor(HexColor.Green);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetColor(HexColor color)
    {
        _color = color;
        _renderer.material = _color switch
        {
            HexColor.Green => greenMaterial,
            HexColor.Brown => brownMaterial,
            HexColor.Blue => blueMaterial,
            _ => _renderer.material
        };
    }
}
