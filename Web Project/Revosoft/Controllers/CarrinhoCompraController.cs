using Revosoft.Models;
using Revosoft.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Revosoft.Controllers
{
    [Authorize]
    public class CarrinhoCompraController : Controller
    {
        private readonly IStoreRepository _storeRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(IStoreRepository storeRepository, CarrinhoCompra carrinhoCompra)
        {
            _storeRepository = storeRepository;
            _carrinhoCompra = carrinhoCompra;
        }
        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();

            _carrinhoCompra.CarrinhoCompraItems = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal(),
            };

            return View(carrinhoCompraVM);
        }

        public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int storeId)
        {
            var pecaSelecionada = _storeRepository.Store.FirstOrDefault(p => p.StoreId == storeId);

            if (pecaSelecionada != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(pecaSelecionada);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult RemoverItemDoCarrinhoCompra(int storeId)
        {
            var pecaSelecionada = _storeRepository.Store.FirstOrDefault(p => p.StoreId == storeId);

            if (pecaSelecionada != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(pecaSelecionada);
            }
            return RedirectToAction("Index");
        }
    }
}
