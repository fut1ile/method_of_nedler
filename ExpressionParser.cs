using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Text.RegularExpressions;

namespace NelderMeadApp
{
    public static class ExpressionParser
    {
        public static Func<double[], double> Compile(string expression, int variableCount)
        {
            string processed = expression;

            for (int i = variableCount; i >= 1; i--)
                processed = processed.Replace($"x{i}", $"__VAR_{i}__");

            if (variableCount >= 1)
                processed = Regex.Replace(processed, @"\bx\b", "__VAR_X__");
            if (variableCount >= 2)
                processed = Regex.Replace(processed, @"\by\b", "__VAR_Y__");
            if (variableCount >= 3)
                processed = Regex.Replace(processed, @"\bz\b", "__VAR_Z__");

            processed = ReplacePowerOperator(processed);

            processed = processed.Replace("sin", "Math.Sin");
            processed = processed.Replace("cos", "Math.Cos");
            processed = processed.Replace("tan", "Math.Tan");
            processed = processed.Replace("exp", "Math.Exp");
            processed = processed.Replace("sqrt", "Math.Sqrt");
            processed = processed.Replace("abs", "Math.Abs");
            processed = processed.Replace("log", "Math.Log");

            for (int i = variableCount; i >= 1; i--)
                processed = processed.Replace($"__VAR_{i}__", $"x[{i - 1}]");

            if (variableCount >= 1)
                processed = processed.Replace("__VAR_X__", "x[0]");
            if (variableCount >= 2)
                processed = processed.Replace("__VAR_Y__", "x[1]");
            if (variableCount >= 3)
                processed = processed.Replace("__VAR_Z__", "x[2]");

            string code = $@"
using System;
public static class DynamicFunction
{{
    public static double Evaluate(double[] x)
    {{
        return {processed};
    }}
}}";

            using (CSharpCodeProvider provider = new CSharpCodeProvider())
            {
                CompilerParameters parameters = new CompilerParameters
                {
                    GenerateInMemory = true,
                    GenerateExecutable = false
                };

                CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

                if (results.Errors.HasErrors)
                {
                    string errors = string.Join("\n",
                        results.Errors.Cast<CompilerError>().Select(err => err.ErrorText));
                    throw new Exception("Ошибка компиляции функции:\n" + errors +
                                        "\n\nСгенерированный код:\n" + code);
                }

                var assembly = results.CompiledAssembly;
                var type = assembly.GetType("DynamicFunction");
                var method = type.GetMethod("Evaluate");
                return (Func<double[], double>)Delegate.CreateDelegate(typeof(Func<double[], double>), method);
            }
        }

        private static string ReplacePowerOperator(string expr)
        {
            string result = expr;
            int prevLength;

            do
            {
                prevLength = result.Length;
                result = Regex.Replace(result, @"\(([^()]+)\)\^(\d+(\.\d+)?)", "Math.Pow($1,$2)");
                result = Regex.Replace(result, @"(\d+(\.\d+)?)\^(\d+(\.\d+)?)", "Math.Pow($1,$3)");
                result = Regex.Replace(result, @"([a-zA-Z_]\w*)\^(\d+(\.\d+)?)", "Math.Pow($1,$2)");
                result = Regex.Replace(result, @"([a-zA-Z_]\w*)\^([a-zA-Z_]\w*)", "Math.Pow($1,$2)");
            } while (result.Length != prevLength);

            return result;
        }
    }
}