using k8s.Models;
using OperatorFxNet.Domain.Models;

namespace OperatorFxNetHost;

[KubernetesEntity(ApiVersion = "v1", Kind = "SampleCustomResource", Group = "lissack.io", PluralName = "samplecustomresources")]
public class SampleCustomResource : CustomResource
{
}
