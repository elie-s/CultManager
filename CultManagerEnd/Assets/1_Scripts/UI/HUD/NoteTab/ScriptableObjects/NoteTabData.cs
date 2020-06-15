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

        public void AddSegment(NoteTabSegment noteTabSegment)
        {
            noteTabSegments.Add(noteTabSegment);
        }

        public void RemoveSegment(NoteTabSegment noteTabSegment)
        {
            noteTabSegments.Remove(noteTabSegment);
        }

        public void Reset()
        {
            noteTabSegments.Clear();
        }


        public void LoadSave(Save _save)
        {
            SetSegments(_save.noteTabSegments);
        }
    }
}

