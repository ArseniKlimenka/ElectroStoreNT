using BLL.Interfaces;
using Common.DTO;
using ElectroStireNT.Controllers;
using ElectroStireNT.Controllers.Admin;
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
   public class ProductControllerTest
    {
        protected Mock<IProductService> productMockRepository;
        protected Mock<ICategoryService> categoryMockRepository;
        protected ProductController controllerUnderTest;

        [TestInitialize]
        public void SetupContext()
        {
            productMockRepository = new Mock<IProductService>();
            productMockRepository.Setup(svc => svc.GetProducts()).Returns(new List<ProductDTO>
            {
                new ProductDTO {ProductId = 1, Name = "Игра1"},
                new ProductDTO {ProductId = 2, Name = "Игра2"},
                new ProductDTO {ProductId = 3, Name = "Игра3"},
                new ProductDTO {ProductId = 4, Name = "Игра4"}
            });
            categoryMockRepository = new Mock<ICategoryService>();
            categoryMockRepository.Setup(svc => svc.GetCategories()).Returns(new List<CategoryDTO>
            {
                new CategoryDTO {Id = 1, CategoryName = "2"},
                new CategoryDTO {Id = 2, CategoryName = "3"},
                new CategoryDTO {Id = 3, CategoryName = "4"}

            });

            controllerUnderTest = new ProductController(productMockRepository.Object,
                                                     categoryMockRepository.Object);

        }


        [TestMethod]
        public void IndexViewModelNotNull()
        {            
         
            //Act
            ViewResult result = controllerUnderTest.Main() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void IndexViewEqualIndexCshtml()
        {       


            ViewResult result = controllerUnderTest.Main() as ViewResult;

            Assert.AreEqual("Main", result.ViewName);
        }
        [TestMethod]
        public void DeleteProductIsNull()
        {
          
            ViewResult result = controllerUnderTest.Delete(1) as ViewResult;

            Assert.IsNull(result);
        }
        [TestMethod]
        public void DeleteViewEqualIndexCshtml()
        {


            ViewResult result = controllerUnderTest.Main() as ViewResult;

            Assert.AreEqual("Main", result.ViewName);
        }


        [TestMethod]
        public void GetAllProducts()
        {           

            ViewResult result = controllerUnderTest.GetAllProducts() as ViewResult;

            Assert.IsNull(result);
            
        }
       
    }
}
