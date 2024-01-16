using UnityEngine;

namespace GlassyCode.TTT.Core.UI
{
    public abstract class Panel : MonoBehaviour
    {
        protected void Hide() => gameObject.SetActive(false);
        protected void Show() => gameObject.SetActive(true);
    }
}