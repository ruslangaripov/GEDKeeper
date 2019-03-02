﻿/*
 *  "GEDKeeper", the personal genealogical database editor.
 *  Copyright (C) 2009-2019 by Sergey V. Zhdanovskih.
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

namespace GKCommon.GEDCOM
{
    public sealed class GEDCOMDateInterpreted : GEDCOMDate
    {
        private string fDatePhrase;

        public string DatePhrase
        {
            get {
                return fDatePhrase;
            }
            set {
                string phrase = value;

                if (!string.IsNullOrEmpty(phrase)) {
                    if (phrase[0] == '(') {
                        phrase = phrase.Remove(0, 1);
                    }

                    if (phrase.Length > 0 && phrase[phrase.Length - 1] == ')') {
                        phrase = phrase.Remove(phrase.Length - 1, 1);
                    }
                }

                fDatePhrase = phrase;
            }
        }

        protected override void CreateObj(GEDCOMTree owner, GEDCOMObject parent)
        {
            base.CreateObj(owner, parent);
            fDatePhrase = "";
        }

        protected override string GetStringValue()
        {
            return ("INT " + base.GetStringValue() + " " + "(" + fDatePhrase + ")");
        }

        // TODO: refactor it
        private string ExtractPhrase(string str)
        {
            string result = str;
            if (result.Length >= 2 && result[0] == '(')
            {
                result = result.Remove(0, 1);

                int c = 0;
                int num = result.Length;
                for (int I = 1; I <= num; I++)
                {
                    if (result[I - 1] == '(')
                    {
                        c++;
                    }
                    else
                    {
                        if (result[I - 1] == ')' || I == result.Length)
                        {
                            c--;
                            if (c <= 0 || I == result.Length)
                            {
                                if (result[I - 1] == ')')
                                {
                                    fDatePhrase = result.Substring(0, I - 1);
                                }
                                else
                                {
                                    fDatePhrase = result.Substring(0, I);
                                }
                                result = result.Remove(0, I);
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

        // Format: INT DATE (phrase)
        public override string ParseString(string strValue)
        {
            string result = strValue;
            if (!string.IsNullOrEmpty(result)) {
                result = GEDCOMUtils.ExtractDelimiter(result);
                if (!GEDCOMUtils.ExtractExpectedIdent(result, "INT", out result, true)) {
                    throw new GEDCOMDateException(string.Format("The interpreted date '{0}' doesn't start with a valid ident", strValue));
                }
                result = GEDCOMUtils.ExtractDelimiter(result);
                result = base.ParseString(result);
                result = GEDCOMUtils.ExtractDelimiter(result);
                result = ExtractPhrase(result);
            }
            return result;
        }

        public GEDCOMDateInterpreted(GEDCOMTree owner, GEDCOMObject parent, string tagName, string tagValue) : base(owner, parent, tagName, tagValue)
        {
        }

        public new static GEDCOMTag Create(GEDCOMTree owner, GEDCOMObject parent, string tagName, string tagValue)
        {
            return new GEDCOMDateInterpreted(owner, parent, tagName, tagValue);
        }
    }
}
