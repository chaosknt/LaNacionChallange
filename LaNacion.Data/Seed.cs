using LaNacion.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaNacion.Data
{
    public class Seed
    {
        public Seed()
        {
        }

        public static void Init(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<LaNacionContext>();

                var contacts = context.Contacts.ToList();
                if (contacts.Any())
                {
                    return;
                }

                var fakeData = new ContactsFakeData();
                foreach (var item in fakeData.Contacts)
                {
                    context.Contacts.Add(item);
                }

                foreach (var item in fakeData.Phones)
                {
                    context.PhoneNumbers.Add(item);
                }

                context.SaveChanges();
            }
        }
    }
}
