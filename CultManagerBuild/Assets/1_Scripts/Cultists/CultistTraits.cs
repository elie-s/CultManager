using System;

namespace CultManager
{
    [Flags]
    public enum CultistTraits
    {
        none = 0,
        TraitA = 1,
        TraitB = 2,
        TraitC = 4,
        TraitD = 8,
        TraitE = 16,
        TraitF = 32,
        TraitG = 64,
        TraitH = 128,
    }
}