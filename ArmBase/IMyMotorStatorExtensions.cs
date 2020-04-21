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
    // This template is intended for extension classes. For most purposes you're going to want a normal
    // utility class.
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
    static class IMyMotorStatorExtensions
    {
        const float TargetVelocityRad = 4f;

        public static IMyMotorStator MoveToPosition(this IMyMotorStator stator, float targetAngle)
        {
            targetAngle %= 360;

            float currentAngle = stator.Angle * 180 / (float) Math.PI;

            float angleDifference = currentAngle - targetAngle;

            if (angleDifference > 0)
            {
                stator.UpperLimitDeg = targetAngle;
                stator.LowerLimitDeg = float.MinValue;

                stator.TargetVelocityRad = TargetVelocityRad;
            }

            if (angleDifference < 0)
            {
                stator.UpperLimitDeg = float.MaxValue;
                stator.LowerLimitDeg = targetAngle;

                stator.TargetVelocityRad = -TargetVelocityRad;
            }

            return stator;
        }
    }
}
