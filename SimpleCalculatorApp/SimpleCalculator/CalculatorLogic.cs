namespace SimpleCalculator;
public static class CalculatorLogic
{
    private static List<string> SplitInput(string input)
    {
        return input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToList();
    }

    private static double Calculate(List<string> inputs)
    {
        List<double> numbers = inputs.Where((_, index) => index % 2 == 0)
                                     .Select(double.Parse)
                                     .ToList();

        List<char> operands = inputs.Where((_, index) => index % 2 != 0)
                                    .Select(ConvertOperator)
                                    .ToList();

        // PEMDAS Priority. Checks for and Handles */- operations
        for (int i = 0; i < operands.Count;)
        {
            if (operands[ i ] is '*' or '/')
            {
                double left = numbers[ i ];
                double right = numbers[ i + 1 ];

                double result = operands[ i ] switch
                {
                    '*' => left * right,
                    '/' => right != 0 ? left / right : throw new DivideByZeroException(),
                    _ => throw new InvalidOperationException($"\"{operands[ i ]}\" is not a proper operand.")
                };

                numbers[ i ] = result;
                numbers.RemoveAt(i + 1);
                operands.RemoveAt(i);
            }
            else
                i++; // Operand is a + or -.
        }

        double total = numbers.First();

        // Secondary PEMDAS Priority check
        for (int i = 0; i < operands.Count; i++)
        {
            total = operands[ i ] switch
            {
                '+' => total + numbers[ i + 1 ],
                '-' => total - numbers[ i + 1 ],
                _ => total
            };
        }

        return total;
    }

    private static char ConvertOperator(string operation)
    {
        switch (operation)
        {
            case "+":
                return '+';
            case "-":
                return '-';
            case "x":
            case "X":
            case "*":
                return '*';
            case "/":
                return '/';
            default:
                throw new InvalidOperationException($"\"{operation}\" is not a proper operand.");
        }
    }

    private static bool ContinueUsing()
    {
        bool output;

        Console.Write("\nContinue (Y\\N): ");
        string input = Console.ReadLine();
        if (input.Trim().ToUpper() == "Y")
        {
            return output = true;
        }
        else
        {
            return output = false;
        }
    }

    public static void PromptUser()
    {
        bool continueUsing = true;

        while (continueUsing)
        {
            try
            {
                Console.Write("\nEnter the equation: ");
                Console.WriteLine($"The answer is: {Calculate(SplitInput(Console.ReadLine()))}.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            continueUsing = ContinueUsing();
        }
    }
}
