using System.Collections.Generic;
using UnityEngine;

public class ViewInfo
{
    public string UIPrefabName; 
    public Transform Parent; //some uis are displayed at the very top of the screen and some are displayed in the scene
    public int SortingOrder;
    public BaseController Controller;
}
public class ViewManager
{
    private readonly Transform _UICanvas = GameObject.Find("UICanvas").transform;
    private readonly Transform _SceneCanvas = GameObject.Find("SceneCavans").transform;

    private readonly Dictionary<ViewType, BaseView> _views = new();
    private readonly Dictionary<ViewType, BaseView> _viewsOpened = new();
    private readonly Dictionary<ViewType, ViewInfo> _viewsInfo = new();

    public void RegisterView(ViewType viewType, BaseController controller, int sortingOrder=0, bool isInScene=false)
    {
        ViewInfo info = new ViewInfo()
        {
            UIPrefabName = viewType.ToString(),
            Controller = controller,
            SortingOrder = sortingOrder,
            Parent = isInScene ? _SceneCanvas : _UICanvas,
        };

        _viewsInfo.TryAdd(viewType, info);
    }

    public void UnregisterView(ViewType viewType) 
    {
        if (_viewsInfo.ContainsKey(viewType))
        {
            _viewsInfo.Remove(viewType);
        }
        if (_viewsOpened.ContainsKey(viewType))
        {
            _viewsOpened.Remove(viewType);
        }
        if (_views.ContainsKey(viewType))
        {
            _views.Remove(viewType);
        }
    }

    public void ShowView(ViewType viewType)
    {
        //check if it's in the views, if not, then create it
    }
}