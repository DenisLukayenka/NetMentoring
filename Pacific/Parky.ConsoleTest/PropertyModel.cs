namespace Parky.ConsoleTest
{
    public class PropertyModel
    {
        public int Sum { get; set; }
    }

    public interface A
    {
        string Prop { get; }
    }

    public class B : A
    {
        public string Prop { get; } = "B";
    }
}