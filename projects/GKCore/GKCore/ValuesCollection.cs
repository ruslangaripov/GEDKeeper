﻿/*
 *  "GEDKeeper", the personal genealogical database editor.
 *  Copyright (C) 2009-2017 by Sergey V. Zhdanovskih.
 *
 *  This file is part of "GEDKeeper".
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Collections.Generic;

namespace GKCore
{
    public sealed class ValuesCollection
    {
        private readonly Dictionary<string, List<string>> fValues;

        public int Count
        {
            get { return fValues.Count; }
        }

        public ValuesCollection()
        {
            fValues = new Dictionary<string, List<string>>();
        }

        public void Clear()
        {
            fValues.Clear();
        }

        public void Add(string name, string value, bool excludeDuplicates = false)
        {
            List<string> arrayList;
            if (!fValues.TryGetValue(name, out arrayList)) {
                arrayList = new List<string>(1);
                fValues.Add(name, arrayList);
            }

            if (value == null) return;

            if (!excludeDuplicates) {
                arrayList.Add(value);
            } else {
                if (!arrayList.Contains(value)) arrayList.Add(value);
            }
        }

        public void Remove(string name)
        {
            fValues.Remove(name);
        }

        public string[] GetValues(string name)
        {
            List<string> list;
            if (!fValues.TryGetValue(name, out list)) {
                return null;
            }

            int num = list.Count;
            string[] array = new string[num];
            list.CopyTo(0, array, 0, num);

            return array;
        }
    }
}
