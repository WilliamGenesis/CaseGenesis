namespace Models
{
	public class ContactModel
	{
		public string Address { get; set; }
		public CompanyModel[] Companies { get; set; }
		public bool IsFreelance { get; set; }
		public string TvaNumber { get; set; }
	}
}
