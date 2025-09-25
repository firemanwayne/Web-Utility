namespace Web.Utility.Authorization.Extensions;

using Microsoft.AspNetCore.Authorization;

using System;
using System.Linq;
using System.Reflection;

public static class AuthorizationPolicyExtensions
{
    public static void AddPoliciesFromRequirements(this AuthorizationOptions options, Assembly assembly)
    {
        RegisterPoliciesFromAssembly(options, assembly);
    }

    private static void RegisterPoliciesFromAssembly(AuthorizationOptions options, Assembly assembly)
    {
        const string POLICYNAME = "PolicyName";

        var requirementType = typeof(IAuthorizationRequirement);

        var requirementTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface && requirementType.IsAssignableFrom(t));

        foreach (var type in requirementTypes)
        {
            // Look for a public const or static field/property called "PolicyName"
            var policyName = type.GetField(POLICYNAME, BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ??
                type.GetProperty(POLICYNAME, BindingFlags.Public | BindingFlags.Static)?.GetValue(null);

            if (policyName is string name)
            {
                // Create instance of requirement
                if (Activator.CreateInstance(type) is IAuthorizationRequirement requirement)
                {
                    options.AddPolicy(name, policy =>
                        policy.Requirements.Add(requirement));
                }
            }
        }
    }
}
