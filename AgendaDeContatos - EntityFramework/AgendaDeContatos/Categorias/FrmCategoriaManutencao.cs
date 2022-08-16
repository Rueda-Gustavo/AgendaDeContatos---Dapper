using AgendaDeContatos.Core;
using AgendaDeContatos.Core.Interfaces;
using AgendaDeContatos.Infra.Interfaces;
using AgendaDeContatos.Infra.Models;
using Microsoft.Extensions.DependencyInjection;


namespace AgendaDeContatos.Categorias.Models
{
    public partial class FrmCategoriaManutencao : Form
    {
        public readonly OperacaoCadastro _operacaoCadastro;
        private readonly IRepository<Categoria,int> _categoriasRepository;

        protected Categoria Categoria { get; set; }
        public FrmCategoriaManutencao(OperacaoCadastro operacaoCadastro, Categoria categoria)
        {
            InitializeComponent();
            _categoriasRepository = Program.ServiceProvider.GetService<IRepository<Categoria, int>>();
            _operacaoCadastro = operacaoCadastro;
            Categoria = categoria;
            categoriaBindingSource.DataSource = Categoria;
            IncializarDados();
        }

        private void IncializarDados()
        {
            switch (_operacaoCadastro)
            {
                case OperacaoCadastro.Incluir:
                    {                        
                        lblMensagem.Text = string.Format(lblMensagem.Text, "Inclusão");
                        this.Text = string.Format(this.Text, " - [Inclusão de Categoria]");
                    }
                    break;
                case OperacaoCadastro.Alterar:
                    {
                        lblMensagem.Text = string.Format(lblMensagem.Text, "Alteração");
                        this.Text = string.Format(this.Text, " - [Alteração de Categoria]");
                    }
                    break;
                case OperacaoCadastro.Excluir:
                    {
                        lblMensagem.Text = string.Format(lblMensagem.Text, "Exclusão");
                        this.Text = string.Format(this.Text, " - [Exclusão de Categoria]");
                    }
                    break;
                case OperacaoCadastro.Consultar:
                    {
                        lblMensagem.Text = string.Format("Consultar Categoria.");
                        this.Text = string.Format("[Consultar Categoria]");
                        btnSim.Enabled = false;
                        btnNao.Text = "Fechar";
                    }
                    break;
            }
        }

        private async void btnSim_Click(object sender, EventArgs e)
        {
            switch (_operacaoCadastro)
            {
                case OperacaoCadastro.Incluir:
                    await _categoriasRepository.InsertAsync(Categoria);
                    break;
                case OperacaoCadastro.Alterar:
                    await _categoriasRepository.UpdateAsync(Categoria);
                    break;
                case OperacaoCadastro.Excluir:
                    await _categoriasRepository.DeleteAsync(Categoria);
                    break;
            }
            Close();
        }

        private void btnNao_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
