using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRage;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        // This file contains your actual script.
        //
        // You can either keep all your code here, or you can create separate
        // code files to make your program easier to navigate while coding.
        //
        // In order to add a new utility class, right-click on your project, 
        // select 'New' then 'Add Item...'. Now find the 'Space Engineers'
        // category under 'Visual C# Items' on the left hand side, and select
        // 'Utility Class' in the main area. Name it in the box below, and
        // press OK. This utility class will be merged in with your code when
        // deploying your final script.
        //
        // You can also simply create a new utility class manually, you don't
        // have to use the template if you don't want to. Just do so the first
        // time to see what a utility class looks like.
        // 
        // Go to:
        // https://github.com/malware-dev/MDK-SE/wiki/Quick-Introduction-to-Space-Engineers-Ingame-Scripts
        //
        // to learn more about ingame scripts.

        const float JointSpeed = 15;

        bool Error { get; set; }


        IMyShipController ShipController { get; set; }

        IMyMotorAdvancedStator BaseJoint { get; set; }
        IMyMotorAdvancedStator FirstJoint { get; set; }
        IMyMotorAdvancedStator SecondJoint { get; set; }

        IMyTextSurface TextSurface { get; set; }

        public Program()
        {
            BaseJoint = GridTerminalSystem.GetBlockWithName("Crane Rotor Base") as IMyMotorAdvancedStator;

            if (BaseJoint == null)
            {
                Error = true;
            }

            FirstJoint = GridTerminalSystem.GetBlockWithName("Crane Arm 1 Rotor") as IMyMotorAdvancedStator;

            if (FirstJoint == null)
            {
                Error = true;
            }

            SecondJoint = GridTerminalSystem.GetBlockWithName("Crane Arm 2 Rotor") as IMyMotorAdvancedStator;

            if (SecondJoint == null)
            {
                Error = true;
            }



            TextSurface = Me.GetSurface(0);
            TextSurface.ContentType = ContentType.TEXT_AND_IMAGE;
            TextSurface.FontSize = 4;
            TextSurface.Alignment = VRage.Game.GUI.TextPanel.TextAlignment.CENTER;

            ShipController = GridTerminalSystem.GetBlockWithName("Crane Cockpit") as IMyShipController;

            if (ShipController == null)
            {
                Error = true;
            }

            Runtime.UpdateFrequency = UpdateFrequency.Update1;

            // The constructor, called only once every session and
            // always before any other method is called. Use it to
            // initialize your script. 
            //     
            // The constructor is optional and can be removed if not
            // needed.
            // 
            // It's recommended to set Runtime.UpdateFrequency 
            // here, which will allow your script to run itself without a 
            // timer block.
        }

        public void Save()
        {
            // Called when the program needs to save its state. Use
            // this method to save your state to the Storage field
            // or some other means. 
            // 
            // This method is optional and can be removed if not
            // needed.
        }

        public void Main(string argument, UpdateType updateSource)
        {
            // The main entry point of the script, invoked every time
            // one of the programmable block's Run actions are invoked,
            // or the script updates itself. The updateSource argument
            // describes where the update came from. Be aware that the
            // updateSource is a  bitfield  and might contain more than 
            // one update type.
            // 
            // The method itself is required, but the arguments above
            // can be removed if not needed.
            if(Error)
            {
                TextSurface.WriteText("ERROR!");
                return;
            }


            float baseDirection = ShipController.MoveIndicator.X != 0 ? ShipController.MoveIndicator.X / Math.Abs(ShipController.MoveIndicator.X) : 0;
            float baseMovement = (baseDirection * JointSpeed) / 180 * (float) Math.PI;
            BaseJoint.TargetVelocityRad = baseMovement;

            float firstDirection = ShipController.MoveIndicator.Z != 0 ? ShipController.MoveIndicator.Z / Math.Abs(ShipController.MoveIndicator.Z) : 0;
            firstDirection *= -1;
            float firstMovement = (firstDirection * JointSpeed) / 180 * (float)Math.PI;
            FirstJoint.TargetVelocityRad = firstMovement;

            float secondDirection = ShipController.MoveIndicator.Y != 0 ? ShipController.MoveIndicator.Y / Math.Abs(ShipController.MoveIndicator.Y) : 0;
            float secondMovement = (secondDirection * JointSpeed) / 180 * (float)Math.PI;
            SecondJoint.TargetVelocityRad = secondMovement;
        }
    }
}
