﻿using Xdd.HttpHelpers.HttpClientGenerator.Models;
using Xdd.HttpHelpers.HttpClientGenerator.Options;

namespace Xdd.HttpHelpers.HttpClientGenerator.CodeGenerator;

internal interface ICommonInterfaceGenerator
{
    GeneratedFileContent Generate(ApiControllerInfo[] apiControllerInfos, string apiProjectName, GeneratorOptions options);
}