using MauiAppMinhasCompras.Models;
using System;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // BOTÃO ADICIONAR PRODUTO
        private async void btnAdicionar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Produto p = new Produto();

                p.Descricao = txtDescricao.Text;
                p.Quantidade = Convert.ToInt32(txtQuantidade.Text);
                p.Preco = Convert.ToDouble(txtPreco.Text);

                await App.Db.Insert(p);

                await AtualizarLista();

                txtDescricao.Text = "";
                txtQuantidade.Text = "";
                txtPreco.Text = "";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        // ATUALIZAR LISTA DE PRODUTOS
        async Task AtualizarLista()
        {
            var lista = await App.Db.GetAll();

            listaProdutos.ItemsSource = lista;

            double total = 0;

            foreach (var item in lista)
            {
                total += item.Preco * item.Quantidade;
            }

            lblTotal.Text = "Total: R$ " + total.ToString("F2");
        }

        // BOTÃO APAGAR TODOS OS PRODUTOS
        private async void btnApagarTudo_Clicked(object sender, EventArgs e)
        {
            bool resposta = await DisplayAlert(
                "Confirmar",
                "Deseja apagar todos os produtos?",
                "Sim",
                "Não"
            );

            if (resposta)
            {
                await App.Db.DeleteAll();
                await AtualizarLista();
            }
        }
    }
}