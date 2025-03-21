using System.Collections.Generic;
using UnityEngine;

public class ViewInfo
{
    public string UIPrefabName;
    public Transform Parent; //some uis are displayed at the very top of the screen and some are displayed in the scene
    public int SortingOrder;
    public BaseController Controller;
}
// public class ViewManager : Singleton<ViewManager>
//TODO: it cannot inherit from Singleton now, because there is not canvas for now, I will add them later.
public class ViewManager
{
    private readonly Transform _UICanvas = GameObject.Find("UICanvas").transform;
    private readonly Transform _SceneCanvas = GameObject.Find("SceneCanvas").transform;

    private readonly Dictionary<ViewType, ViewInfo> _viewsInfo = new();
    private readonly Dictionary<ViewType, BaseView> _views = new();
    private readonly HashSet<ViewType> _viewsOpened = new(); //to store views that have been instantiated


    //we must be clear that view registration involves basic information, not the view object.
    public void RegisterView(ViewType viewType, int sortingOrder = 0, bool isInScene = false)
    {
        ViewInfo info = new ViewInfo()
        {
            UIPrefabName = viewType.ToString(),
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
        if (_viewsOpened.Contains(viewType))
        {
            _viewsOpened.Remove(viewType);
        }
        if (_views.ContainsKey(viewType))
        {
            _views.Remove(viewType);
        }
    }

    public IBaseView GetView(ViewType viewType)
    {
        //if(_viewsOpened.TryGetValue(viewType, out var view))
        //{
        //    return view;
        //}
        //return _views.GetValueOrDefault(viewType);
        if (_viewsOpened.Contains(viewType))
        {
            return _views.GetValueOrDefault(viewType);
        }

        return null;
    }
    public void ShowView(ViewType viewType)
    {
        ViewInfo viewInfo = _viewsInfo.GetValueOrDefault(viewType);
        if (viewInfo == null)
        {
            Debug.LogFormat("View {0} doesn't exist", viewType);
            //Debug.Log(string.Format("View {0} doesn't exist", viewType));
            return;
        }
        //check if it's in the views, if not, then create it
        IBaseView view = GetView(viewType);

        if (view == null)
        {

        }
    }
}