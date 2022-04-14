using Checkito.Assertions.Models;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace Checkito.Assertions.Services
{
    public class JsonComparer
    {
        public bool Compare(string? expectedValue, string? value)
        {

            if (string.Equals(expectedValue, value))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(expectedValue) || string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var expectedJson = (JsonObject?)JsonNode.Parse(expectedValue);
            var json = (JsonObject?)JsonNode.Parse(value);

            if (expectedJson == null || json == null)
            {
                return false;
            }

            return PopulateJson(expectedJson, json);


            foreach (var expectedAttribute in expectedJson)
            {
                if (!json.TryGetPropertyValue(expectedAttribute.Key, out var attributeValue))
                {
                    return false;
                }

                if (expectedAttribute.Value is JsonValue)
                {
                    if (!string.Equals(expectedAttribute.Value!.ToJsonString(), attributeValue!.ToJsonString()))
                    {
                        return false;
                    }
                }
                else if (expectedAttribute.Value is JsonArray expectedJsonArray)
                {

                }
                else if (expectedAttribute.Value is JsonObject expectedJsonObject)
                {

                }
            }

            return false;
        }

        private bool PopulateJson(JsonObject? expectedJsonObject, JsonObject? jsonObject)
        {
            if (expectedJsonObject == null && jsonObject == null)
            {
                return true;
            }

            if (expectedJsonObject == null || jsonObject == null)
            {
                return false;
            }

            foreach (var expectedAttribute in expectedJsonObject)
            {

                if (!jsonObject.TryGetPropertyValue(expectedAttribute.Key, out var attributeValue))
                {
                    return false;
                }

                if (expectedAttribute.Value is JsonValue)
                {
                    if (!string.Equals(expectedAttribute.Value!.ToJsonString(), attributeValue!.ToJsonString()))
                    {
                        return false;
                    }
                }
                else if (expectedAttribute.Value is JsonArray expectedJsonArray)
                {
                    var jsonArray = (JsonArray)attributeValue;
                    for (int i = 0; i < expectedJsonArray.Count; i++)
                    {
                        var expectedJsonArrayItem = expectedJsonArray[i];
                        var jsonArrayItem = jsonArray[i];

                        if (expectedJsonArrayItem is JsonValue)
                        {
                            if (!string.Equals(expectedJsonArrayItem.ToJsonString(), jsonArrayItem!.ToJsonString()))
                            {
                                return false;
                            }
                        }

                        if (!PopulateJson(expectedJsonArrayItem as JsonObject, jsonArrayItem as JsonObject))
                        {
                            return false;
                        }
                    }
                }
                else if (expectedAttribute.Value is JsonObject nestedExpectedJsonObject)
                {
                    if (nestedExpectedJsonObject.TryGetPropertyValue("$assertType", out var assertNode))
                    {
                        var assertType = Enum.Parse<AssertType>(assertNode!.ToString());
                        var assertValue = nestedExpectedJsonObject["$value"]!.ToString();

                        if (assertType == AssertType.MatchRegex)
                        {
                            return Regex.IsMatch(attributeValue.ToString(), assertValue);
                        }
                    }
                    else if (!PopulateJson(nestedExpectedJsonObject, attributeValue as JsonObject))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
