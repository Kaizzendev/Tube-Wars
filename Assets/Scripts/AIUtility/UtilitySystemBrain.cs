using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIUtility
{
    public class UtilitySystemBrain: MonoBehaviour
    {
        private Team _self;
        [SerializeField] private List<UtilitySystemAction> _actions;
        private IAContext _context = new IAContext();

        private float _thinkTimer;
        private float _thinkRatio = 5f;
        private CombatSystem _combatSystem;
        private bool _isContextDirty;
        public void Init(Team team, CombatSystem combatSystem)
        {
            _self = team;
            _combatSystem = combatSystem;
        }

        public void DirtyContext()
        {
            _isContextDirty = true;
        }

        public void Tick(float deltaTime, List<Team> allTeams, List<Node.Node> allNodes)
        {
            _thinkTimer += deltaTime;
            if (_thinkTimer >= _thinkRatio || _isContextDirty)
            {
                Execute(allTeams, allNodes);
                _thinkTimer = 0f;
                _isContextDirty = false;
            }
        }

        public void Execute(List<Team> allTeams, List<Node.Node> allNodes)
        {
            Sense(allTeams, allNodes);
            Act();
        }
        
        private void Sense(List<Team> allTeams, List<Node.Node> allNodes)
        {
            _context.self =  _self;
            _context.allTeams = allTeams;
            _context.allNodes = allNodes;
            _context.self.totalUnits = GetTeamTotalUnits();
            _context.combatSystem = _combatSystem;
            _context.totalUnits = GetGameTotalUnits();
        }

        private int GetTeamTotalUnits()
        {
            int totalUnits = 0;
            foreach (Node.Node node in _context.self.ownedNodes)
            {
                totalUnits += node.currentUnits;
            }
            
            return totalUnits;
        }

        private int GetGameTotalUnits()
        {
            int totalUnits = 0;
            foreach (Node.Node node in _context.allNodes)
            {
                totalUnits += node.currentUnits;
            }

            return totalUnits;
        }

        private void Act()
        {
            float bestScore = 0;
            UtilitySystemAction bestAction = null;
            
            foreach (var action in _actions)
            {
                float score = action.EvaluateScore(_context);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestAction = action;
                }
                Debug.Log("Action: " + action.name + " score: " + score);
            }
            bestAction.ExecuteAction(_context);
        }
    }
}
