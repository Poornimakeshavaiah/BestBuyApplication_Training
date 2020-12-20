using BestBuyTests.Model;
using BestBuyTests.Model.ForSampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace BestBuyTests
{
    [TestClass]
    public class BestBuyAPITest
    {
        private string endpointUrl = "http://ec2-3-129-89-35.us-east-2.compute.amazonaws.com:3030/";

        //Creating a rest client
        IRestClient restClient;

        //Creating a rest request
        IRestRequest restRequest;

        [TestInitialize]
        public void Setup()
        {
            restClient = new RestClient();


        }

        [TestMethod]
        public void VerifyGetProductAPI()
        {

            restRequest = new RestRequest(endpointUrl + "/products");

            IRestResponse restResponse = restClient.Get(restRequest);

            Console.WriteLine(restResponse.Content);

            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode);

            Assert.AreEqual(200, (int)restResponse.StatusCode);
        }

        [TestMethod]
        public void VerifyGetProductAPIWithQueryParam()
        {
            restRequest = new RestRequest(endpointUrl + "/products");

            restRequest.AddQueryParameter("$limit", "5");

            IRestResponse restResponse = restClient.Get(restRequest);

            Console.WriteLine(restResponse.Content);

            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode);

            Assert.AreEqual(200, (int)restResponse.StatusCode);
        }

        [TestMethod]
        public void VerifyGetProductAPIAndDeserializeResponse()
        {
            restRequest = new RestRequest(endpointUrl + "/products");

            restRequest.AddQueryParameter("$limit", "5");

            IRestResponse<RootProductDTO> restResponse = restClient.Get<RootProductDTO>(restRequest);

            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode);

            Assert.AreEqual(200, (int)restResponse.StatusCode);

            var responseData = restResponse.Data;

            Assert.AreEqual(5, responseData.limit);

            Console.WriteLine(responseData.data[0].id);

            List<DatumDTO> data = responseData.data;

            foreach (var item in data)
            {
                Console.WriteLine(item.id);
            }

            Console.WriteLine(responseData.data[0].categories[0].id);
        }

        [TestMethod]
        public void VerifyPostProductAPI()
        {
            restRequest = new RestRequest(endpointUrl + "/products");

            string requestPayload = "{\r\n  \"name\": \"Samsung Mobile\",\r\n  \"type\": \"Mobile\",\r\n  \"price\": 500,\r\n  \"shipping\": 10,\r\n  \"upc\": \"Mobile\",\r\n  \"description\": \"Best Mobile in the town\",\r\n  \"manufacturer\": \"Samsung\",\r\n  \"model\": \"M31\",\r\n  \"url\": \"string\",\r\n  \"image\": \"string\"\r\n}";

            restRequest.AddJsonBody(requestPayload);

            IRestResponse restResponse = restClient.Post(restRequest);

            Assert.AreEqual(HttpStatusCode.Created, restResponse.StatusCode);

            Assert.AreEqual(201, (int)restResponse.StatusCode);


        }

        [TestMethod]
        public void VerifyPostProductAPIWithFileObject()
        {
            restRequest = new RestRequest(endpointUrl + "/products");

            //   string requestPayload = "{\r\n  \"name\": \"Samsung Mobile\",\r\n  \"type\": \"Mobile\",\r\n  \"price\": 500,\r\n  \"shipping\": 10,\r\n  \"upc\": \"Mobile\",\r\n  \"description\": \"Best Mobile in the town\",\r\n  \"manufacturer\": \"Samsung\",\r\n  \"model\": \"M31\",\r\n  \"url\": \"string\",\r\n  \"image\": \"string\"\r\n}";

            string requestPayload = File.ReadAllText("C:/Users/Dhanu Savi/Desktop/Poornima_Training/BestBuyApplication/BestBuyTests/product.json");

            restRequest.AddJsonBody(requestPayload);

            IRestResponse restResponse = restClient.Post(restRequest);

            Assert.AreEqual(HttpStatusCode.Created, restResponse.StatusCode);

            Assert.AreEqual(201, (int)restResponse.StatusCode);


        }

        [TestMethod]
        public void VerifyPutProductAPI()
        {
            //POST Request to create a new Product
            restRequest = new RestRequest(endpointUrl + "/products");

            Dictionary<string, object> requestPayload = new Dictionary<string, object>();

            requestPayload.Add("name", "Samsung Mobile");
            requestPayload.Add("type", "Mobile");
            requestPayload.Add("price", 500);
            requestPayload.Add("shipping", 10);
            requestPayload.Add("upc", "Samsung Mobile");
            requestPayload.Add("description", "Best Samsung Mobile");
            requestPayload.Add("manufacturer", "Samsung Mobile");
            requestPayload.Add("model", "Samsung Mobile M21");
            requestPayload.Add("url", "Samsung Mobile");
            requestPayload.Add("image", "Samsung Mobile");

            restRequest.AddJsonBody(requestPayload);

            IRestResponse<DatumDTO> restResponse = restClient.Post<DatumDTO>(restRequest);


            int productId = restResponse.Data.id;


            //PUT
            IRestRequest restRequestForEdit = new RestRequest($"{endpointUrl}/products/{productId}");

            ProductDTO requestPayloadUpdated = new ProductDTO();

            requestPayloadUpdated.name = "Motorola";
            requestPayloadUpdated.type = "Mobile";
            requestPayloadUpdated.price = 1000;
            requestPayloadUpdated.shipping = 10;
            requestPayloadUpdated.upc = "2asj";
            requestPayloadUpdated.description = "Motorola New Model";
            requestPayloadUpdated.manufacturer = "Lenovo";
            requestPayloadUpdated.model = "Moto razor 12";
            requestPayloadUpdated.url = "rweuru";
            requestPayloadUpdated.image = "sdfsadfasd";

            restRequestForEdit.AddJsonBody(requestPayloadUpdated);

            IRestResponse restResponseForUpdate = restClient.Put(restRequestForEdit);

            Assert.AreEqual(HttpStatusCode.OK, restResponseForUpdate.StatusCode);

            Assert.AreEqual(200, (int)restResponseForUpdate.StatusCode);

            //GET

        }

        [TestMethod]
        public void VerifyPatchProductAPI()
        {
            //POST Request to create a new Product
            restRequest = new RestRequest(endpointUrl + "/products");

            Dictionary<string, object> requestPayload = new Dictionary<string, object>();

            requestPayload.Add("name", "Samsung Mobile");
            requestPayload.Add("type", "Mobile");
            requestPayload.Add("price", 500);
            requestPayload.Add("shipping", 10);
            requestPayload.Add("upc", "Samsung Mobile");
            requestPayload.Add("description", "Best Samsung Mobile");
            requestPayload.Add("manufacturer", "Samsung Mobile");
            requestPayload.Add("model", "Samsung Mobile M21");
            requestPayload.Add("url", "Samsung Mobile");
            requestPayload.Add("image", "Samsung Mobile");

            restRequest.AddJsonBody(requestPayload);

            IRestResponse<DatumDTO> restResponse = restClient.Post<DatumDTO>(restRequest);


            int productId = restResponse.Data.id;


            //PATCH
            IRestRequest restRequestForEdit = new RestRequest($"{endpointUrl}/products/{productId}");

            ProductDTO requestPayloadUpdated = new ProductDTO();

            requestPayloadUpdated.name = "Motorola";

            requestPayloadUpdated.price = 500;


            restRequestForEdit.AddJsonBody(requestPayloadUpdated);

            IRestResponse restResponseForUpdate = restClient.Patch(restRequestForEdit);

            Assert.AreEqual(HttpStatusCode.OK, restResponseForUpdate.StatusCode);

            Assert.AreEqual(200, (int)restResponseForUpdate.StatusCode);

            //GET

        }


        [TestMethod]
        public void VerifyDeleteProductAPI()
        {
            //POST Request to create a new Product
            restRequest = new RestRequest(endpointUrl + "/products");

            Dictionary<string, object> requestPayload = new Dictionary<string, object>();

            requestPayload.Add("name", "Samsung Mobile");
            requestPayload.Add("type", "Mobile");
            requestPayload.Add("price", 500);
            requestPayload.Add("shipping", 10);
            requestPayload.Add("upc", "Samsung Mobile");
            requestPayload.Add("description", "Best Samsung Mobile");
            requestPayload.Add("manufacturer", "Samsung Mobile");
            requestPayload.Add("model", "Samsung Mobile M21");
            requestPayload.Add("url", "Samsung Mobile");
            requestPayload.Add("image", "Samsung Mobile");

            restRequest.AddJsonBody(requestPayload);

            IRestResponse<DatumDTO> restResponse = restClient.Post<DatumDTO>(restRequest);


            int productId = restResponse.Data.id;


            //DELETE
            IRestRequest restRequestForDelete = new RestRequest($"{endpointUrl}/products/{productId}");

            IRestResponse restResponseForDelete = restClient.Delete(restRequestForDelete);

            Assert.AreEqual(HttpStatusCode.OK, restResponseForDelete.StatusCode);

            Assert.AreEqual(200, (int)restResponseForDelete.StatusCode);

            //GET

            IRestRequest restRequestForGet = new RestRequest($"{endpointUrl}/products/{productId}");

            IRestResponse restResponseForGet = restClient.Get(restRequestForGet);

            Assert.AreEqual(HttpStatusCode.NotFound, restResponseForGet.StatusCode);

        }


        [TestMethod]
        public void VerifyPostProductAPIWithDictionaryAsRequestPayload()
        {
            restRequest = new RestRequest(endpointUrl + "/products");

            Dictionary<string, object> requestPayload = new Dictionary<string, object>();

            requestPayload.Add("name", "Samsung Mobile");
            requestPayload.Add("type", "Mobile");
            requestPayload.Add("price", 500);
            requestPayload.Add("shipping", 10);
            requestPayload.Add("upc", "Samsung Mobile");
            requestPayload.Add("description", "Best Samsung Mobile");
            requestPayload.Add("manufacturer", "Samsung Mobile");
            requestPayload.Add("model", "Samsung Mobile M21");
            requestPayload.Add("url", "Samsung Mobile");
            requestPayload.Add("image", "Samsung Mobile");

            restRequest.AddJsonBody(requestPayload);

            IRestResponse restResponse = restClient.Post(restRequest);

            Assert.AreEqual(HttpStatusCode.Created, restResponse.StatusCode);

            Assert.AreEqual(201, (int)restResponse.StatusCode);


        }


        [TestMethod]
        public void VerifyPostProductAPIWithObjectAsRequestPayload()
        {
            restRequest = new RestRequest(endpointUrl + "/products");

            ProductDTO requestPayload = new ProductDTO();

            requestPayload.name = "IPhone";
            requestPayload.type = "Mobile";
            requestPayload.price = 1000;
            requestPayload.shipping = 10;
            requestPayload.upc = "2asj";
            requestPayload.description = "Iphone New Model";
            requestPayload.manufacturer = "Apple";
            requestPayload.model = "iPhone 12";
            requestPayload.url = "rweuru";
            requestPayload.image = "sdfsadfasd";

            restRequest.AddJsonBody(requestPayload);

            IRestResponse restResponse = restClient.Post(restRequest);

            Assert.AreEqual(HttpStatusCode.Created, restResponse.StatusCode);

            Assert.AreEqual(201, (int)restResponse.StatusCode);


        }

        [TestMethod]
        public void SampleData()
        {

            Dictionary<string, object> requestPayload = new Dictionary<string, object>();

            Dictionary<string, object> address = new Dictionary<string, object>();
            Dictionary<string, object> address2 = new Dictionary<string, object>();

            requestPayload.Add("firstName", "Poornima");
            requestPayload.Add("lastName", "Keshavaiah");
            requestPayload.Add("employeeId", 123456);

            List<long> phoneNumbers = new List<long>();

            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                phoneNumbers.Add(random.Next(456456456));

            }

            phoneNumbers.Add(3322116655);
            phoneNumbers.Add(3322116655);

            requestPayload.Add("phoneNumber", phoneNumbers);

            address.Add("type", "home");
            address.Add("houseNumber", 9988);
            address.Add("city", "Bangalore");

            address2.Add("type", "office");
            address2.Add("houseNumber", 2255);
            address2.Add("city", "Delhi");

            List<Dictionary<string, object>> allAddress = new List<Dictionary<string, object>>();

            allAddress.Add(address);
            allAddress.Add(address2);

            requestPayload.Add("address", allAddress);

            restRequest = new RestRequest(endpointUrl + "/products");

            restRequest.AddJsonBody(requestPayload);

            IRestResponse restResponse = restClient.Post(restRequest);
        }

        [TestMethod]
        public void SampleData2()
        {
            RootDetails rootDetails = new RootDetails();

            Address address = new Address();
            Address address2 = new Address();

            rootDetails.firstName = "Poornima";
            rootDetails.lastName = "Keshavaiah";
            rootDetails.employeeId = 123456;

            List<long> phoneNumbers = new List<long>();
            phoneNumbers.Add(9876543210);
            phoneNumbers.Add(0123456789);
            phoneNumbers.Add(7531598426);

            rootDetails.phoneNumber = phoneNumbers;

            List<Address> addresses = new List<Address>();

            address.type = "home";
            address.city = "Bangalore";
            address.houseNumber = 9988;

            address2.type = "Office";
            address2.city = "Delhi";
            address2.houseNumber = 2255;

            addresses.Add(address);
            addresses.Add(address2);

            rootDetails.address = addresses;

        }

        [TestCleanup]
        public void Cleanup()
        {
            Console.WriteLine("Runs after every test method");
        }
    }
}
