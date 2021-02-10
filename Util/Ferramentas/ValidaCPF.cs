using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Ferramentas
{
	public static class ValidaCPF
	{
		public static bool ValidarCPF(string Cpf)
		{
			int[] Multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] Multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			string TempCpf;
			string Digito;
			int Soma;
			int Resto;

			Cpf = Cpf.Trim();
			Cpf = Cpf.Replace(".", "").Replace("-", "");

			if (Cpf.Length != 11)
				return false;

			TempCpf = Cpf.Substring(0, 9);
			Soma = 0;

			for (int i = 0; i < 9; i++)
				Soma += int.Parse(TempCpf[i].ToString()) * Multiplicador1[i];

			Resto = Soma % 11;
			if (Resto < 2)
				Resto = 0;
			else
				Resto = 11 - Resto;

			Digito = Resto.ToString();

			TempCpf = TempCpf + Digito;

			Soma = 0;
			for (int i = 0; i < 10; i++)
				Soma += int.Parse(TempCpf[i].ToString()) * Multiplicador2[i];

			Resto = Soma % 11;
			if (Resto < 2)
				Resto = 0;
			else
				Resto = 11 - Resto;

			Digito = Digito + Resto.ToString();

			return Cpf.EndsWith(Digito);
		}
	}
}
