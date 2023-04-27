using CloudCustomer.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomer.Tests.Fixtures
{
    public static class UsersFixtures
    {
        public static List<User> GetTestUsers() => new()
        {
                new User {
                    Id = 1,
                    Name = "Test1",
                    Address = new Address {
                        Street = "123 Test",
                        City = "Test Land",
                        ZipCode = "12345"
                    },
                    Email = "Test1@xunit.com"
                },
                new User {
                    Id = 2,
                    Name = "Test2",
                    Address = new Address {
                        Street = "123 Test",
                        City = "Test Land",
                        ZipCode = "12345"
                    },
                    Email = "Test2@xunit.com"
                },
                new User {
                    Id = 3,
                    Name = "Test3",
                    Address = new Address {
                        Street = "123 Test",
                        City = "Test Land",
                        ZipCode = "12345"
                    },
                    Email = "Test3@xunit.com"
                },
                new User {
                    Id = 4,
                    Name = "Test4",
                    Address = new Address {
                        Street = "123 Test",
                        City = "Test Land",
                        ZipCode = "12345"
                    },
                    Email = "Test4@xunit.com"},
                new User {
                    Id = 5,
                    Name = "Test5",
                    Address = new Address {
                        Street = "123 Test",
                        City = "Test Land",
                        ZipCode = "12345"
                    },
                    Email = "Test5@xunit.com"}
            };
    }
}
