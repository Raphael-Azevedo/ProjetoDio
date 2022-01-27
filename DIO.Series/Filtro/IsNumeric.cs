namespace DIO.Series.Filtro
{
    public class validacao
    {
        public static bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }  
    }
}