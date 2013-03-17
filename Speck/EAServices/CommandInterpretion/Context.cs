using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Speck.EAServices.Helper;

namespace Speck.EAServices.CommandInterpretion
{
    public class Context
    {
        private Token mCurrentToken;
        private StringTokenizer mTokenizer;
        private IList<object> mResults=new List<object>();

        public Context(string text)
        {
            mTokenizer = new StringTokenizer(text);
            mTokenizer.IgnoreWhiteSpace = true;
            mTokenizer.SymbolChars = new char[] {':',';'};
            NextToken();
        }

        public void NextToken()
        {
            mCurrentToken= mTokenizer.Next();
        }

        public Token CurrentToken()
        {
            return mCurrentToken;
        }

        public void SkipToken(string tokenValue)
        {
            if (tokenValue != mCurrentToken.Value)
            {
                throw new Exception("Warning:" + tokenValue + " is Expected,but" + mCurrentToken.Value + " is found");
            }
            NextToken();
        }

        public IList<object> Result
        {
            get
            {
                return mResults;
            }
        }

    }
}
