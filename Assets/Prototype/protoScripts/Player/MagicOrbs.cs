using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagicOrbs : MonoBehaviour
{
    public Queue<int> magicElements = new Queue<int>(new int[] { 1, 1, 1 });
    public Player player; 
    public float orbitRadius = 1.5f;
    public float rotationSpeed = 100f;
    public float height = 2f;

    private Transform[] orbs;

    void Start()
    {
        player = GetComponentInParent<Player>();
        orbs = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            orbs[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        HandleMagicInput();
        UpdateOrbColors();
        RotateOrbs();
        player.currentElements = ReadCurrentMagicState();
        player.currentMagicStateID = GetMagicStateID();
    }

    private void HandleMagicInput()
    {
        if (Input.GetKeyDown(KeyCode.J)) UpdateMagicElements(1);
        else if (Input.GetKeyDown(KeyCode.K)) UpdateMagicElements(2);
        else if (Input.GetKeyDown(KeyCode.L)) UpdateMagicElements(3);
    }

    private void UpdateMagicElements(int element)
    {
        if (magicElements.Count >= 3)
            magicElements.Dequeue(); 

        magicElements.Enqueue(element);
    }

    void RotateOrbs()
    {
        float centerX = player.transform.position.x;
        float centerY = player.transform.position.y + height; 

        for (int i = 0; i < orbs.Length; i++)
        {
            float angle = (Time.time * rotationSpeed + i * 120f) * Mathf.Deg2Rad;
            float x = centerX + Mathf.Cos(angle) * orbitRadius;
            float y = centerY + Mathf.Sin(angle) * orbitRadius;
            orbs[i].position = new Vector3(x, y, 0);
        }
    }

    private void UpdateOrbColors()
    {
        int[] elementsArray = magicElements.ToArray();
        for (int i = 0; i < orbs.Length; i++)
        {
            if (i >= elementsArray.Length) continue;

            Color orbColor = Color.white;
            switch (elementsArray[i])
            {
                case 1: orbColor = Color.red; break;
                case 2: orbColor = Color.yellow; break;
                case 3: orbColor = Color.blue; break;
            }
            orbs[i].GetComponent<SpriteRenderer>().color = orbColor;
        }
    }

    public string ReadCurrentMagicState()
    {
        int[] elementsArray = magicElements.ToArray();
        string magicState = "";

        foreach (int element in elementsArray)
        {
            switch (element)
            {
                case 1: magicState += "Fire "; break;
                case 2: magicState += "Light "; break;
                case 3: magicState += "Wind "; break;
            }
        }

        return magicState.Trim();
    }

    public int GetMagicStateID()
    {
        var elementCounts = magicElements.GroupBy(x => x)
                                         .OrderByDescending(g => g.Count())
                                         .ThenBy(g => g.Key)
                                         .Select(g => g.Key)
                                         .ToArray();

        // Convert the unique combination into an integer ID for efficient lookup
        int magicID = 0;
        foreach (int element in elementCounts)
        {
            magicID = magicID * 10 + element; 
        }
        return magicID;
    }

}
