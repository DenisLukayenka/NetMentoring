namespace Parky.ConsoleTest
{
    public class TestModel
    {
        public TestModel(SubModel sub) 
        {
            this.SubModel = sub;
        }

        public PropertyModel PropertyModel { get; set; }

        public SubModel SubModel { get; set; }
        public string Data { get; set; }
    }
}