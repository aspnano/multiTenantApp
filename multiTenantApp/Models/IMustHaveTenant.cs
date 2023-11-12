namespace multiTenantApp.Models
{
    public interface IMustHaveTenant
    {
        public string TenantId { get; set; }
    }
}
