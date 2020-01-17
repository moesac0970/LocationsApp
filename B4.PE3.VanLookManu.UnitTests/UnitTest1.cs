using B4.PE3.VanLookManu.Domain.Models;
using B4.PE3.VanLookManu.Domain.Services.Validator;
using Xunit;

namespace B4.PE3.VanLookManu.UnitTests
{
    /// <summary>
    /// sorry ik had weinig te testen dus heb ik er voor de opdracht 
    /// een validator tussen gegooid die makkelijk aan te passen is
    /// en te testen, sorry voor de oppervlakkigheid maar heb veel werk 
    /// van stage en andere EE's en deadlines 
    /// 
    /// hopelijk was het een goede app :D
    /// </summary>
    public class UnitTest1
    {
        Validator validator;

        [Fact]
        public void ValidUserNo()
        {
            //arange 
            var user = new User { Name = "" };

            //act 
            validator = new Validator();
            var val = validator.ValidatorUser(user);

            // assert
            Assert.Single(val);
        }


        [Fact]
        public void ValidUserYes()
        {
            //arange 
            var user = new User { Name = "ddddd" };

            //act 
            validator = new Validator();
            var val = validator.ValidatorUser(user);

            // assert
            Assert.Empty(val);
        }

        [Fact]
        public void ValidLocNo()
        {
            //arange 
            var locationUser = new LocationUser { Name = "" };

            //act 
            validator = new Validator();
            var val = validator.ValidatorLocation(locationUser);

            // assert
            Assert.Single(val);
        }

        [Fact]
        public void ValidLocYes()
        {
            //arange 
            var locationUser = new LocationUser { Name = "ddddd" };

            //act 
            validator = new Validator();
            var val = validator.ValidatorLocation(locationUser);

            // assert
            Assert.Empty(val);
        }
    }
}
