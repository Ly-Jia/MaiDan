using MaiDan.Infrastructure;
using MaiDan.Service.Business;
using MaiDan.Service.Business.DataContract;
using MaiDan.Service.Dal;
using MaiDan.Service.Domain;
using NFluent;
using TechTalk.SpecFlow;


namespace Test.MaiDan.Service.Business.Integration
{
    [Binding]
    public class ChiefSteps
    {
        private string dishId;
        private string dishName;
        private Menu menu = new Menu();
        private Chief chief;
        private readonly Database database = new Database();

        [Given(@"A dish (.*) that I want to propose to my customers")]
        public void GivenADishThatIWantToProposeToMyCustomers(string dish)
        {
            this.dishId = dish;
            this.dishName = dish;
        }

        [When(@"I add it to the menu")]
        public void WhenIAddItToTheMenu()
        {
            var dishContract = new DishDataContract { Id = dishId, Name = dishName };
            this.chief.AddToMenu(dishContract);
        }

        [Then(@"the dish (.*) can be ordered by the customers")]
        public void ThenTheDishCanBeOrderedByTheCustomers(string dish)
        {
            using (var session = database.OpenSession())
            {
                var dishInMenu = session.QueryOver<Dish>().Where(d => d.Name == dishName).SingleOrDefault();
                Check.That(dishInMenu).IsNotNull();
            }
        }

        [Given(@"A dish (.*) \- (.*) in the menu")]
        public void GivenADishInTheMenu(string id, string name)
        {
            this.dishId = id;
            this.dishName = name;
            using (var session = database.OpenSession())
            {
                session.Transaction.Begin();
                session.Save(new Dish(id, name));
                session.Transaction.Commit();
            }
        }

        [When(@"I update the name to (.*)")]
        public void WhenIUpdateTheNameTo(string newDishName)
        {
            var dishContract = new DishDataContract { Id = dishId, Name = newDishName };
            this.chief.Update(dishContract);
        }

        [Then(@"the dish (.*) is displayed as (.*)")]
        public void ThenTheDishIsDisplayedAs(string id, string name)
        {
            using (var session = database.OpenSession())
            {
                var dishInMenu = session.Get<Dish>(id);
                Check.That(dishInMenu.Name).Equals(name);
            }
        }

        [BeforeScenario("chief")]
        public void Initialize()
        {
            this.chief = new Chief(menu);
        }

        [AfterScenario("chief")]
        public void Cleanup()
        {
            using (var session = database.OpenSession())
            {
                session.Transaction.Begin();
                session.Delete("from Dish");
                session.Transaction.Commit();
            }
        }
    }
}
