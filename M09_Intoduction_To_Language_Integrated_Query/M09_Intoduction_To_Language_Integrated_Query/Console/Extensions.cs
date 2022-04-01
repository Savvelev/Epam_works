namespace M09_Intoduction_To_Language_Integrated_Query
{
    public static class Extensions
    {
       public static string ToFormatedString(this Student student) =>

             string.Format("{0}\t{1}\t{2}\t{3}", student.Name, student.Test, student.Date.ToString("dd/MM/yyyy"), student.Mark);
    }
}
