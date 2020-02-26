using Parky.Attributes;

namespace Parky.ConsoleTest
{
    [ImportConstructor]
    public class TestModel
    {
        public TestModel(SubModel sub) 
        {
            this.SubModel = sub;
        }

        [Import]
        public PropertyModel PropertyModel { get; set; }

        public SubModel SubModel { get; set; }
        public string Data { get; set; }
    }
}