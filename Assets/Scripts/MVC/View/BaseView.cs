namespace MVC
{
    using UnityEngine;

    public class BaseView : MonoBehaviour, IBaseView
    {
        protected Canvas canvas; //each view has a independent canvas as its parent
        private void Awake()
        {
            canvas = GetComponent<Canvas>();
        }
        public BaseController BaseController { get; set; }

        public void DestroyView()
        {
            throw new System.NotImplementedException();
        }

        public void InitData()
        {
        }

        public void InitUI()
        {
        }

        public bool IsShown()
        {
            //canvas.isActiveAndEnabled, this check the gameobject is active or not
            return canvas.enabled;
        }

        public void SetCanvasEnable(bool isEnabled)
        {
            canvas.enabled = isEnabled;
        }

        public void ShowView(params object[] data)
        {
            SetCanvasEnable(true);
        }

        public void HideView(params object[] data)
        {
            SetCanvasEnable(false);
        }

    }

}