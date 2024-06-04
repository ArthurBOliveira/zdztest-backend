namespace ZdzTest_Models
{
    public class Activity : BaseModel
    {
        public Guid IdDeveloper { get; set; }
        public Guid IdCustomer { get; set; }
        public float Hours { get; set; }
    }
}
