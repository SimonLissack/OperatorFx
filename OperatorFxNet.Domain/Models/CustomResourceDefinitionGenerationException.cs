namespace OperatorFxNet.Domain.Models;

public class CustomResourceDefinitionGenerationException<T>(string message)
    : Exception($"Could not generate Custom Resource Definition for resource {nameof(T)}: {message}") where T : CustomResource;
