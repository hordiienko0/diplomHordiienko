using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ctor.IntegrationTests.MyEntityControllerTests;
public class MyEntityControllerFixture : IClassFixture<TestingWebAppFactory<Program>>
{
    protected const string _MY_ENTITY_CONTROLLER_API = "/api/MyEntity";
}
