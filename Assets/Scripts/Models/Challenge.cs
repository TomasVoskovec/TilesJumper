﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Challenge
{
    public int ID;
    public GroupName Group;
    public string Name;
    public int Difficulty;
    [TextArea(5,10)]
    public string Description;
    public int Reward;
    public int Progress;
    public int Goal;
    public bool Completed;

    public enum GroupName
    {
        JumpMaster,
        DeathJumper,
        ScoreJumper,
        BallMaster,
        ChallengeBeginner,
        ChallengeAmateur,
        ChallengePro,
        ChallengeMaster,
        FirstSteps
    }
}
