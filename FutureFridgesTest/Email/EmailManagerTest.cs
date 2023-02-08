using FutureFridges.Business.Email;

namespace FutureFridgesTest.Email
{
    [TestClass]
    public class EmailManagerTest
    {
        [TestMethod]
        public void EmailManager_IsValidEmail_ReturnsFalseForEmailWithNoAsperand ()
        {

            IEmailManager _EmailManager = new EmailManager();

            string _EmailMissingAsperand = "test.com";

            bool _Result = _EmailManager.IsValidEmail(_EmailMissingAsperand);

            Assert.IsFalse(_Result);
        }

        [TestMethod]
        public void EmailManager_IsValidEmail_ReturnsFalseForEmailWithNoTextBeforeAsperand ()
        {
            IEmailManager _EmailManager = new EmailManager();

            string _EmailMissingStart = "@e.com";

            bool _Result = _EmailManager.IsValidEmail(_EmailMissingStart);

            Assert.IsFalse(_Result);
        }

        [TestMethod]
        public void EmailManager_IsValidEmail_ReturnsTrueForValidEmail ()
        {

            IEmailManager _EmailManager = new EmailManager();

            string _ValidEmail = "test@email.com";

            bool _Result = _EmailManager.IsValidEmail(_ValidEmail);

            Assert.IsTrue(_Result);
        }
    }
}
