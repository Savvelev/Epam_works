using System;
using System.Text.RegularExpressions;

namespace GenericsAndCollections
{
    public class PolishNotation
    {
        readonly Regex regex = new(@"^\s*(\d+\s+){2}((\d+|[-+*\/])\s+)*[-+*\/]\s*$");
        public double ReverseCalculator(string expression)
        {
            if (expression.Length == 0)           
                return 0;
            
            if (!regex.Match(expression).Success)           
                throw new ArgumentException("Wrong expression");
            
            string[] splitedExpression = expression.Split(' ');
            
            var st = new Stack<double>();

            for (int i = 0; i < splitedExpression.Length; i++)
            {
                if (double.TryParse(splitedExpression[i], out double result))
                    st.Push(result);
                else
                {
                    double temp;
                    switch (splitedExpression[i])
                    {
                        case "+":
                            st.Push(st.Pop() + st.Pop());
                            break;
                        case "*":
                            st.Push(st.Pop() * st.Pop());
                            break;
                        case "-":
                            temp = st.Pop();
                            st.Push(st.Pop() - temp);
                            break;
                        case "/":
                            temp = st.Pop();
                            if (temp != 0.0)
                                st.Push(st.Pop() / temp);
                            else
                                throw new DivideByZeroException("Divide by zero");
                            break;
                    }
                }
            }
            return st.Pop();
        }
    }
}
