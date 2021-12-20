﻿#region License
/*
   Copyright 2021-2022 Hawxy

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */
#endregion

namespace Fga.Net;

internal static class FgaConstants
{
    public static readonly string AuthenticationUrl = "https://fga.us.auth0.com/";
    public static readonly string AuthorizationUrl = "https://api.{0}.fga.dev/";
    public static readonly string Audience = "https://api.{0}.fga.dev/";
}
