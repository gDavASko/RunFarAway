using System;
using RFW.Pool;
using UnityEngine;

namespace RFW
{
    public class UnitView : MonoBehaviour, IUnitView
    {
        public Type Type => typeof(UnitView);

        private IReleaser<IUnitView> _releaser = null;

        public void Init(params object[] parameters)
        {

        }

        public void DestroySelf()
        {
            Destroy(this.gameObject);
        }

        public void Dispose()
        {
            Release();
        }

        public void SetReleaser(IReleaser<IUnitView> releaser)
        {
            _releaser = releaser;
        }

        public void Release()
        {
            _releaser.Release(this);
        }
    }
}