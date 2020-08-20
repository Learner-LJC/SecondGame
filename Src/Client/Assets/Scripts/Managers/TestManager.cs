using Common.Data;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : Singleton<TestManager> {
	public void Init()
    {
        NPCManager.Instance.RegisterNpcEvent(Common.Data.NpcFunction.InvokeShop, OnNpcShop);
        NPCManager.Instance.RegisterNpcEvent(Common.Data.NpcFunction.InvokeShop, OnNpcInvoke);
    }

    private bool OnNpcShop(NpcDefine npc)
    {
        Debug.LogFormat("TestManager.OnNpcShop :NPC:[{0}{1}] Type:{2} Function:{3}", npc.ID, npc.Name, npc.Type, npc.Function);
        UITest test=UIManager.Instance.Show<UITest>();
        test.SetTile(npc.Name);
        return true;
    }
    private bool OnNpcInvoke(NpcDefine npc)
    {
        Debug.LogFormat("TestManager.OnNpcInvokeShop :Npc:[{0}{1}] Type:{2} Function{3}", npc.ID, npc.Name, npc.Type, npc.Function);
        MessageBox.Show("点击NPC：" + npc.Name, "NPC对话");
        return true;
    }
}
