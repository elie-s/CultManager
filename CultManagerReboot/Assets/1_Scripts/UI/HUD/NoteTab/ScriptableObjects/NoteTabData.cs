using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/UI/NoteTab/NoteTabData")]
    public class NoteTabData : ScriptableObject, ILoadable
    {
        public List<NoteTabSegment> noteTabSegments;

        public void SetSegments(NoteTabSegment[] _noteTabSegments)
        {
            noteTabSegments = new List<NoteTabSegment>();
            for (int i = 0; i < _noteTabSegments.Length; i++)
            {
                noteTabSegments.Add(_noteTabSegments[i]);
            }
        }


        public void LoadSave(Save _save)
        {
            SetSegments(_save.noteTabSegments);
        }
    }
}

