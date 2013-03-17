using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Speck.EAServices.Helper;

namespace Speck.EAServices.CommandInterpretion
{
    internal class Expression:ITerm
    {
        private ITerm mAction;
        private ITerm mExpression;
        private IList<ITerm> mList = new List<ITerm>();
        private ITerm mProvidedAction;

        public Expression()
        { }

        public Expression(ITerm providedAction)
        {
            mProvidedAction = providedAction;
        }

        public void Parse(Context context)
        {
            while (true)
            {
                if (context.CurrentToken().Kind == TokenKind.EOF || context.CurrentToken().Kind == TokenKind.EOL)
                {
                    break;
                }
                else if (Action.IsAction(context))
                {
                    if (mProvidedAction == null)
                    {
                        mAction = new Action();
                    }
                    else
                    {
                        mAction = mProvidedAction;
                    }
                    mAction.Parse(context);
                    mList.Add(mAction);
                    //break;
                }
                else if(context.CurrentToken().Value==";")
                {
                    context.SkipToken(";");
                }
                else
                {
                    mExpression = new Expression();
                    mExpression.Parse(context);
                    mList.Add(mExpression);
                }
            }
        }

        public void Interprete()
        {
            foreach(var expression in mList)
            {
                expression.Interprete();
            }
        }
    }
}
