﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.DotNet.Interactive.Parsing;

namespace Microsoft.DotNet.Interactive.Commands;

public class SubmitCode : KernelCommand
{
    public SubmitCode(
        string code,
        string targetKernelName = null,
        SubmissionType submissionType = SubmissionType.Run) : base(targetKernelName)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        SubmissionType = submissionType;
    }
      
    internal SubmitCode(
        LanguageNode languageNode,
        SubmissionType submissionType = SubmissionType.Run,
        KernelNameDirectiveNode kernelNameDirectiveNode = null)
        : base(languageNode.Name)
    {
        Code = languageNode.Text;
        LanguageNode = languageNode;
        SubmissionType = submissionType;
        KernelNameDirectiveNode = kernelNameDirectiveNode;
        SchedulingScope = languageNode.CommandScope;

        if (languageNode is ActionDirectiveNode actionDirectiveNode)
        {
            TargetKernelName = actionDirectiveNode.ParentKernelName;
        }
    }

    public string Code { get; internal set; }

    public SubmissionType SubmissionType { get; }

    public override string ToString() => $"{nameof(SubmitCode)}: {Code?.TruncateForDisplay()}";

    internal LanguageNode LanguageNode { get; }

    internal KernelNameDirectiveNode KernelNameDirectiveNode { get; }
}