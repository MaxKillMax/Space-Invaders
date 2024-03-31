using UnityEngine;

namespace SI.Purchasings
{
    [CreateAssetMenu(fileName = nameof(RemoveLastProduct), menuName = PATH_START + nameof(RemoveLastProduct), order = ORDER)]
    public class RemoveLastProduct : Product
    {
        public override void Execute() => ResearchTree.RemoveLastProduct();
    }
}