try
{
    Console.WriteLine(
        """
        Simple Calculator Application

        Enter the math equation you would like solved, the format is:
        number,operand,number. ex: 2,/,3,+,5
        """
    );

    do
    {
        Console.Write("\nEnter the equation: ");
        Console.WriteLine($"The answer is: {Calculate(SplitInput(Console.ReadLine()))}."); 
    } while (ContinueUsing());
}
catch (Exception e)
{
    Console.WriteLine();
    throw;
}

List<string> SplitInput(string input)
{
    return input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();
}

double Calculate(List<string> inputs)
{
    try
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
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        return 0;
    }
}

char ConvertOperator(string operation)
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

bool ContinueUsing()
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
