using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Test.PruebasUnitarias
{
    [TestClass]
    public class PrimeraLetraMayusculaTest
    {
        [TestMethod]
        public void PrimeraLetraMinuscula_DevuelveError()
        {
            //preparacion
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            var valor = "pepe";
            var valContext = new ValidationContext(new {a=valor});


            //ejecucion
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);


            //verificacion
            Assert.AreEqual("La primera letra del nombre debe ser mayúscula", resultado.ErrorMessage);
        }
        
        [TestMethod]
        public void ValorNulo_NoDevuelveError()
        {
            //preparacion
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            string valor = null;
            var valContext = new ValidationContext(new {a=valor});


            //ejecucion
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);


            //verificacion
            Assert.IsNull(resultado);
        }


        [TestMethod]
        public void ValorConPrimeraLetraMayuscula_NoDevuelveError()
        {
            //preparacion
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            string valor = "Pepe";
            var valContext = new ValidationContext(new {a=valor});


            //ejecucion
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);


            //verificacion
            Assert.IsNull(resultado);
        }
    }
}