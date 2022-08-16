using AgendaDeContatos.Core;
using AgendaDeContatos.Infra.Interfaces;
using AgendaDeContatos.Infra.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaDeContatos.Contatos
{
    public partial class FrmContatosManutencao : Form
    {
        public readonly OperacaoCadastro _operacaoCadastro;
        private readonly IRepository<Categoria, int> _categoriaRepository;
        private readonly IRepository<Contato, int> _contatoRepository;

        protected Contato Contato { get; set; }

        public FrmContatosManutencao(OperacaoCadastro operacaoCadastro, Contato contato)
        {
            InitializeComponent();
            _categoriaRepository = Program.ServiceProvider.GetService<IRepository<Categoria, int>>();
            _contatoRepository = Program.ServiceProvider.GetService<IRepository<Contato, int>>();
            _operacaoCadastro = operacaoCadastro;
            Contato = contato;
            contatosBindingSource.DataSource = Contato;
            InicializarDados();
        }


        private async void InicializarDados()
        {
            categoriasBindingSource.DataSource = await _categoriaRepository.ObterTodosAsync();
            switch (_operacaoCadastro)
            {
                case OperacaoCadastro.Incluir:
                    {
                        Contato.DataCadastro = DateTime.Now;
                        lblMensagem.Text = string.Format(lblMensagem.Text, "Inclusão");
                        this.Text = string.Format(this.Text, " - [Inclusão de Contato]");
                    }
                    break;
                case OperacaoCadastro.Alterar:
                    {
                        lblMensagem.Text = string.Format(lblMensagem.Text, "Alteração");
                        this.Text = string.Format(this.Text, " - [Alteração de Contato]");
                    }
                    break;
                case OperacaoCadastro.Excluir:
                    {
                        lblMensagem.Text = string.Format(lblMensagem.Text, "Exclusão");
                        this.Text = string.Format(this.Text, " - [Exclusão de Contato]");
                    }
                    break;
                case OperacaoCadastro.Consultar:
                    {
                        lblMensagem.Text = string.Format(lblMensagem.Text, "Consulta");
                        this.Text = string.Format(this.Text, " - [Consultar Contato]");
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
                    await _contatoRepository.InsertAsync(Contato);
                    break;
                case OperacaoCadastro.Alterar:
                    await _contatoRepository.UpdateAsync(Contato);
                    break;
                case OperacaoCadastro.Excluir:
                    await _contatoRepository.DeleteAsync(Contato);
                    break;
            }
            Close();
        }

        private void btnNao_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void FrmContatosManutencao_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
