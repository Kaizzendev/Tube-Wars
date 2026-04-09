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

        public void Init(Team team)
        {
            _self = team;
        }
        
        public void Tick(float deltaTime, List<Team> allTeams, List<Node.Node> allNodes)
        {
            _thinkTimer += deltaTime;
            if (_thinkTimer >= _thinkRatio)
            {
                Execute(allTeams, allNodes);
                _thinkTimer = 0f;
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
            _context.self.totalUnits = 200;
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
            }
            bestAction.ExecuteAction(_context);
        }
    }
}
