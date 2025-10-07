using TimeTracker.BE.DB.Models.Entities;

namespace TimeTracker.PC.Models
{
	/// <summary>
	/// Třída ShiftCmb rozšiřuje entitu Shift o další vlastnosti pro zobrazení v UI.
	/// </summary>
	public class ShiftCmb : Shift
	{
		/// <summary>
		/// Vrací řetězec s datem začátku směny ve formátu "dd.MM.yy".
		/// Pokud není datum nastaveno (je MaxValue), vrací prázdný řetězec.
		/// </summary>
		public string StartDateStr
		{
			get
			{
				var result = "";
				if (StartDate != DateTime.MaxValue)
					result = StartDate.ToString("dd.MM.yy");

				return result;
			}
		}

		/// <summary>
		/// Výchozí konstruktor nastaví StartDate na MaxValue.
		/// </summary>
		public ShiftCmb()
		{
			StartDate = DateTime.MaxValue;
		}

		/// <summary>
		/// Konstruktor, který vytvoří instanci ShiftCmb z existujícího objektu Shift.
		/// </summary>
		/// <param name="shift">Objekt Shift, ze kterého se převezmou hodnoty.</param>
		public ShiftCmb(Shift shift) : this()
		{
			GuidId = shift.GuidId;
			Description = shift.Description;
			StartDate = shift.StartDate;
		}
	}
}