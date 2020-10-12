using NUnit.Framework;
using RestSharp;
using System.Linq;

namespace TrainingByTesting.Tests.APITesting.PetSwaggerTests
{
    [TestFixture]
    public class VerifyingPetSwaggerDataTests
    {
        [Test]
        public void VerifyPetDetailsTest()
        {
            RestClient restClient = new RestClient("https://petstore.swagger.io/v2/pet/");
            RestRequest restRequest = new RestRequest("112233", Method.GET);
            IRestResponse restResponse = restClient.Execute(restRequest);

            int statusCode = (int)restResponse.StatusCode;

            Assert.AreEqual(200, statusCode, "Failed to get the details for the pet");

            PetStoreSwagger petDetails = SimpleJson.DeserializeObject<PetStoreSwagger>(restResponse.Content);

            Assert.AreEqual("Postman", petDetails.name, "Pet name is not matching");

            Assert.AreEqual("available", petDetails.status, "Status of the pet isn't matching");

            Assert.AreEqual(30, petDetails.category.id, "Category Id of the pet isn't matching");

            Assert.AreEqual("Eq-dog", petDetails.category.name, "Category name of the pet isn't matching");

            Assert.AreEqual(10, petDetails.tags.Select(tag => tag.id).First(), "Tag Id of the pet isn't matching");

            Assert.AreEqual("Doberman", petDetails.tags.Select(tag => tag.name).First(), "Tag name of the pet isn't matching");
        }

        [Test]
        public void PostingPetDetailsTest()
        {
            RestClient restClient = new RestClient("https://petstore.swagger.io/v2/pet/");
            RestRequest restRequest = new RestRequest("", Method.POST);
            PetStoreSwagger petDetails = new PetStoreSwagger();

            petDetails.id = 99;
            petDetails.name = "Kitty";
            petDetails.category.id = 2;
            petDetails.category.name = "Cat";
            petDetails.status = "Pending";
            petDetails.tags.Add(new PetStoreTags { id = 2, name = "Kitty" });
            petDetails.photoUrls.Add("Kitty.jpg");

            restRequest.AddJsonBody(petDetails);
            restRequest.RequestFormat = DataFormat.Json;

            restRequest.AddParameter("application/json", petDetails, ParameterType.RequestBody);

            IRestResponse restResponse = restClient.Execute(restRequest);

            int statusCode = (int)restResponse.StatusCode;

            Assert.AreEqual(200, statusCode, "Failed to post the details for the pet");
        }
    }
}
