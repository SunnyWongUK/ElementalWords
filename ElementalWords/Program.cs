namespace ElementalWords
{
    public class BuiltIn
    {
        public static string ELEMENTS(string symbol) 
        {
            //The helper object ELEMENTS has been provided, which is a map from each element symbol to its corresponding full name.
            //Assume this ELEMENTS returns null if an element does not exist from the symbol.

            //Just assume return null to avoid design error
            return null;
        }
    }

    internal class Program : BuiltIn
    {      
        static string ChangeElementLetterToUpperOrLowerCase(string symbol, int letterPos, bool isUpperCase)
        {
            //Change element letter to upper or lower Case

            string startSubString;
            string endSubString;

            startSubString = letterPos == 0 ? "" : symbol.Substring(0, letterPos - 1);
            endSubString = letterPos == symbol.Length - 1 ? "" : symbol.Substring(letterPos + 1);
            if (isUpperCase)
            {
                symbol = startSubString + symbol.Substring(letterPos, 1).ToUpper() + endSubString;
            }
            else
            {
                symbol = startSubString + symbol.Substring(letterPos, 1).ToLower() + endSubString;
            }

            return symbol;
        }

        static string checkElement(ref string symbol, int startIndex)
        {
            //Find an element with all of possible symbol upper and lower cases recursively

            string element = ELEMENTS(symbol);
            if (ELEMENTS(symbol) != null)
            {
                return element;
            }
            else
            {
                for (int i = startIndex; i < symbol.Length; i++)
                {
                    for (int j = 1; j <= 2; j++)
                    {
                        //Find an element with all of possible symbols having next symbol letter upper case
                        symbol = ChangeElementLetterToUpperOrLowerCase(symbol, i, true);

                        //Continue to find an element with the next symbol letter upper case recursively
                        element = checkElement(ref symbol, i + 1);

                        if (element != null)
                        {
                            return element;
                        }
                        else
                        {
                            //Find an element with all of possible symbols having next symbol letter lower case
                            symbol = ChangeElementLetterToUpperOrLowerCase(symbol, i, false);

                            //Continue to find an element with the next symbol letter lower case recursively
                            element = checkElement(ref symbol, i + 1);

                            if (element != null)
                            {
                                return element;
                            }
                        }
                    }
                }

                return null;
            }
        }

        static void elementalForms(ref string word, ref int lettersNoCheck, ref List<List<string>> forms)
        {
            List<string> form;

            //The max symbol length is 3.
            if (word.Length != 0 && lettersNoCheck <= 3)
            {
                int actualLetterNoCheck = word.Length < lettersNoCheck ? word.Length : lettersNoCheck;

                string lettersCheck = word.Substring(0, actualLetterNoCheck);
                string element = checkElement(ref lettersCheck, 0);

                //If element is not null, the element is found from the symbol and added into the List forms.
                if (element != null)
                {
                    form = forms.Count == 0 ? new List<string>() : forms[forms.Count - 1];

                    form.Add(element + " (" + lettersCheck + ")");

                    if (word.Length > actualLetterNoCheck)
                    {
                        word = word.Substring(actualLetterNoCheck);
                        lettersNoCheck = lettersNoCheck == 3 ? 1 : lettersNoCheck++;

                        //Continue to find whether an element exists recursively
                        elementalForms(ref word, ref lettersNoCheck, ref forms);
                    }
                }
                else
                {
                    //If an element is not existed from the symbol and removed the List forms.
                    if (forms.Count != 0)
                    {
                        forms.RemoveAt(forms.Count - 1);
                    }
                }
            }
            else
            {
                //If an element is not existed from the symbol and removed the List forms.
                if (forms.Count != 0)
                {
                    forms.RemoveAt(forms.Count - 1);
                }
            }
        }

        static string[,] elementalForms(string word)
        {
            string[,] forms;
            List<List<string>> tempForms = new List<List<string>>();
            string processedword = word;
            int lettersNoCheck = 1;
            int row;
            int col;

            elementalForms(ref processedword, ref lettersNoCheck, ref tempForms);

            //The usage List tempForms is easy to manipulate under this process.  After result List tempForms evaluated, the following coding would be
            //converted to List to Array for output.
            if (tempForms.Count == 0) 
            {
                forms = new string[0, 0];
            }
            else
            {
                row = tempForms.Count;
                var formCountList = tempForms.Select(i => i.Count()).ToList();
                col = formCountList.Max();
                forms = new string[row, col];

                for (int i = 0; i < tempForms.Count; i++)
                {
                    for (int j = 0; j < tempForms[i].Count; j++)
                    {
                        forms[i, j] = tempForms[i][j];
                    }
                }
            }

            return forms;
        }

        static void Main(string[] args)
        {
            string[,] forms = elementalForms("BEACH");
        }
    }
}
