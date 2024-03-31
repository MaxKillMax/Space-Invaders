using UnityEngine;

namespace SI.Purchasings
{
    public abstract class Product : ScriptableObject
    {
        protected const string PATH_START = "Products/";
        protected const int ORDER = 51;

        [SerializeField] private Sprite _icon;
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private float _price;
        [SerializeField] private bool _singleTime;

        public Sprite Icon => _icon;
        public string Title => _title;
        public string Description => _description;
        public float Price => _price;
        public bool SingleTime => _singleTime;

        public abstract void Execute();
    }
}