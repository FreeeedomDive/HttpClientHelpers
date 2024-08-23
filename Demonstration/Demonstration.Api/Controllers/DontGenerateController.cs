using Microsoft.AspNetCore.Mvc;
using Xdd.HttpHelpers.Models.Attributes;

namespace Demonstration.Api.Controllers;

[DontGenerate]
/* Client will not be generated for controller with this attribute */
public class DontGenerateController : Controller
{
    [HttpGet("{id:guid}")]
    public void DoSomething()
    {
    }
}