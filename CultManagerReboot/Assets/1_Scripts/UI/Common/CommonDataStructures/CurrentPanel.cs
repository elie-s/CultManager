using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public enum CurrentIsland
    {
        Transition = -2,
        All = -1,
        Origin =0,
        RecruitmentIsland=1,
        SacrificeIsland=2,
        AltarIsland=3,
        PuzzleIsland=4,
        SummonArea=5,
    }
    public enum CurrentPanel
    {
        None=0,
        NoteTabPanel = 1,
        RecruitmentPanel = 2,
        AltarPanel = 3,
        PuzzlePanel = 4,
        DemonBook = 5,
        HotKeys=6,
        PolicePanel=7,
        DemonPage=8,
        DemonTreePanel,
        DemonStatuePanel,
    }
}

