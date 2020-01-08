﻿using Newtonsoft.Json;
using Sidekick.Helpers.POENinjaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sidekick.Helpers.POENinjaAPI
{
    public class NinjaClient
    {

        public readonly static Uri POE_NINJA_ITEMOVERVIEW_BASE_URL = new Uri("https://poe.ninja/api/data/itemoverview");
        public readonly static Uri POE_NINJA_CURRENCYOVERVIEW_BASE_URL = new Uri("https://poe.ninja/api/data/currencyoverview");
        private readonly HttpClient _client;

        public NinjaClient()
        {
            _client = new HttpClient();
        }

        public async Task<NinjaQueryResult<NinjaItem>> GetItemOverview(string league, ItemType itemType)
        {

            var url = $"{POE_NINJA_ITEMOVERVIEW_BASE_URL}?league={league}&type={itemType}";

            var responseString = await PerformRequestAndValidateResponse(url);

            return JsonConvert.DeserializeObject<NinjaQueryResult<NinjaItem>>(responseString);
        }

        public async Task<NinjaQueryResult<NinjaCurrency>> GetCurrencyOverview(string league, CurrencyType currency)
        {

            var url = $"{POE_NINJA_CURRENCYOVERVIEW_BASE_URL}?league={league}&type={currency}";

            var responseString = await PerformRequestAndValidateResponse(url);

            return JsonConvert.DeserializeObject<NinjaQueryResult<NinjaCurrency>>(responseString);
        }

        private async Task<string> PerformRequestAndValidateResponse(string url)
        {
            var response = await _client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"poe.ninja returned an error for {url}: [{response.StatusCode}] {responseString}");
            }

            if (String.IsNullOrEmpty(responseString))
            {
                throw new Exception("poe.ninja returned an empty result. Check the provided league.");
            }

            return responseString;
        }
    }
}
