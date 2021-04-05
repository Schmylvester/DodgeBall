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
        public Vector3 position;
    }
    
    [SerializeField] GameObject m_unitPrefab;
    [SerializeField] Character[] m_characters;
    [SerializeField] Team[] m_teams;
    
    void Start()
    {
        Vector2[] positions =
        {
            new Vector2(-6, 4),
            new Vector2(6, 4),
            new Vector2(-3, 2),
            new Vector2(3, 2),
            new Vector2(-6, 0),
            new Vector2(6, 0),
            new Vector2(-3, -2),
            new Vector2(3, -2),
            new Vector2(-6, -4),
            new Vector2(6, -4)
        };
        for (int i = 0; i < 10; ++i)
        {
            int team = i % 2;
            UnitToSpawn spawnUnit = new UnitToSpawn
            {
                characterIndex = Random.Range(0,2),
                team = team,
                position = positions[i]
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
        instance.transform.localPosition = _unit.position;
        instance.GetComponent<Animator>().runtimeAnimatorController = m_characters[_unit.characterIndex].animationController;
    }
}
