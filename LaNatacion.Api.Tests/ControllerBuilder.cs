using LaNacion.Api.Controllers.Api.Version1;
using System;

namespace LaNatacion.Api.Tests
{
    public class ControllerBuilder<T>
    {
        private T _Controller { get; set; }

        public ControllerBuilder(T controller) { this._Controller = controller; }

        public ControllerBuilder()
        {
        }

        private ContactController contactController;

        public T Build(TestServiceWebApplicationFactory factory)
        {
            if (GetDeclaredType(_Controller) == this.GetDeclaredType(this.contactController))
            {
                return (T)Convert.ChangeType(new ContactController(factory.Logger,factory.ContactManager), typeof(T));
            }
            else
            {
                throw new Exception("unrecognized type");
            }
        }

        private Type GetDeclaredType<K>(K obj)
        {
            return typeof(K);
        }

    }
}
