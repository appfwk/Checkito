using Checkito.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Checkito.Environments.Models
{
    public class ExecutionEnv : EntityBase
    {
        public string Name { get; set; } = "local";
        public IDictionary<string, string> Variables { get; set; } = new Dictionary<string, string>();
        public bool IsSelected { get; set; }

        public void SetVariables(string formattedVariables)
        {
            Variables.Clear();

            if (!string.IsNullOrWhiteSpace(formattedVariables))
            {
                foreach (var formattedValue in formattedVariables.Split(Environment.NewLine.ToCharArray()))
                {
                    if (string.IsNullOrWhiteSpace(formattedValue))
                    {
                        continue;
                    }

                    //TODO Add Validation
                    var separatorIndex = formattedValue.IndexOf(':');
                    if (separatorIndex > -1)
                    {
                        Variables.Add(formattedValue.Substring(0, separatorIndex).Trim(), formattedValue.Substring(separatorIndex + 1).Trim());
                    }
                    else
                    {
                        Variables.Add(formattedValue.Substring(0).Trim(), string.Empty);
                    }
                }
            };
        }

        public string? ResolveVariables(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            var result = Regex.Replace(value, @"\{\{(?<exp>[^}]+)\}\}", match =>
            {
                var groupes = match.Groups["exp"];
                if (Variables.TryGetValue(groupes.Value, out var variableValue))
                {
                    return ResolveVariables(variableValue);
                }

                return match.Value;
            });

            return result;
        }

        public string GetVariables()
        {
            var formattedValue = new StringBuilder();
            foreach (var variable in Variables)
            {

                formattedValue
                    .Append(variable.Key)
                    .Append(": ")
                    .Append(variable.Value);

                formattedValue.AppendLine();
            }

            return formattedValue.ToString();
        }
    }
}
