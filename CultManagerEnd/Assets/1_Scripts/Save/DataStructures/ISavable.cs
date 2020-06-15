using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public interface ILoadable
    {
        void LoadSave(Save _save);
    }
}