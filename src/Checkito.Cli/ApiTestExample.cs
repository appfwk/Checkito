using Checkito.Apis.Models;
using Checkito.Assertions.Models;
using Checkito.Testing.Models;

namespace Checkito.Cli
{
    public class ApiTestExample
    {
        public void Test()
        {
            var testCase = new TestCase
            {
                Name = "GetUserListTest",
                Steps = new List<TestCaseStep>
                {
                    new TestCaseStep
                    {
                        Name = "Test",
                        ApiCall = new ApiCallDefinition
                        {
                            Name = "GetUsers",
                            HttpMethod = HttpVerb.Get,
                            Uri = "api/users",
                            Headers = new Dictionary<string, string>
                            {
                                ["Content-Type"] = "application/json"
                            }
                        },
                        Asserts = new List<AssertDefinition>
                        {
                            new AssertDefinition
                            {
                                Source = AssertSourceCodes.HttpStatus,
                                //ExpectedValue = 200
                            },
                            new AssertDefinition
                            {
                                Source = AssertSourceCodes.Json,
                                ExpectedValue = "{\"page\":1,\"per_page\":6,\"total\":12,\"total_pages\":2,\"data\":[{\"id\":1,\"email\":\"george.bluth@reqres.in\",\"first_name\":\"George\",\"last_name\":\"Bluth\",\"avatar\":\"https://reqres.in/img/faces/1-image.jpg\"},{\"id\":2,\"email\":\"janet.weaver@reqres.in\",\"first_name\":\"Janet\",\"last_name\":\"Weaver\",\"avatar\":\"https://reqres.in/img/faces/2-image.jpg\"},{\"id\":3,\"email\":\"emma.wong@reqres.in\",\"first_name\":\"Emma\",\"last_name\":\"Wong\",\"avatar\":\"https://reqres.in/img/faces/3-image.jpg\"},{\"id\":4,\"email\":\"eve.holt@reqres.in\",\"first_name\":\"Eve\",\"last_name\":\"Holt\",\"avatar\":\"https://reqres.in/img/faces/4-image.jpg\"},{\"id\":5,\"email\":\"charles.morris@reqres.in\",\"first_name\":\"Charles\",\"last_name\":\"Morris\",\"avatar\":\"https://reqres.in/img/faces/5-image.jpg\"},{\"id\":6,\"email\":\"tracey.ramos@reqres.in\",\"first_name\":\"Tracey\",\"last_name\":\"Ramos\",\"avatar\":\"https://reqres.in/img/faces/6-image.jpg\"}],\"support\":{\"url\":\"https://reqres.in/#support-heading\",\"text\":\"TokeepReqResfree,contributionstowardsservercostsareappreciated!\"}}"
                            }
                        }
                    }
                }
            };

            //new TestRunner().Run(testCase).GetAwaiter().GetResult();
        }
    }
}
