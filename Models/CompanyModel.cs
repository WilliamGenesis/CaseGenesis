namespace Models
{
	public class CompanyModel
	{
		public AddressModel MainAddress { get; set; }
		public AddressModel[] OtherAddresses { get; set; }
		public string TvaNumber { get; set; }
	}
}
