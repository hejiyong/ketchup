﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Ketchup.Core.Modules
{
    public class KernelModuleProvider : IKernelModuleProvider
    {
        private readonly List<KernelModule> _modules;
        private readonly KetchupPlatformContainer _container;
        private readonly ILogger<KernelModuleProvider> _logger;

        public KernelModuleProvider(List<KernelModule> modules, KetchupPlatformContainer container, ILogger<KernelModuleProvider> logger)
        {
            _modules = modules;
            _container = container;
            _logger = logger;
        }

        public List<KernelModule> Modules => _modules;

        public void Initialize()
        {
            _modules.ForEach(item =>
            {
                try
                {
                    item.Initialize(_container);
                }
                catch (Exception e)
                {
                    _logger.Log(LogLevel.Error, e, "initialize exception ...");
                }
            });
        }
    }
}
