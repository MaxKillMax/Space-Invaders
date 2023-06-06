using System;
using System.Linq;
using SpaceInvaders.LiveObjects.LiveComponents.Targets;
using UnityEngine;

namespace SpaceInvaders.LiveObjects.LiveComponents.Healths
{
    /// <summary>
    /// Indicates that the LiveObject may die
    /// </summary>
    public class Health : LiveComponent
    {
        public event Action OnDestroyed;
        public event Action OnChanged;

        private GameObject _gameObject;
        private Target _target;

        public float MaxAmount { get; private set; }
        public float Amount { get; private set; }

        public Health(HealthConstructData data)
        {
            _gameObject = data.GameObject;
            _target = data.Target;
            MaxAmount = data.Amount;
            Amount = data.Amount;
        }

        /// <summary>
        /// To act damage, need to healthAction with negative amount value
        /// </summary>
        public bool TryAct(HealthAction action)
        {
            if (!action.TargetTeamIndexs.Any((i) => i == _target.TeamIndex))
                return false;

            Amount += action.Amount;
            OnChanged?.Invoke();

            if (Amount <= 0)
                Destroy();
            else if (Amount > MaxAmount)
                Amount = MaxAmount;

            return true;
        }

        public void Destroy()
        {
            Amount = 0;
            OnDestroyed?.Invoke();
            UnityEngine.Object.Destroy(_gameObject);
        }

        public override void TryReplace(LiveComponent component)
        {
            if (component is not Health health)
                return;

            _gameObject = health._gameObject;
            _target = health._target;
            MaxAmount = health.MaxAmount;
            Amount = health.Amount;
        }
    }
}
