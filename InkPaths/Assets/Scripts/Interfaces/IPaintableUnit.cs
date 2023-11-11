using UnityEngine;

public interface IPaintableUnit
{
    public void Paint(Color color);
    public bool IsPainted();
    public Color GetColor();
}
