using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections;
using System.Collections.Generic;
using WorkManager.Controllers;
using WorkManager.Data.Models;
using WorkManager.Responses.Interfaces;
using Xunit;

namespace WorkManagerTests
{
    public class ClientControllerTests
    {
        /// <summary>
        /// Обертка IEnumerable основного класса для тестирования
        /// </summary>
        private class ClientData : IEnumerable<Client[]>
        {
            public IEnumerator<Client[]> GetEnumerator()
            {
                yield return new Client[]
                {
                    _client
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private readonly ClientController _clientController;

        private readonly Mock<ILogger<ClientController>> _mockLogger;

        private readonly Mock<IValidator<Client>> _mockValidator;

        private readonly Mock<IResponse<Client>> _mockResponse;

        private static Client _client = new Client()
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Testov",
            Email = "test@test.com",
            Age = 99
        };

        public ClientControllerTests()
        {
            _mockLogger = new Mock<ILogger<ClientController>>();
            _mockValidator = new Mock<IValidator<Client>>();
            _mockResponse = new Mock<IResponse<Client>>();

            _clientController = new ClientController(_mockLogger.Object, _mockResponse.Object, _mockValidator.Object);
        }

        #region RegisterElement
        [Theory]
        [ClassData(typeof(ClientData))]
        public void RegisterElementTest_CheckCall(Client client)
        {
            // Arrange
            _mockResponse
                .Setup(resp => resp.Register(client));
            _mockValidator
                .Setup(val => val.Validate(client))
                .Returns(new FluentValidation.Results.ValidationResult());

            // Act
            var result = _clientController.RegisterElement(client);

            // Assert
            _mockResponse
                .Verify(resp => resp.Register(client), Times.Once());

        }

        [Theory]
        [ClassData(typeof(ClientData))]
        public void RegisterElementTest_CheckData(Client client)
        {
            // Arrange
            _mockResponse
                .Setup(resp => resp.Register(client));
            _mockValidator
                .Setup(val => val.Validate(client))
                .Returns(new FluentValidation.Results.ValidationResult());

            // Act
            var result = _clientController.RegisterElement(client);
            OkObjectResult resultOkObject = (OkObjectResult)result;
            string? resultObj = resultOkObject.Value as string;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(resultOkObject);
            Assert.NotNull(resultObj);
            Assert.Equal($"Клиент {client.FirstName} {client.LastName} был создан!", resultObj);

        }
        #endregion

        #region GetElements
        [Fact]
        public void GetElementsTest_CheckCall()
        {
            // Arrange
            _mockResponse
                .Setup(resp => resp.GetAllData());

            // Act
            var result = _clientController.GetElements();

            // Assert
            _mockResponse
                .Verify(resp => resp.GetAllData(), Times.Once());

        }

        [Fact]
        public void GetElementsTest_CheckData()
        {
            // Arrange
            _mockResponse
                .Setup(resp => resp.GetAllData())
                .Returns(new Dictionary<int, Client>()
                {
                    {
                        1,
                        _client
                    }
                });

            // Act
            var result = _clientController.GetElements();
            OkObjectResult resultOkObject = (OkObjectResult)result;
            Dictionary<int, Client>? resultObj = resultOkObject.Value as Dictionary<int, Client>;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(resultOkObject);
            Assert.NotNull(resultObj);
            Assert.Equal("Test", resultObj[1].FirstName);
        }
        #endregion

        #region GetById
        [Theory]
        [InlineData(1)]
        public void GetByIdTest_CheckCall(int id)
        {
            // Arrange
            _mockResponse
                .Setup(resp => resp.GetById(id));

            // Act
            var result = _clientController.GetById(id);

            // Assert
            _mockResponse
                .Verify(resp => resp.GetById(id), Times.Once());
        }

        [Theory]
        [InlineData(1)]
        public void GetByIdTest_CheckData(int id)
        {
            // Arrange
            _mockResponse
                .Setup(resp => resp.GetById(id))
                .Returns(_client);

            // Act
            var result = _clientController.GetById(id);
            OkObjectResult resultOkObject = (OkObjectResult)result;
            Client? resultObj = resultOkObject.Value as Client;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(resultOkObject);
            Assert.NotNull(resultObj);
            Assert.Equal(1, resultObj.Id);
        }
        #endregion

        #region UpdateById
        [Theory]
        [InlineData(1, "Company", "TestCompany")]
        [InlineData(10, "FirstName", "TestFirstName")]
        public void UpdateByIdTest_CheckCall(int id, string reqColumnName, string value)
        {
            // Arrange
            _mockResponse
                .Setup(resp => resp.UpdateById(id, reqColumnName, value));

            // Act
            var result = _clientController.UpdateById(id, reqColumnName, value);

            // Assert
            _mockResponse
                .Verify(resp => resp.UpdateById(id, reqColumnName, value), Times.Once());
        }

        [Theory]
        [InlineData(1, "Company", "TestCompany")]
        [InlineData(10, "FirstName", "TestFirstName")]
        public void UpdateByIdTest_CheckData(int id, string reqColumnName, string value)
        {
            // Arrange
            _mockResponse
                .Setup(resp => resp.UpdateById(id, reqColumnName, value));

            // Act
            var result = _clientController.UpdateById(id, reqColumnName, value);
            OkObjectResult resultOkObject = (OkObjectResult)result;
            string? resultObj = resultOkObject.Value as string;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(resultOkObject);
            Assert.NotNull(resultObj);
            Assert.Equal($"Клиенту с id {id} был обновлен параметр {reqColumnName} на значение {value}!", resultObj);
        }
        #endregion

        #region DeleteById
        [Theory]
        [InlineData(1)]
        public void DeleteByIdTest_CheckCall(int id)
        {
            // Arrange
            _mockResponse
                .Setup(resp => resp.DeleteById(id));

            // Act
            var result = _clientController.DeleteById(id);

            // Assert
            _mockResponse
                .Verify(resp => resp.DeleteById(id), Times.Once());
        }

        [Theory]
        [InlineData(1)]
        public void DeleteByIdTest_CheckData(int id)
        {
            // Arrange
            _mockResponse
                .Setup(resp => resp.DeleteById(id));

            // Act
            var result = _clientController.DeleteById(id);
            OkObjectResult resultOkObject = (OkObjectResult)result;
            string? resultObj = resultOkObject.Value as string;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(resultOkObject);
            Assert.NotNull(resultObj);
            Assert.Equal($"Клиент с id {id} был успешно удален!", resultObj);

        }
        #endregion
    }
}