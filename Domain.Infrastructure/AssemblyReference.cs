﻿using System.Reflection;

namespace Domain.Infrastructure;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
