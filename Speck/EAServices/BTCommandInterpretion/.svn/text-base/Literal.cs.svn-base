using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Speck.EAServices.BTCommandInterpretion
{
    public class Literal:ITerm
    {
        private string mName;
        private string mMainParam;
        private string mParam;
        //private IList<ITerm> mTerms = new List<ITerm>();
        private static readonly Regex mNoneLiterals = new Regex(";");
        public void Parse(Context context)
        {
            if (mNoneLiterals.IsMatch(context.CurrentToken().Value))
            {
                return;
            }
            
            string currentTokenValue=context.CurrentToken().Value.ToLower();

            //bt specific evaluation
            if (currentTokenValue == "bt")
            {
                mName = currentTokenValue;
                context.SkipToken(context.CurrentToken().Value);

                currentTokenValue = context.CurrentToken().Value.ToLower();
                if (currentTokenValue == "address" || currentTokenValue == "status")
                {
                    mMainParam = currentTokenValue;
                    context.SkipToken(context.CurrentToken().Value);
                }
                else
                {
                    throw new Exception("Invalid command format!");
                }

                currentTokenValue = context.CurrentToken().Value.ToLower();
                if (mMainParam == "status")
                {
                    if (!mNoneLiterals.IsMatch(currentTokenValue))
                    {
                        throw new Exception("Invalid command format!");
                    }
                }
                else if (mMainParam == "address")
                {
                    if (!mNoneLiterals.IsMatch(currentTokenValue))
                    {
                        mParam = context.CurrentToken().Value;
                        context.NextToken();
                    }
                }
                return;
            }
            else
            {
                throw new Exception("Invalid command format!");
            }
        }

        public void Interprete()
        {
            //throw new NotImplementedException();
        }


        public override string ToString()
        {
            return mName+" "+ mMainParam+ " "+ mParam;
        }

        public string MainParam 
        { 
            get
            {
                return mMainParam;
            }
        }

        public string Name
        {
            get
            {
                return mName;
            }
        }

        public string Param
        {
            get
            {
                return mParam;
            }
        }

        public string this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return mName;
                        break;
                    case 1:
                        return mMainParam;
                        break;
                    case 2:
                        return mParam;
                        break;
                    default:
                        return null;
                }
            }
        }
    }
}
