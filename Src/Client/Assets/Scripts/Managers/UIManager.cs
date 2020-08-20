using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

    class UIElement
    {
        public string Resources;
        public bool Cache;
        public GameObject Instance;
    }
    private Dictionary<Type, UIElement> UIResources = new Dictionary<Type, UIElement>();
    public UIManager()
    {
        this.UIResources.Add(typeof(UITest), new UIElement() { Resources = "UI/UITest", Cache = true });
        this.UIResources.Add(typeof(UIBag), new UIElement(){ Resources = "UI/UIBag", Cache = true });
    }
    ~UIManager()
    { 

    }
    public T Show<T>()
    {
        //SoundManage.Instance.PlaySound("ui_open")
        Type type = typeof(T);
        if (this.UIResources.ContainsKey(type))
        {
            UIElement info = this.UIResources[type];
            if (info.Instance!=null)
            {
                info.Instance.SetActive(true);
            }
            else
            {
                UnityEngine.Object perfab = Resources.Load(info.Resources);
                if (perfab == null)
                {
                    return default(T);
                }
                info.Instance = (GameObject)GameObject.Instantiate(perfab);
            }
            return info.Instance.GetComponent<T>();
        }
        return default(T);
    }
    public void Close(Type type)   
    {
        //SoundManager.Instance.PlaySound("ui_close")
        if (this.UIResources.ContainsKey(type))
        {
            UIElement info = this.UIResources[type];
            if (info.Cache)
            {
                info.Instance.SetActive(false);
            }
            else
            {
                GameObject.Destroy(info.Instance);
                info.Instance = null;
            }
        }
    }
}
