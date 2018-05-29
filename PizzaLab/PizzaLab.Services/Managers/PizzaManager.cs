using System.Collections.Generic;
using PizzaLab.Models;
using PizzaLab.Models.BindingModels;
using PizzaLab.Models.ViewModels;
using PizzaLab.Models.ViewModels.Ingredients;
using PizzaLab.Models.ViewModels.Pizzas;
using PizzaLab.Services.Interfaces;
using PizzaLab.Services.SqlServer;

namespace PizzaLab.Services.Managers
{
    public class PizzaManager : IPizzaManager
    {
        private IPizzaRepository pizzaRepository;

        public PizzaManager()
            : this(new PizzaRepository())
        {
        }

        public PizzaManager(IPizzaRepository pizzaRepository)
        {
            this.pizzaRepository = pizzaRepository;
        }

        public ICollection<PizzaDetailsViewModel> GetAllPizzas()
        {
            using (this.pizzaRepository)
            {
                ICollection<PizzaDetailsViewModel> allPizzas = this.pizzaRepository.GetAllPizzas();
                return allPizzas;
            }
        }

        public PizzaDetailsViewModel GetPizzaDetailsById(int pizzaId)
        {
            using (this.pizzaRepository)
            {
                PizzaDetailsViewModel pizza = this.pizzaRepository.GetPizzaDetailsById(pizzaId);
                return pizza;
            }
        }

        public MostWantedPizzaViewModel GetMostWantedPizza()
        {
            using (this.pizzaRepository)
            {
                MostWantedPizzaViewModel pizza = this.pizzaRepository.GetMostWantedPizza();
                return pizza;
            }
        }

        public PizzaOrderViewModel GetPizzaToOrder(int id)
        {
            using (this.pizzaRepository)
            {
                PizzaModel pizzaInfo = this.pizzaRepository.GetPizzaInfoById(id);

                ICollection<DoughTypeDescription> allDoughTypes = this.pizzaRepository.GetAllDoughTypes();
                ICollection<SizeDescription> allSizes = this.pizzaRepository.GetAllSizes();
                ICollection<IngredientDescription> allIngredients = this.pizzaRepository.GetAllIngredients();
                ICollection<IngredientDescription> pizzaIngredients = this.pizzaRepository.GetAllPizzaIngredients(id);
                ICollection<AllIngredientDescription> orderIngredients = this.MergeIngredients(allIngredients, pizzaIngredients);

                PizzaOrderViewModel pizzaOrder = new PizzaOrderViewModel(
                    pizzaInfo.Id,
                    pizzaInfo.Name,
                    pizzaInfo.Description,
                    pizzaInfo.PictureUrl,
                    allDoughTypes,
                    allSizes,
                    orderIngredients);

                return pizzaOrder;
            }
        }

        public ShoppingCartViewModel GetShoppingCart(ShoppingCart shoppingCart)
        {
            using (this.pizzaRepository)
            {
                ShoppingCartViewModel shoppingCartViewModel = new ShoppingCartViewModel();

                foreach (PizzaOrderBindingModel pizza in shoppingCart.Pizzas)
                {
                    PizzaModel pizzaInfo = this.pizzaRepository.GetPizzaInfoById(pizza.PizzaId);

                    decimal doughTypePrice = this.pizzaRepository.GetDoughTypePrice(pizza.DoughTypeId);
                    decimal sizePrice = this.pizzaRepository.GetSizePrice(pizza.SizeId);
                    decimal ingredientsPrice = this.pizzaRepository.GetIngredientsPrice(pizza.Ingredients);
                    decimal pizzaPrice = doughTypePrice + sizePrice + ingredientsPrice;

                    PizzaWithPrice pizzaWithPrice = new PizzaWithPrice(pizzaInfo.Id, pizzaInfo.Name, pizzaInfo.Description, pizzaInfo.PictureUrl, pizzaPrice);
                    shoppingCartViewModel.Pizzas.Add(pizzaWithPrice);
                }

                return shoppingCartViewModel;
            }
        }

        public bool FinishOrder(int userId, decimal totalPrice, ShoppingCart shoppingCart)
        {
            using (this.pizzaRepository)
            {
                bool createOrderResult = this.pizzaRepository.CreateOrder(userId, totalPrice, shoppingCart);
                return createOrderResult;
            }
        }

        private ICollection<AllIngredientDescription> MergeIngredients(ICollection<IngredientDescription> allIngredients, ICollection<IngredientDescription> pizzaIngredients)
        {
            IDictionary<int, AllIngredientDescription> orderIngredients = new Dictionary<int, AllIngredientDescription>();

            foreach (IngredientDescription allIngredient in allIngredients)
            {
                orderIngredients[allIngredient.Id] = new AllIngredientDescription(allIngredient.Id, allIngredient.Name, false);
            }

            foreach (IngredientDescription pizzaIngredient in pizzaIngredients)
            {
                orderIngredients[pizzaIngredient.Id].IsSelected = true;
            }

            return orderIngredients.Values;
        }
    }
}