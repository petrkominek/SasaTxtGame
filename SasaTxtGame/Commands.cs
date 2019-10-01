using System;
using System.Collections.Generic;
using System.Text;

namespace SasaTxtGame
{
    public class Commands
    {
        Persona user;
        public Commands(Persona p) {
            user = p;
        }

        public void GoForward() {
            user.y += 1;
        }

        public void GoBackward()
        {
            user.y -= 1;
        }
        public void GoLeft()
        {
            user.x -= 1;
        }

        public void GoRight()
        {
            user.x += 1;
        }
        public void TakeItem()
        {
            
        }

        public void ReleaseItem()
        {
            
        }

        public string Help()
        {
            return "Help yourself";
        }

    }
}
