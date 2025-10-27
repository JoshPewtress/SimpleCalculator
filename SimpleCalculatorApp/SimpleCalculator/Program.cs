

List<string> SplitInput(string input)
{
    return input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();
}
