using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EgzekucjeModel.CodeGenerator
{
    public class JsonAdapterGenerator
    {
        public string Generate()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var classes = GetTypesWithApplicationServiceAttribute(assembly);

            foreach (var clazz in classes)
            {
                StringBuilder clazzString = new StringBuilder();
                clazzString.Append($"public class {clazz.Name}JsonAdapter");
                clazzString.AppendLine().Append("{");

                clazzString.AppendLine().Append($"private static {clazz.Name} {clazz.Name.ToLowerFirstChar()} = new {clazz.Name}();");

                var methods = clazz.GetMethods();
                foreach (var method in methods)
                {
                    var name = method.Name;
                    if (name == "Equals" || name == "GetHashCode" || name == "GetType" || name == "ToString") continue;
                    var parameters = method.GetParameters();
                    Type returnType = method.ReturnType;

                    var paramList = string.Join(", ", parameters.ToList().Select(p => GetPrintableType(p.ParameterType) + " " + p.Name).ToArray());
                    clazzString.AppendLine().Append($"public static {GetPrintableType(returnType)} {name}({paramList})");
                    clazzString.AppendLine().AppendLine("{");
                    clazzString.AppendLine("   throw new NotImplementedException();");
                    clazzString.AppendLine("}");

                }

                clazzString.AppendLine().Append("}");
                Console.WriteLine(clazzString.ToString());
            }

            return string.Empty;
        }

        private string GetPrintableType(Type type)
        {
            if (type.IsGenericType)
            {
                string typeString = (type.GetGenericTypeDefinition() == typeof(List<>)) ? "List" : type.Name;
                return type.Namespace + "." + typeString + "<" + string.Join(", ", type.GenericTypeArguments.ToList().Select(a => a.FullName).ToArray()) + ">"; 
            }

            var fullName = type.FullName;

            if (fullName == "System.Void") return "void";
            if (fullName == "System.String") return "string";
            if (fullName.StartsWith("System.Int64")) return "long";
            if (fullName.StartsWith("System.Int")) return "int";
            return fullName;
        }

        static IEnumerable<Type> GetTypesWithApplicationServiceAttribute(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(ApplicationServiceAttribute), true).Length > 0)
                {
                    yield return type;
                }
            }
        }


    }

    public static class StringExtensions
    {
        public static string ToLowerFirstChar(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToLower(input[0]) + input.Substring(1);
        }
    }
}
