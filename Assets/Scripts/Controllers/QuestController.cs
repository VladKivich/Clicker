using BaseScripts;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class QuestController: BaseController
    {
        private Dictionary<int, List<IQuestEventSender>> sendersList;
        private List<int> requiredQuestEventTypes;
        private ClickerController clickController;

        public QuestController()
        {
            Core.Instance.AddController<QuestController>(this);
            clickController = Core.Instance.GetController<ClickerController>();
        }
    }
}
