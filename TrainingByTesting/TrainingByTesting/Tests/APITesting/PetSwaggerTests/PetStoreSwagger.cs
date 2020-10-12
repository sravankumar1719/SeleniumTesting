using System.Collections.Generic;
namespace TrainingByTesting.Tests.APITesting.PetSwaggerTests
{
    public class PetStoreSwagger
    {
        public int id { get; set; }

        public string name { get; set; }

        public string status { get; set; }

        public Category category = new Category();

        public List<string> photoUrls = new List<string>();

        public List<PetStoreTags> tags = new List<PetStoreTags>();
    }
}
