using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeradorDeProcessos.Models
{
	[MetadataType(typeof(EmpreendimentosMetadado))]
	public partial class Empreendimentos
	{
	}

	public class EmpreendimentosMetadado
	{
		[DisplayName("Empreendimento")]
		public int IDEmpreendimento { get; set; }
		[Required(ErrorMessage = "Obrigatório informar nome do empreendimento")]
		[StringLength(80, ErrorMessage = "Nome do empreendimento deve possuir no máximo 80 caracteres")]
		public string Nome { get; set; }
		[DisplayName("Habite-se")]
		[Required(ErrorMessage = "Data de habite-se é obrigatória")]
		//[DisplayFormat(DataFormatString = "{dd/mm/yyyy}", ApplyFormatInEditMode = true)]
		public System.DateTime DataEntrega { get; set; }
		public string Produto { get; set; }
		public string Campanha { get; set; }
		[DisplayName("Empresa")]
		public int IDEmpresa { get; set; }
	}
}