using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadSystem
{
    [System.Serializable]
    public class LevelWinCondition
    {
        public MissionWinConditionType WinConditionType = MissionWinConditionType.ReachFinishPoint;

        public Position FinishPoint;
        public bool PointPassPositiveDirection = true;

        /// <summary>
        /// 0 time limit = no time limit
        /// </summary>
        public float TimeLimit = 0;

        public float CurrentTimerOfMission { get; set; } = 0;



        public LevelWinCondition()
        {

        }
        public LevelWinCondition(Vector3 finishPoint)
        {
            WinConditionType = MissionWinConditionType.ReachFinishPoint;
            FinishPoint = new Position(finishPoint);
        }

        public bool CheckWinCondition(Vector3 playerCarPosition)
        {
            if (WinConditionType == MissionWinConditionType.ReachFinishPoint)
            {
                if (PointPassPositiveDirection) return playerCarPosition.x > FinishPoint.X;
                else return playerCarPosition.x < FinishPoint.X;
            }
            else if (WinConditionType == MissionWinConditionType.TimeRace)
            {

            }
            else if (WinConditionType == MissionWinConditionType.Transportation)
            {

            }
            return false;
        }


        [System.Serializable]
        public enum MissionWinConditionType : byte
        {
            ReachFinishPoint = 0,
            TimeRace = 1,
            Transportation = 2
        }
    }
}
