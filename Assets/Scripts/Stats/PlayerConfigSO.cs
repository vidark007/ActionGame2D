using ActionGame.Stats;
using UnityEngine;

namespace ActionGame.Stats
{
    [CreateAssetMenu(fileName = "NewPlayer", menuName = "Characters/New Player", order = 0)]
    public class PlayerConfigSO : CharacterConfigSO
    {
        public readonly bool isPlayer = true;
        
    }
}
