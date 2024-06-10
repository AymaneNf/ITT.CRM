namespace ITT.CRM.Domain
{
	public class Contact
	{
        public int Id { get; set; }
        public string? Civilite { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Entreprise { get; set; }
        public string? Fonction { get; set; }
        public string? Telephone { get; set; }
        public string? Email { get; set; }
        public string? Adresse { get; set; }
    }
}
