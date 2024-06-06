/*
 * Switcheroo - The incremental-search task switcher for Windows.
 * http://www.switcheroo.io/
 * Copyright 2009, 2010 James Sulak
 * Copyright 2014 Regin Larsen
 * 
 * Switcheroo is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Switcheroo is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Switcheroo.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using ManagedWinapi.Windows;

namespace Switcheroo.Core
{
    public class WindowFinder
    {
        public List<AppWindow> GetWindows(bool CrrentScreenOnly, bool CurrentApplicationTypeOnly, SystemWindow foregroundWindow)
        {
            var foregroundProcessName = foregroundWindow.Process.ProcessName;

            return AppWindow.AllToplevelWindows
                .Where(a => IsCurrentApplicationType(CrrentScreenOnly, CurrentApplicationTypeOnly, foregroundProcessName, a))
                .ToList();
        }

        private static bool IsCurrentApplicationType(bool CrrentScreenOnly, bool CurrentApplicationTypeOnly,
            string foregroundProcessName, AppWindow a)
        {
            var isAltTabWindow = a.IsAltTabWindow();
            if (!isAltTabWindow)
            {
                return false;
            }
            var isCurrentScreenWindow = CrrentScreenOnly ? a.IsCurrentScreenWindow() : true;
            if (!isCurrentScreenWindow)
            {
                return false;
            }
            var isCurrentApplicationType = CurrentApplicationTypeOnly ? a.ProcessTitle == foregroundProcessName : true;
            if (!isCurrentApplicationType)
            {
                return false;
            }
                 
            return isAltTabWindow && isCurrentScreenWindow && isCurrentApplicationType;
        }
    }
}