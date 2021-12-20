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

using System.Net.Http.Json;

namespace Fga.Net.Authentication;

/// <inheritdoc />
public class FgaAuthenticationClient : IFgaAuthenticationClient
{
    private readonly HttpClient _httpClient;
    private readonly bool _isInternalHttpClient;


    public FgaAuthenticationClient(HttpClient client)
    {
        _httpClient = client;
    }

    private FgaAuthenticationClient(HttpClient client, bool isInternalHttp)
    {
        _httpClient = client;
        _isInternalHttpClient = isInternalHttp;
    }

    public static FgaAuthenticationClient Create(HttpClient? client = null)
    {
        var httpClient = client ?? new HttpClient();
        httpClient.BaseAddress = new Uri(FgaConstants.AuthenticationUrl);
        return new FgaAuthenticationClient(httpClient, client is null);
    }
    /// <inheritdoc />
    public async Task<AccessTokenResponse?> FetchTokenAsync(AccessTokenRequest request)
    {
        var dict = new Dictionary<string, string>
        {
            { "grant_type", request.GrantType },
            { "client_id", request.ClientId },
            { "client_secret", request.ClientSecret },
            { "audience", string.Format(FgaConstants.Audience, request.Environment) }
        };
        var content = new FormUrlEncodedContent(dict);

        var res = await _httpClient.PostAsync("oauth/token", content);

        return await res.Content.ReadFromJsonAsync<AccessTokenResponse>();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if(_isInternalHttpClient)
            _httpClient.Dispose();
    }
}