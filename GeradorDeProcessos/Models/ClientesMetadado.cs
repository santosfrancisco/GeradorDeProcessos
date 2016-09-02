using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeradorDeProcessos.Models
{
	[MetadataType(typeof(ClientesMetadado))]
	public partial class Clientes
	{
	}

	public class ClientesMetadado
	{
		public int IDCliente { get; set; }
		[DisplayName("Tipo pessoa")]
		public int TipoPessoa { get; set; }
		[Required(ErrorMessage = "Obrigatório informar CPF/CNPJ")]
		public string CpfCnpj { get; set; }
		[Required(ErrorMessage = "Nome do cliente é obrigatório")]
		[StringLength(80, ErrorMessage = "O nome do cliente deve possuir no máximo 80 caracteres")]
		public string Nome { get; set; }
		public string Sexo { get; set; }
		[DisplayName("Profissão")]
		[StringLength(60, ErrorMessage = "Profissão deve ter no máximo 60 caracteres" )]
		public string Profissao { get; set; }
		[DisplayName("Data de nascimento")]
		public Nullable<System.DateTime> DataNascimento { get; set; }
		[Required(ErrorMessage = "Obrigatório informar a renda do cliente")]
		public string Renda { get; set; }
		[DisplayName("Estado civil")]
		public string EstadoCivil { get; set; }
		[DisplayName("Regime de casamento")]
		public string RegimeCasamento { get; set; }
		[DisplayName("CPF cônjuge")]
		public string Conjuge_Cpf { get; set; }
		[DisplayName("Nome cônjuge")]
		[StringLength(80, ErrorMessage = "O nome do(a) cônjuge deve possuir no máximo 80 caracteres")]
		public string Conjuge_Nome { get; set; }
		[DisplayName("Usuário")]
		public int IDUsuario { get; set; }



		public string Cliente
		{
			get
			{
				return string.Format("{0} - {1}", Nome, CpfCnpj);
			}
		}
	}
}