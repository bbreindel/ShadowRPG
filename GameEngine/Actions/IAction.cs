using GameEngine.GameModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Actions
{
    public interface IAction
    {
        event EventHandler<string> OnActionPerformed;
        void Execute(LivingEntity actor, LivingEntity target);
    }
}
