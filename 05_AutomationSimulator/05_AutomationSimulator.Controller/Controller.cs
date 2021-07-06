using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_AutomationSimulator
{
    public class Controller
    {
        public static bool slow = false;
        public static bool rightsensor = false;
        public static bool start = false;
        public static bool finish = false;
        public static Outputs move=new Outputs();
        public static Outputs Update(Inputs inputs)
        {
            if (inputs.PositioningEnabled && !finish)
            {
                if (!start)
                {
                    start = true;
                    move.MoveRight = true;
                    move.MoveLeft = false;
                }
                else if (inputs.ProximitySensorMiddle)
                {
                    slow = true;
                    move.MoveSpeed = Configuration.MotorSpeedSlow;
                }
                else if (!slow)
                {
                    move.MoveSpeed = inputs.CurrentTimeInMilliseconds * Configuration.MotorAcceleration;
                }
                else if (inputs.ProximitySensorRight)
                {
                    move.MoveRight = false;
                    move.MoveLeft = true;
                    rightsensor = true;
                    inputs.CurrentTimeInMilliseconds = 0;
                }
                else if (inputs.CurrentTimeInMilliseconds > 9500 && rightsensor == true)
                {
                    move.MoveRight = false;
                    move.MoveLeft = false;
                    finish = true;
                    
                }
                if (move.MoveSpeed > Configuration.MotorSpeedFast)
                    move.MoveSpeed = Configuration.MotorSpeedFast;
            }
            else
                move.MoveSpeed = 0;

            return move;
        }
     
    }
}
