using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.GameModels
{
    public class QuestStatus : BaseNotificationClass
    {
        private bool _isCompleted;
        public Quest PlayerQuest { get; }
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                _isCompleted = value;
                OnPropertyChanged();
            }
        }

        public QuestStatus(Quest quest)
        {
            PlayerQuest = quest;
            IsCompleted = false;
        }
    }
}
