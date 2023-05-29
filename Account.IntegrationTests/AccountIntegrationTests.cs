using Account.Application.Features.Account.Command.CreateAccount;
using Account.Application.Features.Account.Command.Deposit;
using Account.Application.Features.Account.Command.Withdraw;
using Account.Domain;
using Microsoft.AspNetCore.Mvc.Testing;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace AccountApiIntegrationTestsNamespace
{
    [TestClass]
    public class AccountApiIntegrationTests
    {
        private HttpClient _httpClient;
        private WebApplicationFactory<Program> _factory;

       
        public  AccountApiIntegrationTests()
        {
            _factory = new WebApplicationFactory<Program>();
            _httpClient = _factory.CreateClient();
        }

      

        [Fact]
        public async Task CreateAccount_ShouldReturnSuccess()
        {
            // Arrange
            var command = new CreateAccountCommand
            {
                UserId = 1,
                initial_balance=100
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/Account/CreateAccount", command);

            // Assert
            response.EnsureSuccessStatusCode();
            // Add additional assertions as needed
        }

        [Fact]
        public async Task GetAccounts_ShouldReturnSuccess()
        {
            // Arrange
            var userId = 1;

            // Act
            var response = await _httpClient.GetAsync($"/api/Account/GetAccounts/{userId}");

            // Assert
            response.EnsureSuccessStatusCode();
        
        }

        [Fact]
        public async Task Deposit_ShouldNotReturnSuccess_IF_AMOUNT_IS_10001()
        {
            var command = new CreateAccountCommand
            {
                UserId = 1,
                initial_balance = 100
            };

            // Act
            await _httpClient.PostAsJsonAsync("/api/Account/CreateAccount", command);

            var userId = 1;

            // Act
            var accountResponse = await _httpClient.GetAsync($"/api/Account/GetAccounts/{userId}");

           var accountresult= JsonConvert.DeserializeObject<List<UserAccount>>(await accountResponse.Content.ReadAsStringAsync());
            // Arrange
            var depositcommand = new DepositCommand
            {
                UserId = 1, 
                AccountId = accountresult[0].AccountId, 
                Amount = 10001
            };

            // Act
           var  result = await _httpClient.PostAsJsonAsync("/api/Transaction/Deposit", depositcommand);

            // Assert
            Xunit.Assert.False(result.IsSuccessStatusCode);
            // Add additional assertions as needed
        }

        [Fact]
        public async Task Deposit_ShouldReturnSuccess()
        {
            var command = new CreateAccountCommand
            {
                UserId = 1,
                initial_balance = 100
            };

            // Act
            await _httpClient.PostAsJsonAsync("/api/Account/CreateAccount", command);

            var userId = 1;

            // Act
            var accountResponse = await _httpClient.GetAsync($"/api/Account/GetAccounts/{userId}");

            var accountresult = JsonConvert.DeserializeObject<List<UserAccount>>(await accountResponse.Content.ReadAsStringAsync());
            // Arrange
            var depositcommand = new DepositCommand
            {
                UserId = 1,
                AccountId = accountresult[0].AccountId,
                Amount = 100
            };

            // Act
            var result = await _httpClient.PostAsJsonAsync("/api/Transaction/Deposit", depositcommand);

            // Assert
            result.EnsureSuccessStatusCode();
            // Add additional assertions as needed
        }

        [Fact]
        public async Task Withdraw_ShouldReturnSuccess()
        {
            var command = new CreateAccountCommand
            {
                UserId = 1,
                initial_balance = 100
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/Account/CreateAccount", command);

            var userId = 1;

            // Act
            var accountResponse = await _httpClient.GetAsync($"/api/Account/GetAccounts/{userId}");

            var accountresult = JsonConvert.DeserializeObject<List<UserAccount>>(await accountResponse.Content.ReadAsStringAsync());
            // Arrange
            var depositcommand = new DepositCommand
            {
                UserId = 1,
                AccountId = accountresult[0].AccountId,
                Amount = 500
            };

            // Act
            var result = await _httpClient.PostAsJsonAsync("/api/Transaction/Deposit", depositcommand);
            // Arrange
            var witcommand = new WithdrawCommand
            {
                UserId = 1,
                AccountId = accountresult[0].AccountId,
                Amount = 500 
            };

            // Act
            var witresponse = await _httpClient.PostAsJsonAsync("/api/Transaction/Withdraw", witcommand);

            // Assert
            witresponse.EnsureSuccessStatusCode();
          
        }


        [Fact]
        public async Task Withdraw_ShouldReturn_NotReturn_SuccessIf_Amount_is_More()
        {
            var command = new CreateAccountCommand
            {
                UserId = 1,
                initial_balance = 100
            };

            // Act
            await _httpClient.PostAsJsonAsync("/api/Account/CreateAccount", command);

            var userId = 1;

            // Act
            var accountResponse = await _httpClient.GetAsync($"/api/Account/GetAccounts/{userId}");

            var accountresult = JsonConvert.DeserializeObject<List<UserAccount>>(await accountResponse.Content.ReadAsStringAsync());
            // Arrange
            var depositcommand = new DepositCommand
            {
                UserId = 1,
                AccountId = accountresult[0].AccountId,
                Amount = 500
            };

        
            await _httpClient.PostAsJsonAsync("/api/Transaction/Deposit", depositcommand);
         
            var witcommand = new WithdrawCommand
            {
                UserId = 1,
                AccountId = accountresult[0].AccountId,
                Amount = 550
            };

     
            var witresponse = await _httpClient.PostAsJsonAsync("/api/Transaction/Withdraw", witcommand);

       
            Xunit.Assert.False(witresponse.IsSuccessStatusCode);

        }

        [Fact]
        public async Task DeleteAccount_ShouldReturnSuccess()
        {
           

            var command = new CreateAccountCommand
            {
                UserId = 1,
                initial_balance = 100
            };

            await _httpClient.PostAsJsonAsync("/api/Account/CreateAccount", command);

            var accountResponse = await _httpClient.GetAsync($"/api/Account/GetAccounts/{command.UserId}");

            var accountresult = JsonConvert.DeserializeObject<List<UserAccount>>(await accountResponse.Content.ReadAsStringAsync());
           


            var deleteResponse = await _httpClient.DeleteAsync($"/api/Account/DeleteAccount/{command.UserId}/{accountresult[0].AccountId}");

       
            deleteResponse.EnsureSuccessStatusCode();
      
        }
    }
}
