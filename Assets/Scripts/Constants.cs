using UnityEngine;

public class Constants
{
    public Color g_actionNotSelectedColor { get; } = Color.clear;
    public Color g_actionCurrentlySelectingColor { get; } = Color.yellow;
    public Color g_actionFinishedSelectingColour { get; } = Color.green;

    public int g_maxActionsWithBall { get; } = 3;
}
