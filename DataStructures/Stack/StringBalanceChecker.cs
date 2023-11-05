namespace Stack;

public class StringBalanceChecker
{
    private readonly char[] _leftBrackets = {'(', '[', '{', '<'};
    private readonly char[] _rightBrackets = {')', ']', '}', '>'};
    private bool IsOpenSymbol(char ch)
    {
        
        return _leftBrackets.Contains(ch);
    }

    private bool IsCloseSymbol(char ch)
    {
        return _rightBrackets.Contains(ch);
    }

    private bool DoesMatch(char open, char close)
    {
        return Array.IndexOf(_leftBrackets, open) 
               ==
               Array.IndexOf(_rightBrackets, close);
    }
    private readonly Stack<char> _symbolsInString = new();
    public bool IsBalance(string stringValue)
    {
        foreach (var character in stringValue)
        {
            if (IsOpenSymbol(character))
                _symbolsInString.Push(character);

            else if (IsCloseSymbol(character))
            {
                if (IsOpenSymbolsStackEmpty())
                    return false;
                if (!DoesMatch(_symbolsInString.Pop(), character))
                    return false;
            }
        }
        

        return true;
    }

    private bool IsOpenSymbolsStackEmpty()
    {
        return _symbolsInString.Count == 0;
    }
}