using Common.Data;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managers
{
    public class NPCManager:Singleton<NPCManager>
    {
        public delegate bool NpcActionHandler(NpcDefine npc);
        public Dictionary<NpcFunction, NpcActionHandler> eventMap = new Dictionary<NpcFunction, NpcActionHandler>();

        public void RegisterNpcEvent(NpcFunction function,NpcActionHandler action)
        {
            if (!eventMap.ContainsKey(function))
            {
                eventMap[function] = action;
            }
            else
            {
                eventMap[function] += action;
            }
        }
		//获取NPC信息
        public NpcDefine GetNpcDefine(int npcId)
        {
            NpcDefine npc = null;
            DataManager.Instance.Npcs.TryGetValue(npcId, out npc);
            return npc;
        }
		//用户交互函数
        public bool Interactive(int npcId)
        {
            if (DataManager.Instance.Npcs.ContainsKey(npcId))
            {
                var npc = DataManager.Instance.Npcs[npcId];
                return Interactive(npc);
            }
            return false;
        }
        public bool Interactive(NpcDefine npc)
        {
            if (npc.Type == NpcType.Task)
            {
                return DoTaskInteractive(npc);
            }
            else if (npc.Type == NpcType.Functional)
            {
                return DoFuntionInteractive(npc);
            }
            return false;
        }

        public bool DoFuntionInteractive(NpcDefine npc)
        {
            if (npc.Type != NpcType.Functional)
            {
                return false;
            }
            if (!eventMap.ContainsKey(npc.Function))
            {
                return false;
            }
            return eventMap[npc.Function](npc);
        }

        public bool DoTaskInteractive(NpcDefine npc)
        {
            MessageBox.Show("点击了NPC:" + npc.Name, "NPC对话");
            return true;
        }
    }
}
