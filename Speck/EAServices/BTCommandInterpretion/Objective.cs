using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Speck.EAServices.Helper;

namespace Speck.EAServices.BTCommandInterpretion
{
    internal class Objective:ITerm
    {
        private ITerm mLiteral;
        private object mParam;
        private ITerm mObjective;
        private IList<ITerm> mTerms=new List<ITerm>();
        public void Parse(Context context)
        {
            string currentTokenValue = context.CurrentToken().Value.ToLower();
            context.SkipToken(context.CurrentToken().Value);
            while (true)
            {
                if (context.CurrentToken().Kind == TokenKind.EOF || context.CurrentToken().Kind == TokenKind.EOL)
                {
                    break;
                }
                else if (currentTokenValue == "bt" || currentTokenValue == "adress" || currentTokenValue == "status")
                {
                    mLiteral = new Literal();
                    mLiteral.Parse(context);
                    mTerms.Add(mLiteral);
                    //break;
                }
                else if(currentTokenValue!=";")
                {
                    mObjective = new Objective();
                    mObjective.Parse(context);
                    mTerms.Add(mObjective);
                }
            }
        }

        public void Interprete()
        {
            foreach (ITerm term in mTerms)
            {
                term.Interprete();
            }
            IEnumerator<ITerm> it=mTerms.GetEnumerator();
            while(it.MoveNext())
            {
                //it.Current.Interprete();
                if (it.ToString() == "bt")
                {
                    if (!it.MoveNext())
                    {
                        //throw new Exception(Invalid);
                    }
                }
            }

        }

        public object this[int index]
        {
            get
            {
                return mTerms[index];
            }
        }

        public override string ToString()
        {
            return null;
        }
         
    }
}
