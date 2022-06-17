using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    [SerializeField] Text m_tipText = null;
    [SerializeField] Button[] m_buttons = null;
    [SerializeField] Sprite[] m_icons = null;
    [SerializeField] Image[] m_actionHighlights = null;
    [SerializeField] Image[] m_actionIconIndicators = null;
    [SerializeField] UnitSelection m_unitSelection = null;
    [SerializeField] EntitySpawner m_entitySpawner = null;
    [SerializeField] GameObject m_readyButton = null;
    [HideInInspector] public Unit m_selectedUnit = null;
    Action m_waitingForAClick = Action.Null;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && m_waitingForAClick != Action.Null)
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (m_waitingForAClick == Action.Move)
            {
                Vector2 movePos = getPointOnMoveRadius(clickPos);
                m_selectedUnit.addMoveTarget(movePos);
                addAction(Action.Move);
                m_selectedUnit.showMoveRadius(false);
                finishWaitingForClick();
            }
            else if (m_waitingForAClick == Action.Pass)
            {
                List<Unit> unitsNearClick = m_unitSelection.getNearbyUnitsToClick(clickPos);
                unitsNearClick.Remove(m_selectedUnit);
                if (unitsNearClick.Count > 0)
                {
                    m_selectedUnit.m_passTo = unitsNearClick[0];
                    addAction(Action.Pass);
                    finishWaitingForClick();
                }
            }
            else if (m_waitingForAClick == Action.Throw)
            {
                m_selectedUnit.m_throwDirection = (clickPos - (Vector2)m_selectedUnit.m_handTransform.position).normalized;
                addAction(Action.Throw);
                m_selectedUnit.showAimCone(false);
                finishWaitingForClick();
            }
            else
            {
                Debug.LogError("Something didn't get set correctly, I'm waiting for a click for an action that doesn't need one");
            }
        }
    }

    void finishWaitingForClick()
    {
        m_unitSelection.setCanSelectUnit(true);
        m_waitingForAClick = Action.Null;
        m_tipText.text = "";
    }

    Vector2 getPointOnMoveRadius(Vector2 clickPos)
    {
        return Vector2.zero;
    }

    void updateActionQueueUI(Unit _unit, bool _selected)
    {
        for (int i = 0; i < m_actionHighlights.Length; ++i)
        {
            int queueIndex = _unit.actionQueueLength();
            Color indicatorColour = (queueIndex == i && _selected) ? GameManager.Get.constants.g_actionCurrentlySelectingColor
                : queueIndex > i ? GameManager.Get.constants.g_actionFinishedSelectingColour
                : GameManager.Get.constants.g_actionNotSelectedColor;
            m_actionHighlights[i].color = indicatorColour;
            _unit.setActionQueueRenderer(i, indicatorColour);
        }
        if (_selected)
        {
            for (int i = 0; i < m_actionIconIndicators.Length; ++i)
            {
                Action action = _unit.getActionInQueue(i);
                if (action == Action.Null)
                {
                    m_actionIconIndicators[i].sprite = null;
                }
                else
                {
                    m_actionIconIndicators[i].sprite = m_icons[(int)action];
                }
            }
        }
        if (m_entitySpawner.allUnitsActionsSelected())
        {
            m_readyButton.SetActive(true);
        }
    }

    /// <summary>
    /// rather than set buttons to interactable, we should
    /// just change the colour of buttons we don't think are good
    /// because the state of the game is uncertain we should not
    /// disable buttons that we think won't be available
    /// </summary>
    /// <param name="_button">the button to change</param>
    /// <param name="_buttonGood">0 - 1, chances that this action will succeed</param>
    void applyButtonColour(Button _button, float _buttonGood)
    {
        ColorBlock colourSet = _button.colors;
        colourSet.normalColor = Color.Lerp(
            GameManager.Get.constants.g_awfulActionColour,
            GameManager.Get.constants.g_fineActionColour,
            _buttonGood);
        colourSet.selectedColor = Color.Lerp(
            GameManager.Get.constants.g_awfulActionColour,
            GameManager.Get.constants.g_fineActionColour,
            _buttonGood);
        _button.interactable = _buttonGood != 0;
        _button.colors = colourSet;
    }

    float calculateNearBallPrediction(Unit _unit)
    {
        if (_unit.nearBall())
            return 1;
        return _unit.getActionInQueue(0) == Action.Null ? 0.05f : 0.2f;
    }

    float calculateHasBallPrediction(Unit _unit)
    {
        if (_unit.hasBall())
            return 1.0f;

        if (_unit.getActionInQueue(0) == Action.Catch)
        {
            return 0.25f;
        }

        return 0.0f;
    }

    void updateButtonInteractability(Unit _unit)
    {
        if (_unit.actionQueueLength() >= 2)
        {
            foreach(Button button in m_buttons)
            {
                button.interactable = false;
            }
        }
        else
        {
            // currently no conditions for whether a player can move
            applyButtonColour(m_buttons[(int)Action.Move], 1);
            // you can only throw a ball if you have one
            applyButtonColour(m_buttons[(int)Action.Throw], calculateHasBallPrediction(_unit));
            // currently no conditions for whether a player can wait
            applyButtonColour(m_buttons[(int)Action.Wait], 1);
            // you can only catch a ball if you don't have one
            applyButtonColour(m_buttons[(int)Action.Catch], 1 - calculateHasBallPrediction(_unit));
            // rushing for both actions wouldn't do anything
            applyButtonColour(m_buttons[(int)Action.Rush], _unit.getActionInQueue(0) != Action.Rush ? 1: 0);
            // you can pick up a ball if you're near one and don't already have one
            applyButtonColour(m_buttons[(int)Action.PickUp], Mathf.Max(calculateNearBallPrediction(_unit) - calculateHasBallPrediction(_unit), 0));
            // you can only block with a ball
            applyButtonColour(m_buttons[(int)Action.Block], calculateHasBallPrediction(_unit));
            // you can only pass a ball if you have one
            applyButtonColour(m_buttons[(int)Action.Pass], calculateHasBallPrediction(_unit));
        }
    }

    public void onUnitSelected(Unit _unit)
    {
        if (m_selectedUnit) updateActionQueueUI(m_selectedUnit, false);
        m_selectedUnit = _unit;
        updateActionQueueUI(_unit, true);
        updateButtonInteractability(_unit);
    }

    public void onActionSelected(int _action)
    {
        switch ((Action)_action)
        {
            case Action.Move:
                m_unitSelection.setCanSelectUnit(false);
                m_selectedUnit.showMoveRadius(true);
                m_tipText.text = "Move where?";
                m_waitingForAClick = Action.Move;
                break;
            case Action.Throw:
                m_unitSelection.setCanSelectUnit(false);
                m_selectedUnit.showAimCone(true);
                m_tipText.text = "Throw where?";
                m_waitingForAClick = Action.Throw;
                break;
            case Action.Pass:
                m_unitSelection.setCanSelectUnit(false);
                m_tipText.text = "Pass to whom?";
                m_waitingForAClick = Action.Pass;
                break;
            case Action.Wait:
            case Action.PickUp:
            case Action.Block:
            case Action.Catch:
            case Action.Rush:
                addAction((Action)_action);
                break;
            default:
                Debug.LogError("Unhandled action button");
                break;
        }
    }

    void addAction(Action _action)
    {
        m_selectedUnit.addActionToQueue(_action);
        updateActionQueueUI(m_selectedUnit, true);
        updateButtonInteractability(m_selectedUnit);
    }
}
