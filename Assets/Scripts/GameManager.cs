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
    public NodeSelectionSystem nodeSelectionSystem;
    [SerializeField] private CombatSystem _combatSystem;
    private Dictionary<int, Team> _teamsById = new Dictionary<int, Team>();
    public SquadManager squadManager;
    
    public static GameManager Instance;
    
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    private void OnEnable()
    {
        GameEventManager.onChangeLeader += HandleNodeChange;
    }

    private void OnDisable()
    {
        GameEventManager.onChangeLeader -= HandleNodeChange;
    }

    private void HandleNodeChange(Node.Node node, int oldTeam, int newTeam)
    {
        ChangeTeam(node, oldTeam, newTeam);
        foreach (UtilitySystemBrain brain in _brains)
        {
            brain.DirtyContext();
        }
    }

    private void ChangeTeam(Node.Node node, int oldTeamId, int newTeamId)
    {
        bool isSquadAlive = false;

        Team oldTeam = _teamsById[oldTeamId];
        Team newTeam = _teamsById[newTeamId];

        oldTeam.ownedNodes.Remove(node);
        newTeam.ownedNodes.Add(node);

        for (int i = 0; i < squadManager.squads.Count; i++)
        {
            if (squadManager.squads[i].ownerId == oldTeamId)
            {
                isSquadAlive = true;
                break;
            }
        }
        
        if (oldTeam.ownedNodes.Count <= 0 && !isSquadAlive)
        {
            _teamsById.Remove(oldTeamId);
            _brains.Remove(oldTeam.brain);
            _allTeams.Remove(oldTeam);
            
            Destroy(oldTeam.brain);
        }
    }

    private void Start()
    {
        InitPlayer();
        BuildTeamsFromNodes();
        GenerateBrains();
    }

    private void InitPlayer()
    {
        nodeSelectionSystem.Init(_combatSystem);
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
        _teamsById = new Dictionary<int, Team>();

        foreach (var node in nodeManager.GetNodes())
        {
            int id = node.ownerId;

            if (!_teamsById.ContainsKey(id))
            {
                Team team = new Team
                {
                    id = id,
                    ownedNodes = new List<Node.Node>()
                };
                _teamsById.Add(id, team);
            }
            _teamsById[id].ownedNodes.Add(node);
        }
        _allTeams = new List<Team>(_teamsById.Values);
    }
    
    private void GenerateBrains()
    {
        foreach (Team team in _allTeams)
        {
            if (team.id > 1)
            {
                GameObject parent = new GameObject("BrainParent");
                var brain = Instantiate(brainPrefab, parent.transform);
                _brains.Add(brain.GetComponent<UtilitySystemBrain>());
                team.brain = brain.GetComponent<UtilitySystemBrain>();
                brain.GetComponent<UtilitySystemBrain>().Init(team, _combatSystem);
            }
        }
    }
}
