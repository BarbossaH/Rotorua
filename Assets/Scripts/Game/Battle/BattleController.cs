using Common;
using UnityEngine;

namespace Game.Battle
{
    [AutoRegisterController(typeof(IBattleController))]
    public class BattleController : IBattleController
    {
        public void EndEnemyTurn()
        {
            throw new System.NotImplementedException();
        }

        public void EndPlayerTurn()
        {
            throw new System.NotImplementedException();
        }

        public void EndTurn()
        {
            throw new System.NotImplementedException();
        }

        public void OnRoundEnd()
        {
            throw new System.NotImplementedException();
        }

        public void StartBattle()
        {
            Debug.Log("Let's go fight, this is on fire now,wahh");
        }

        public void StartEnemyTurn()
        {
            throw new System.NotImplementedException();
        }

        public void StartPlayerTurn()
        {
            throw new System.NotImplementedException();
        }

        public void StartTurn()
        {
            throw new System.NotImplementedException();
        }
    }
}