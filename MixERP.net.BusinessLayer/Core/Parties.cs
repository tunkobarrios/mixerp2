﻿/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MixERP.Net.BusinessLayer.Core
{
    public static class Parties
    {
        public static bool IsCreditAllowed(string partyCode)
        {
            return MixERP.Net.DatabaseLayer.Core.Parties.IsCreditAllowed(partyCode);
        }

        public static string GetDisplayField()
        {
            string displayField = Pes.Utility.Helpers.ConfigurationHelper.GetSectionKey("MixERPParameters", "PartyDisplayField");
            if(string.IsNullOrWhiteSpace(displayField))
            {
                displayField = "party_name";
            }

            return displayField;
        }
    }
}
