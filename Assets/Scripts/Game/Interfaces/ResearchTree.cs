using System.Collections.Generic;
using SI.Purchasings;
using UnityEngine;
using UnityEngine.Assertions;

namespace SI
{
    public class ResearchTree : MonoBehaviour
    {
        private static ResearchTree Instance;

        public const string TIER_KEY = "TIER";
        public const string PRODUCTS_KEY = "APPLIED_PRODUCTS";
        private const int PRODUCTS_PER_TIER = 2;

        [SerializeField] private Product[] _products;

        private List<int> _appliedProducts;
        private int _currentTier = 0;

        private int FirstProductIndex => _currentTier * PRODUCTS_PER_TIER;
        public Product FirstProduct => GetProductOfIndex(FirstProductIndex);

        private int SecondProductIndex => (_currentTier * PRODUCTS_PER_TIER) + 1;
        public Product SecondProduct => GetProductOfIndex(SecondProductIndex);

        private Product GetProductOfIndex(int index) => _products.Length > index ? null : _products[index];

        public void Initialize()
        {
            Assert.IsNull(Instance);

            Instance = this;

            if (Saving.Load(TIER_KEY, out int tier))
                _currentTier = tier;

            if (Saving.Load(PRODUCTS_KEY, out List<int> products))
                _appliedProducts = products;

            for (int i = 0; i < _appliedProducts.Count; i++)
            {
                if (_products[_appliedProducts[i]].SingleTime)
                    continue;

                _products[_appliedProducts[i]].Execute();
            }
        }

        public static void ExecuteProduct(Product product)
        {
            if (Instance.FirstProduct == product)
                Instance._appliedProducts.Add(Instance.FirstProductIndex);
            else if (Instance.SecondProduct == product)
                Instance._appliedProducts.Add(Instance.SecondProductIndex);
            else
                throw new System.Exception("Wrong product exception");

            Instance._currentTier++;
            Saving.Save(TIER_KEY, Instance._currentTier);

            product.Execute();
            Saving.Save(PRODUCTS_KEY, Instance._appliedProducts);
        }

        public static void RemoveLastProduct()
        {
            Instance._currentTier--;
            Saving.Save(TIER_KEY, Instance._currentTier);

            Instance._appliedProducts.RemoveAt(Instance._appliedProducts.Count - 1);
            Saving.Save(PRODUCTS_KEY, Instance._appliedProducts);
        }
    }
}