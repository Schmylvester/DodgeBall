using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Character
    {
        public string name;
        public RuntimeAnimatorController animationController;
        public Sprite sprite;
        public Vector3 gridOffset;
    }

    [System.Serializable]
    public struct Team
    {
        public string name;
        public Color colour;
        public int facingDirection;
    }

    public struct UnitToSpawn
    {
        public int characterIndex;
        public int team;
        public Vector2Int gridPosition;
    }
    
    [SerializeField] GameObject m_unitPrefab;
    [SerializeField] Character[] m_characters;
    [SerializeField] Team[] m_teams;
    [SerializeField] UnityEngine.Grid m_grid;

    void Start()
    {
        for (int i = 0; i < 12; ++i)
        {
            UnitToSpawn spawnUnit = new UnitToSpawn
            {
                characterIndex = Random.Range(0,2),
                team = i % 2,
                gridPosition = new Vector2Int(Random.Range(0, 5) + (5 * (i % 2)), Random.Range(0, 7))
            };
            spawn(spawnUnit);
        }
    }

    public void spawn(UnitToSpawn _unit)
    {
        GameObject instance = Instantiate(m_unitPrefab, transform);
        SpriteRenderer spriteRenderer = instance.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = m_characters[_unit.characterIndex].sprite;
        spriteRenderer.color = m_teams[_unit.team].colour;
        spriteRenderer.flipX = m_teams[_unit.team].facingDirection == -1;
        instance.transform.position = m_grid.CellToWorld(new Vector3Int(_unit.gridPosition.x, _unit.gridPosition.y, 0)) + m_characters[_unit.characterIndex].gridOffset;
        instance.GetComponent<Animator>().runtimeAnimatorController = m_characters[_unit.characterIndex].animationController;
    }
}
