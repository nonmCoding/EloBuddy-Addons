﻿#region

using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using SimplisticTemplate.Champion.Fizz.Utils;

#endregion

namespace SimplisticTemplate.Champion.Fizz.Modes
{
    internal static class Harass
    {
        public static AIHeroClient Me
        {
            get { return ObjectManager.Player; }
        }

        public static Vector3? LastPos { get; set; }

        public static void Execute()
        {
            var target = TargetSelector.GetTarget(Fizz.Q.Range, DamageType.Magical);
            if (!target.IsValidTarget()) return;

            if (LastPos == null && Me.ServerPosition.IsValid())
            {
                LastPos = Me.ServerPosition;
            }

            var useQ = GameMenu.HarassMenu["useQ"].Cast<CheckBox>().CurrentValue;
            var useW = GameMenu.HarassMenu["useW"].Cast<CheckBox>().CurrentValue;
            var useE = GameMenu.HarassMenu["useE"].Cast<CheckBox>().CurrentValue;
            var useWMode = GameMenu.MiscMenu["useWMode"].Cast<Slider>().CurrentValue;
            var useEMode = GameMenu.HarassMenu["useEMode"].Cast<Slider>().CurrentValue;

            if (LastPos != null && Misc.JumpValid)
            {
                Fizz.E.Cast((Vector3) LastPos);
            }

            if (useW && useWMode == 0 && Me.IsInAutoAttackRange(target))
            {
                Fizz.W.Cast();
            }

            if (Fizz.Q.IsReady() && useQ)
            {
                Fizz.Q.Cast(target);
            }

            if (Fizz.E.IsReady() && useE && useEMode == 1)
            {
                Fizz.E.Cast(target);
            }
        }
    }
}