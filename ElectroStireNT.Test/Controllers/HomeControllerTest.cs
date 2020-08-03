using BLL.Interfaces;
using Common.DTO;
using Common.Entitites;
using ElectroStireNT.Controllers;
using ElectroStireNT.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ElectroStireNT.Test.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        protected Mock<IProductService> productMockRepository;
        protected Mock<IOrderService> orderMockRepository;
        protected Mock<ShoppingCartFactory> shopMockRepository;
        protected HomeController controllerUnderTest;

       // [TestMethod]
       // public void IndexViewModelNotNull(int? id, string category, int? page, string searchName)
       // {
       //     // Arrange
            
       //     productMockRepository = new Mock<IProductService>();
       //     productMockRepository.Setup(svc => svc.GetProducts()).Returns(new List<ProductDTO>
       //     {
       //         new ProductDTO {ProductId = 1, Name = "Игра1"},
       //         new ProductDTO {ProductId = 2, Name = "Игра2"},
       //         new ProductDTO {ProductId = 3, Name = "Игра3"},
       //         new ProductDTO {ProductId = 4, Name = "Игра4"}
       //     });
       //     orderMockRepository = new Mock<IOrderService>();
       //     shopMockRepository = new Mock<ShoppingCartFactory>();

       //     controllerUnderTest = new HomeController(productMockRepository.Object,
       //                                              orderMockRepository.Object,
       //                                              shopMockRepository.Object);
           

       //// Act
       // ViewResult result = controllerUnderTest.Index(1,"1",1, "1") as ViewResult;

       // // Assert
       // Assert.IsNull(result);  
       // }

       
        
    }
}




