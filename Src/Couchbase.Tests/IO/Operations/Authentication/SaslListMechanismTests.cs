﻿using System;
using System.Configuration;
using Couchbase.Configuration.Client;
using Couchbase.Core.Transcoders;
using Couchbase.IO;
using Couchbase.IO.Converters;
using Couchbase.IO.Operations.Authentication;
using Couchbase.IO.Services;
using Couchbase.Utils;
using NUnit.Framework;

namespace Couchbase.Tests.IO.Operations.Authentication
{
    [TestFixture]
    public class SaslListMechanismTests
    {
        private IIOService _ioService;
        private IConnectionPool _connectionPool;
        private readonly string _address = ConfigurationManager.AppSettings["OperationTestAddress"];
        private const uint OperationLifespan = 2500; //ms

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var ipEndpoint = UriExtensions.GetEndPoint(_address);
            var connectionPoolConfig = new PoolConfiguration();
            _connectionPool = new ConnectionPool<Connection>(connectionPoolConfig, ipEndpoint);
            _ioService = new PooledIOService(_connectionPool);
        }

        [Test]
        public void Test_SaslListMechanism()
        {
            var response = _ioService.Execute(new SaslList(new DefaultTranscoder(), OperationLifespan));
            Assert.That(() => !string.IsNullOrEmpty(response.Value));
            Console.WriteLine(response.Value);
            Assert.IsTrue(response.Success);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _connectionPool.Dispose();
        }
    }
}

#region [ License information          ]

/* ************************************************************
 *
 *    @author Couchbase <info@couchbase.com>
 *    @copyright 2014 Couchbase, Inc.
 *
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *
 *        http://www.apache.org/licenses/LICENSE-2.0
 *
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *
 * ************************************************************/

#endregion