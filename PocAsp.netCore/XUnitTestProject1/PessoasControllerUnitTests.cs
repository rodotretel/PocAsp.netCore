using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using POC.NetCore.API.Controllers;
using POC.NetCore.Model;
using POC.NetCore.Services;
using POC.NetCore.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication1;
using Xunit;

namespace XUnitTestProject1
{
    public class PessoasControllerUnitTests
    {
        [Fact]
        public async Task Get_All()
        {
            // Arrange
            var controller = new PessoasController(new PessoaService());

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            List<Pessoa> Pessoas = (List <Pessoa>) okResult.Value.Should().BeAssignableTo<IEnumerable<Pessoa>>().Subject;

            Pessoas.Count.Should().Be(50);
        }

        [Fact]
        public async Task Get_Specific()
        {
            // Arrange
            var controller = new PessoasController(new PessoaService());

            // Act
            var result = await controller.Get(16);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var Pessoa = okResult.Value.Should().BeAssignableTo<Pessoa>().Subject;
            Pessoa.Id.Should().Be(16);
        }

        [Fact]
        public async Task Pessoa_Add()
        {
            // Arrange
            var controller = new PessoasController(new PessoaService());
            var novaPessoa = new Pessoa
            {
                PrimeiroNome = "John",
                LastName = "Doe",
                Idade = 50,
                Titulo = "FooBar",
                Email = "john.doe@foo.bar"
            };

            // Act
            var result = await controller.Post(novaPessoa);

            // Assert
            var okResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            var Pessoa = okResult.Value.Should().BeAssignableTo<Pessoa>().Subject;
            Pessoa.Id.Should().Be(51);
        }

        [Fact]
        public async Task Pessoa_Modificar()
        {
            // Arrange
            var service = new PessoaService();
            var controller = new PessoasController(service);
            var newPerson = new Pessoa
            {
                PrimeiroNome = "John",
                LastName = "Doe",
                Idade = 50,
                Titulo = "FooBar",
                Email = "john.doe@foo.bar"
            };

            // Act
            var result = await controller.Put(20, newPerson);

            // Assert
            var okResult = result.Should().BeOfType<NoContentResult>().Subject;

            var Pessoa = service.Get(20);
            Pessoa.Id.Should().Be(20);
            Pessoa.PrimeiroNome.Should().Be("John");
            Pessoa.LastName.Should().Be("Doe");
            Pessoa.Idade.Should().Be(50);
            Pessoa.Titulo.Should().Be("FooBar");
            Pessoa.Email.Should().Be("john.doe@foo.bar");
        }
    }
}
