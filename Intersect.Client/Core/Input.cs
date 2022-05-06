using System;

using Intersect.Admin.Actions;
using Intersect.Client.Core.Controls;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game;
using Intersect.Client.Maps;
using Intersect.Client.Entities;
using Intersect.Client.Networking;
using Intersect.Logging;
using Intersect.Utilities;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.MonoGame.Input;
using Intersect.Client.Core;


namespace Intersect.Client.Core
{

    public static class Input
    {

        public delegate void HandleKeyEvent(Keys key);

        public static HandleKeyEvent KeyDown;

        public static HandleKeyEvent KeyUp;

        public static HandleKeyEvent MouseDown;

        public static HandleKeyEvent MouseUp;


        public static void OnKeyPressed(Keys key)
        {
            if (key == Keys.None)
            {
                return;
            }

            var consumeKey = false;
            bool canFocusChat = true;

            KeyDown?.Invoke(key);
            switch (key)
            {
                case Keys.Escape:
                    if (Globals.GameState != GameStates.Intro)
                    {
                        break;
                    }

                    Fade.FadeIn();
                    Globals.GameState = GameStates.Menu;

                    return;

                case Keys.Enter:

                    for (int i = Interface.Interface.InputBlockingElements.Count - 1; i >= 0; i--)
                    {
                        try
                        {
                            var iBox = (InputBox)Interface.Interface.InputBlockingElements[i];
                            if (iBox != null && !iBox.IsHidden)
                            {
                                iBox.okayBtn_Clicked(null, null);
                                canFocusChat = false;

                                break;
                            }
                        }
                        catch { }

                        try
                        {
                            var eventWindow = (EventWindow)Interface.Interface.InputBlockingElements[i];
                            if (eventWindow != null && !eventWindow.IsHidden && Globals.EventDialogs.Count > 0)
                            {
                                eventWindow.EventResponse1_Clicked(null, null);
                                canFocusChat = false;

                                break;
                            }
                        }
                        catch { }
                    }

                    break;
            }

            if (Controls.Controls.ControlHasKey(Control.OpenMenu, key))
            {
                if (Globals.GameState != GameStates.InGame)
                {
                    return;
                }

                // First try and unfocus chat then close all UI elements, then untarget our target.. and THEN open the escape menu.
                // Most games do this, why not this?
                if (Interface.Interface.GameUi != null && Interface.Interface.GameUi.ChatFocussed)
                {
                    Interface.Interface.GameUi.UnfocusChat = true;
                }
                else if (Interface.Interface.GameUi != null && Interface.Interface.GameUi.CloseAllWindows())
                {
                    // We've closed our windows, don't do anything else. :)
                }
                else if (Globals.Me != null && Globals.Me.TargetIndex != Guid.Empty)
                {
                    Globals.Me.ClearTarget();
                }
                else
                {
                    Interface.Interface.GameUi?.EscapeMenu?.ToggleHidden(); 
                }
            }

            if (Interface.Interface.HasInputFocus())
            {
                return;
            }

            Controls.Controls.GetControlsFor(key)
                ?.ForEach(
                    control =>
                    {
                        if (consumeKey)
                        {
                            return;
                        }

                        switch (control)
                        {
                            case Control.Screenshot:
                                Graphics.Renderer?.RequestScreenshot();

                                break;

                            case Control.ToggleGui:
                                if (Globals.GameState == GameStates.InGame)
                                {
                                    Interface.Interface.HideUi = !Interface.Interface.HideUi;
                                }

                                break;
                        }

                        switch (Globals.GameState)
                        {
                            case GameStates.Intro:
                                break;

                            case GameStates.Menu:
                                break;

                            case GameStates.InGame:
                                switch (control)
                                {
                                    case Control.MoveUp:
                                        break;

                                    case Control.MoveLeft:
                                        break;

                                    case Control.MoveDown:
                                        break;

                                    case Control.MoveRight:
                                        break;

                                    case Control.AttackInteract:
                                        break;

                                    case Control.Block:
                                        Globals.Me?.TryBlock();

                                        break;

                                    case Control.AutoTarget:
                                        Globals.Me?.AutoTarget();

                                        break;

                                    case Control.PickUp:
                                        Globals.Me?.TryPickupItem(Globals.Me.MapInstance.Id, Globals.Me.Y * Options.MapWidth + Globals.Me.X);

                                        break;

                                    case Control.Enter:
                                        if (canFocusChat)
                                        {
                                            Interface.Interface.GameUi.FocusChat = true;
                                            consumeKey = true;
                                        }

                                        return;

                                    case Control.Hotkey1:
                                    case Control.Hotkey2:
                                    case Control.Hotkey3:
                                    case Control.Hotkey4:
                                    case Control.Hotkey5:
                                    case Control.Hotkey6:
                                    case Control.Hotkey7:
                                    case Control.Hotkey8:
                                    case Control.Hotkey9:
                                    case Control.Hotkey0:
                                        break;

                                    case Control.OpenInventory:
                                        Interface.Interface.GameUi?.GameMenu?.ToggleInventoryWindow();

                                        break;

                                    case Control.OpenQuests:
                                        Interface.Interface.GameUi?.GameMenu?.ToggleQuestsWindow();

                                        break;

                                    case Control.OpenCharacterInfo:
                                        Interface.Interface.GameUi?.GameMenu?.ToggleCharacterWindow();

                                        break;

                                    case Control.OpenParties:
                                        Interface.Interface.GameUi?.GameMenu?.TogglePartyWindow();

                                        break;

                                    case Control.OpenSpells:
                                        Interface.Interface.GameUi?.GameMenu?.ToggleSpellsWindow();

                                        break;

                                    case Control.OpenFriends:
                                        Interface.Interface.GameUi?.GameMenu?.ToggleFriendsWindow();

                                        break;

                                    case Control.OpenSettings:
                                        Interface.Interface.GameUi?.EscapeMenu?.OpenSettings();

                                        break;

                                    case Control.OpenDebugger:
                                        Interface.Interface.GameUi?.ShowHideDebug();

                                        break;

                                    case Control.OpenAdminPanel:
                                        PacketSender.SendOpenAdminWindow();

                                        break;

                                    case Control.OpenGuild:
                                        Interface.Interface.GameUi?.GameMenu.ToggleGuildWindow();

                                        break;
                                }

                                break;

                            case GameStates.Loading:
                                break;

                            case GameStates.Error:
                                break;

                            default:
                                throw new ArgumentOutOfRangeException(
                                    nameof(Globals.GameState), Globals.GameState, null
                                );
                        }
                    }
                );
        }

        public static void OnKeyReleased(Keys key)
        {
            KeyUp?.Invoke(key);
            if (Interface.Interface.HasInputFocus())
            {
                return;
            }

            if (Globals.Me == null)
            {
                return;
            }

            if (Controls.Controls.ControlHasKey(Control.Block, key))
            {
                Globals.Me.StopBlocking();
            }
        }

        public static void OnMouseDown(GameInput.MouseButtons btn)
        {
            var key = Keys.None;
            switch (btn)
            {
                case GameInput.MouseButtons.Left:
                    key = Keys.LButton;
                //editado por rodrigo

                    if 
                        (Globals.GameState == GameStates.InGame && 
                        Globals.Me != null && 
                        Interface.Interface.HasInputFocus() == false && 
                        Interface.Interface.MouseHitGui() == false &&
                        Globals.Me.TryTarget() == false)
                    {


                        Pointf mouse;

                        var window_width = (int)Graphics.Renderer.ActiveResolution.X;
                        var window_height = (int)Graphics.Renderer.ActiveResolution.Y;
                        var window_center_x = (int)(window_width / 2);
                        var window_center_y = (int)(window_height / 2);

                        //get the mouse position
                        mouse = Globals.InputManager.GetMousePosition();


                        //lets find the real mouse coordinates inside the game window

                        var real_mouse_x = (int)(mouse.X);
                        var real_mouse_y = (int)(mouse.Y);

                        int mouse_x = (int)(mouse.X / Options.TileWidth);
                        int mouse_y = (int)(mouse.Y / Options.TileHeight);

                        int screen_width = Graphics.Renderer.GetScreenWidth();

                        int player_x = (int)(Globals.Me.X);
                        int player_y = (int)(Globals.Me.Y);

                        int steps_to_walk = player_x - mouse_x;

                        if (steps_to_walk < 0) { steps_to_walk = steps_to_walk * (-1); }

                        //do the math to find out the mouse most probably direction
                        int dif_x = real_mouse_x - window_center_x;
                        int dif_y = real_mouse_y - window_center_y;

                        //ok, now we convert to positive integer to check the biggest difference, horizontal or vertical so we can decide where to move
                        var chk_x = 0; var chk_y = 0; var direction = "";

                        if (dif_x < 0) 
                            { 
                                chk_x = (dif_x * (-1)); 
                            } else {
                                chk_x = dif_x;
                            }
                        if (dif_y < 0) 
                            { 
                                chk_y = (dif_y * (-1)); 
                            } else { 
                                chk_y = dif_y; 
                            }

                        //we clear any existing movement before setting a new one
                        //Globals.Me.multi_mouse_move_active = false;
                        Globals.Me.multi_mouse_move_count = -1;

                        if(chk_x > chk_y)
                        { //horizontal movement
                            if(real_mouse_x < window_center_x)
                            { // move left
                                direction = "left";
                                Globals.Me.multi_mouse_move_direction = 1;
                            } else
                            { // move right
                                direction = "right";
                                Globals.Me.multi_mouse_move_direction = 3;
                            }

                        } else
                        { //vertical movement
                            if (real_mouse_y < window_center_y)
                            { // move up
                                direction = "up";
                                Globals.Me.multi_mouse_move_direction = 0;
                            }
                            else
                            { // move down
                                direction = "down";
                                Globals.Me.multi_mouse_move_direction = 2;
                            }
                        }
                        Globals.Me.multi_mouse_move_count = steps_to_walk;


                        Globals.Me.IsMoving = true; 
                        Globals.Me.HandleInput(Globals.Me.multi_mouse_move_direction);
                        Globals.Me.multi_mouse_move_active = true;
                        

                        //The commented code below shows a chat bubble with the direction and "steps". For testing purposes.
                        //PacketSender.SendChatMsg("going " + direction + " " + Globals.Me.multi_mouse_move_count.ToString() + " steps." , 0);
                        //PacketSender.SendChatMsg($"player:  {player_x},{player_y}  mouse: {mouse_x}, {mouse_y} real mouse: {real_mouse_x} pl-px: {real_pl_x} " , 0);
                    }
                    //fim do editado por rodrigo

                    break;

                case GameInput.MouseButtons.Right:
                    key = Keys.RButton;

                    break;

                case GameInput.MouseButtons.Middle:
                    key = Keys.MButton;

                    break;
            }

            MouseDown?.Invoke(key);
            if (Interface.Interface.HasInputFocus())
            {
                return;
            }

            if (Globals.GameState != GameStates.InGame || Globals.Me == null)
            {
                return;
            }

            if (Interface.Interface.MouseHitGui())
            {
                return;
            }

            if (Globals.Me == null)
            {
                return;
            }

            if (Globals.Me.TryTarget())
            {
                return;
            }

            if (Controls.Controls.ControlHasKey(Control.PickUp, key))
            {
                if (Globals.Me.TryPickupItem(Globals.Me.MapInstance.Id, Globals.Me.Y * Options.MapWidth + Globals.Me.X, Guid.Empty, true))
                {
                    return;
                }

                if (Globals.Me.AttackTimer < Timing.Global.Ticks / TimeSpan.TicksPerMillisecond)
                {
                    Globals.Me.AttackTimer = Timing.Global.Ticks / TimeSpan.TicksPerMillisecond + Globals.Me.CalculateAttackTime();
                }
            }

            if (Controls.Controls.ControlHasKey(Control.Block, key))
            {
                if (Globals.Me.TryBlock())
                {
                    return;
                }
            }

            if (key != Keys.None)
            {
                OnKeyPressed(key);
            }
        }

        public static void OnMouseUp(GameInput.MouseButtons btn)
        {
            var key = Keys.LButton;
            switch (btn)
            {
                case GameInput.MouseButtons.Right:
                    key = Keys.RButton;
                    if(Globals.Me.IsMoving == false) 
                    {
                        PacketSender.SendDirection((byte)Globals.Me.Dir);
                        Globals.Me.start_hotbar_selected_spell = true;                    
                    }

                    break;

                case GameInput.MouseButtons.Middle:
                    key = Keys.MButton;
                    break;
            }

            MouseUp?.Invoke(key);
            if (Interface.Interface.HasInputFocus())
            {
                return;
            }

            if (Globals.Me == null)
            {
                return;
            }

            if (Controls.Controls.ControlHasKey(Control.Block, key))
            {
                Globals.Me.StopBlocking();
            }

            if (btn != GameInput.MouseButtons.Right)
            {
                return;
            }

            if (Globals.InputManager.KeyDown(Keys.Shift) != true)
            {
                return;
            }

            var x = (int) Math.Floor(Globals.InputManager.GetMousePosition().X + Graphics.CurrentView.Left);
            var y = (int) Math.Floor(Globals.InputManager.GetMousePosition().Y + Graphics.CurrentView.Top);

            foreach (MapInstance map in MapInstance.Lookup.Values)
            {
                if (!(x >= map.GetX()) || !(x <= map.GetX() + Options.MapWidth * Options.TileWidth))
                {
                    continue;
                }

                if (!(y >= map.GetY()) || !(y <= map.GetY() + Options.MapHeight * Options.TileHeight))
                {
                    continue;
                }

                //Remove the offsets to just be dealing with pixels within the map selected
                x -= (int) map.GetX();
                y -= (int) map.GetY();

                //transform pixel format to tile format
                x /= Options.TileWidth;
                y /= Options.TileHeight;
                var mapNum = map.Id;

                if (Globals.Me.GetRealLocation(ref x, ref y, ref mapNum))
                {
                    PacketSender.SendAdminAction(new WarpToLocationAction(map.Id, (byte) x, (byte) y));
                }

                return;
            }
        }

    }

}
