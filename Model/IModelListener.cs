using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interaction.Model
{
    public interface IModelListener
    {
        void shootFired(Microsoft.Xna.Framework.Vector2 possition);
    }
}
