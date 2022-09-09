using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using UnitedNavigationTask.MVC.Dtos;

namespace UnitedNavigationTask.MVC.Handlers
{
    public class ApiHandler
    {
        private IEnumerable<CsvDto> csvDtos;
        private readonly HttpClient httpClient;

        public ApiHandler(IEnumerable<CsvDto> csvDtos, HttpClient httpClient, string baseUrl)
        {
            this.csvDtos = csvDtos;
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri(baseUrl);
        }

        internal async Task<string> GetJWTToken(string endpoint)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{httpClient.BaseAddress}{endpoint}");
            string content = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
            }
            return content;
        }

        internal async Task<IEnumerable<string>> PlaceOrderPost(string token, string endpoint)
        {
            // This method attempts to take a list of selected rows whether the orders ids match or not and based on the order IDs split them
            // and then send a request foreach orderID instead of the number of rows. However there seems to be a bug somewhere which is not 
            // making the correct number of requests. However it can be simplified to exactly what the document was asking for by simply only selecting
            // rows which do have matching order ids and then just create a list of purchaseOrderItems and pass that to the apiDto without any for/foreach loops
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var records = csvDtos.ToList();
            var apiDtos = new List<ApiDto>();
            var purchaseOrderItems = new List<Purchaseorderitem>();
            var orderNoChecker = string.Empty;
            // I want to make sure Im sending one request for mutiple orders with the same order id.

            for (int i = 0; i < records.Count(); i++)
            {
                var purchaseOrderItem = new Purchaseorderitem()
                {
                    stockCode = records[i].ParcelCode,
                    description = records[i].ItemDesciption,
                    orderQty = int.Parse(records[i].ItemQuantity),
                    unitPrice = float.Parse(records[i].ItemValue)
                };
                purchaseOrderItems.Add(purchaseOrderItem);
            }

            var responseIds = new List<string>();
            var distinctRecords = csvDtos.DistinctBy(m => m.OrderNumber);
            foreach(CsvDto csvDto in distinctRecords)
            {
                var groupedOrders = new List<Purchaseorderitem>();
                for(int i = 0; i <= distinctRecords.Count(); i++)
                {
                    if (i == 0)
                    {
                        groupedOrders.Add(purchaseOrderItems[i]);
                        continue;
                    }
                    if (records[i].OrderNumber != records[i - 1].OrderNumber)
                        break;
                    else
                        groupedOrders.Add(purchaseOrderItems[i]);
                }
                var apiDto = new ApiDto()
                {
                    orderNumber = csvDto.OrderNumber,
                    accountRef = csvDto.ConsignmentNumber,
                    address1 = csvDto.AddressOne,
                    address2 = csvDto.AddressTwo,
                    address4 = csvDto.City,
                    address5 = csvDto.CountryCode,
                    contactName = csvDto.ConsigneeName,
                    purchaseOrderItems = groupedOrders.ToArray()
                };

                apiDtos.Add(apiDto);
                // Serialize our concrete class into a JSON String
                var stringPayload = JsonConvert.SerializeObject(apiDto);

                // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync($"{httpClient.BaseAddress}{endpoint}", httpContent);
                // If the response contains content we want to read it!
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    responseIds.Add(responseContent);
                    Console.WriteLine(responseContent);
                }
            }
            return responseIds;
        }
    }
}
