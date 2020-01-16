﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Peppy.Dependency
{
    /// <summary>
    /// All classes implement this interface are automatically registered to dependency injection as singleton object.
    /// </summary>
    public interface ISingletonDependency : IDependency
    {
    }
}