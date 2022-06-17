using UnityEngine;

public class Constants
{
    public Color g_actionNotSelectedColor { get; } = Color.clear;
    public Color g_actionCurrentlySelectingColor { get; } = Color.yellow;
    public Color g_actionFinishedSelectingColour { get; } = Color.green;

    public Color g_fineActionColour { get; } = new Color(0.7f, 1.0f, 0.7f);
    public Color g_awfulActionColour { get; } = new Color(1.0f, 0.7f, 0.7f);

    public int g_maxActionsWithBall { get; } = 3;
}
