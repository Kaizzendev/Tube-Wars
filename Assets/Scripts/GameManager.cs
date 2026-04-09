using System;
using System.Collections.Generic;
using AIUtility;
using Node;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Team> _allTeams;
    public NodeManager nodeManager;
    private List<UtilitySystemBrain> _brains =  new List<UtilitySystemBrain>();
    public GameObject brainPrefab;
    
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        
    }

    private void Start()
    {
        BuildTeamsFromNodes();
        GenerateBrains();
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        foreach (UtilitySystemBrain brain in _brains)
        {
            brain.Tick(deltaTime, _allTeams, nodeManager.GetNodes());
        }
    }

    private void BuildTeamsFromNodes()
    {
        Dictionary<int, Team> teamsById = new Dictionary<int, Team>();

        foreach (var node in nodeManager.GetNodes())
        {
            int id = node.ownerId;
            
            if (id is 0 or 1) continue;

            if (!teamsById.ContainsKey(id))
            {
                Team team = new Team
                {
                    id = id,
                    ownedNodes = new List<Node.Node>()
                };
                    teamsById.Add(id, team);
            }
            teamsById[id].ownedNodes.Add(node);
        }
        _allTeams = new List<Team>(teamsById.Values);
    }
    
    //TODO: Remove or Add conquered nodes to teams using events
    

    private void GenerateBrains()
    {
        foreach (Team team in _allTeams)
        {
            if (team.id > 1)
            {
                var brain = Instantiate(brainPrefab);
                _brains.Add(brain.GetComponent<UtilitySystemBrain>());
                brain.GetComponent<UtilitySystemBrain>().Init(team);
            }
        }
    }
}
