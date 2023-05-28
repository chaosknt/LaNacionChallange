using LaNacion.Data;

namespace LaNatacion.Api.Tests
{
    public static class DatabaseHelper
    {
        public static void InitContacts(LaNacionContext database)
        {
            var fakeData = new ContactsFakeData();

            foreach (var item in fakeData.Contacts)
            {
                database.Contacts.Add(item);
            }

            foreach (var item in fakeData.Phones)
            {
                database.PhoneNumbers.Add(item);
            }

             database.SaveChanges();
        }
    }
}
