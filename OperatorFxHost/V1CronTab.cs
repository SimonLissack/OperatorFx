using k8s.Models;
using OperatorFx.Domain.Models;

namespace OperatorFxHost;

// https://kubernetes.io/docs/tasks/extend-kubernetes/custom-resources/custom-resource-definitions/#create-a-customresourcedefinition
[KubernetesEntity(ApiVersion = "v1", Kind = "CronTab", Group = "stable.example.com", PluralName = "crontabs")]
[CustomResourceEntity]
public class V1CronTab : CustomResource
{
    public V1CronTabSpec Spec { get; set; } = new();
}

public class V1CronTabSpec
{
    public string CronSpec { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public int Replicas { get; set; } = 0;
}
