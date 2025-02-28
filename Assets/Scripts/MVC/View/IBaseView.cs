public interface IBaseView
{
    bool IsShown();
    void InitUI();
    void InitData();

    void ShowView(params object[] data);
    void HideView(params object[] data);

    void DestroyView();

    void SetCanvasEnable(bool isEnabled);

    BaseController BaseController { set; get; }
}