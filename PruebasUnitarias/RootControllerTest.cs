using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiAutores.Controllers.V1;
using WebApiAutores.Test.Mocks;

namespace WebApiAutores.Test.PruebasUnitarias
{
    [TestClass]
    public class RootControllerTest
    {
        [TestMethod]
        public async Task SiUsuarioEsAdmin_Obtenemos4Links()
        {
            //preparacion
            var authorizationServiceMock = new AuthorizationServicesMock();
            authorizationServiceMock.resultado = AuthorizationResult.Success();
            var rootController = new RootController(authorizationServiceMock);
            rootController.Url = new URLHelperMock();


            //ejecucion
            var resultado = await rootController.Get();

            //verificacion
            Assert.AreEqual(4, resultado.Value.Count());


        }
        
        [TestMethod]
        public async Task SiUsuarioNoEsAdmin_Obtenemos2Links()
        {
            //preparacion
            var authorizationServiceMock = new AuthorizationServicesMock();
            authorizationServiceMock.resultado = AuthorizationResult.Failed();

            var rootController = new RootController(authorizationServiceMock);
            rootController.Url = new URLHelperMock();


            //ejecucion
            var resultado = await rootController.Get();

            //verificacion
            Assert.AreEqual(2, resultado.Value.Count());


        }
        
        
        [TestMethod]
        public async Task SiUsuarioNoEsAdmin_Obtenemos2Links_UsandoMoq()
        {
            //preparacion
            var mockAuthorizationService = new Mock<IAuthorizationService>();
            mockAuthorizationService.Setup
                (
                    x => x.AuthorizeAsync
                        (
                            It.IsAny<ClaimsPrincipal>(), 
                            It.IsAny<object>(), 
                            It.IsAny<IEnumerable<IAuthorizationRequirement>>()
                        )
                )
                .Returns(Task.FromResult(AuthorizationResult.Failed()));

            mockAuthorizationService.Setup
                (
                    x => x.AuthorizeAsync
                        (
                            It.IsAny<ClaimsPrincipal>(),
                            It.IsAny<object>(),
                            It.IsAny<string>()
                        )
                )
                .Returns(Task.FromResult(AuthorizationResult.Failed()));

            var rootController = new RootController(mockAuthorizationService.Object);
            rootController.Url = new URLHelperMock();

            var mockURLHelper = new Mock<IUrlHelper>();
            mockURLHelper.Setup(x => x.Link(It.IsAny<string>(),It.IsAny<object>())).Returns("mockURL");

            rootController.Url = mockURLHelper.Object;


            //ejecucion
            var resultado = await rootController.Get();

            //verificacion
            Assert.AreEqual(2, resultado.Value.Count());


        }
    }
}
