using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeradorDeProcessos.Repositorios
{
	public class RepositorioListas
	{
		// lista de tipos de pessoa
		public static List<SelectListItem> TipoPessoa()
		{
			IList<SelectListItem> tipo = new List<SelectListItem>();
			tipo.Add(new SelectListItem() { Text = "Física", Value = "0" });
			tipo.Add(new SelectListItem() { Text = "Jurídica", Value = "1" });

			return tipo.ToList();
		}
		// lista de sexos
		public static List<SelectListItem> Sexo()
		{
			IList<SelectListItem> sexos = new List<SelectListItem>();
			sexos.Add(new SelectListItem() { Text = "Masculino", Value = "Masculino" });
			sexos.Add(new SelectListItem() { Text = "Feminino", Value = "Feminino" });
			return sexos.ToList();
		}
		// lista de estados civis
		public static List<SelectListItem> EstadoCivil()
		{
			IList<SelectListItem> estadosCivis = new List<SelectListItem>();
			estadosCivis.Add(new SelectListItem() { Text = "Solteiro(a)", Value = "Solteiro(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "Casado(a)", Value = "Casado(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "Divorciado(a)", Value = "Divorciado(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "Separado(a) judicialmente", Value = "Separado(a) judicialmente" });
			estadosCivis.Add(new SelectListItem() { Text = "Viúvo(a)", Value = "Viúvo(a)" });
			estadosCivis.Add(new SelectListItem() { Text = "União Estável", Value = "União Estável" });
			return estadosCivis.ToList();
		}

		// lista de regimes de casamento
		public static List<SelectListItem> RegimeCasamento()
		{
			IList<SelectListItem> regimesCasamento = new List<SelectListItem>();
			regimesCasamento.Add(new SelectListItem() { Text = "Comunhão universal de bens", Value = "Comunhão universal de bens" });
			regimesCasamento.Add(new SelectListItem() { Text = "Comunhão parcial de bens", Value = "Comunhão parcial de bens" });
			regimesCasamento.Add(new SelectListItem() { Text = "Separação total de bens", Value = "Separação total de bens" });
			regimesCasamento.Add(new SelectListItem() { Text = "Participação final nos aquestos", Value = "Participação final nos aquestos" });
			regimesCasamento.Add(new SelectListItem() { Text = "Separação obrigatória de bens", Value = "Separação obrigatória de bens" });
			return regimesCasamento.ToList();
		}

		public static List<SelectListItem> TipoAnalise()
		{
			IList<SelectListItem> tiposAnalise = new List<SelectListItem>();
			tiposAnalise.Add(new SelectListItem() { Text = "Padrão", Value = "Padrão" });
			tiposAnalise.Add(new SelectListItem() { Text = "Aluguel", Value = "Aluguel" });
			return tiposAnalise.ToList();
		}

		public static List<SelectListItem> StatusUnidade()
		{
			IList<SelectListItem> status = new List<SelectListItem>();
			status.Add(new SelectListItem() { Text = "Disponível", Value = "Disponível" });
			status.Add(new SelectListItem() { Text = "Vendida", Value = "Vendida" });
			return status.ToList();
		}

		public static List<SelectListItem> TipoUnidade()
		{
			IList<SelectListItem> tipo = new List<SelectListItem>();
			tipo.Add(new SelectListItem() { Text = "Residencial", Value = "Residencial" });
			tipo.Add(new SelectListItem() { Text = "Comercial", Value = "Comercial" });
			return tipo.ToList();
		}
	}

}