using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    public class TraitsSerializeReference
    {
        [SerializeReference, SubclassSelector]
        public ITraitInterface[] traits;

        private void Awake()
        {
            foreach (var trait in traits)
            {
                trait.ExecuteTrait();
            }
        }
    }
}