using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public static class PolishCharactersChanger
    {
        private static char ChangePolishCharacter(char characterToChange)
        {
            char changedCharacter = characterToChange switch
            {
                'ą' => 'a',
                'Ą' => 'A',
                'ć' => 'c',
                'Ć' => 'C',
                'ę' => 'e',
                'Ę' => 'E',
                'ł' => 'l',
                'Ł' => 'L',
                'ń' => 'n',
                'Ń' => 'N',
                'ó' => 'o',
                'Ó' => 'O',
                'ś' => 's',
                'Ś' => 'S',
                'ż' => 'z',
                'Ż' => 'Z',
                'ź' => 'z',
                'Ź' => 'Z',
                _ => characterToChange
            };
            return changedCharacter;
        }

        public static string ChangePolishCharatersInString(string stringToChange)
        {
            var stringChars = stringToChange.ToCharArray();
            for(int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = ChangePolishCharacter(stringChars[i]);
            }
            return stringChars.ToString();
        }
    }
}
