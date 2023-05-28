using Xunit;

namespace LaNatacion.Api.Tests
{
    public abstract class BasePortalControllerTestClass<T> : IClassFixture<TestServiceWebApplicationFactory>
    {
        internal readonly TestServiceWebApplicationFactory factory;
        internal T Controller;


        public BasePortalControllerTestClass(TestServiceWebApplicationFactory factory)
        {
            this.factory = factory;
            this.BuildController();
        }


        private void BuildController()
        {
            this.Controller = new ControllerBuilder<T>().Build(factory);
        }
    }
}
